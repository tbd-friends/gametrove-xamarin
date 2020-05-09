using System;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditGamePage : ContentPage
    {
        private readonly Guid _id;
        private readonly EditGameViewModel _vm;

        public EditGamePage(Guid id)
        {
            InitializeComponent();

            _id = id;

            BindingContext = _vm = new EditGameViewModel();

            MessagingCenter.Subscribe<EditGameViewModel, GameModel>(this, "Game:Updated", async (vm, game) =>
                {
                    await Navigation.PopModalAsync(true);
                });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.LoadGameCommand.Execute(_id);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}