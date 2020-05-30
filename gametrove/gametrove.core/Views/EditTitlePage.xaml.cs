using System;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTitlePage : ContentPage
    {
        private readonly EditTitleViewModel _vm;

        public EditTitlePage(Guid gameId)
        {
            InitializeComponent();

            BindingContext = _vm = new EditTitleViewModel(gameId);

            MessagingCenter.Subscribe<EditTitleViewModel, TitleModel>(this, "Title:Updated", async (vm, game) =>
                {
                    await Navigation.PopModalAsync(true);
                });
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}