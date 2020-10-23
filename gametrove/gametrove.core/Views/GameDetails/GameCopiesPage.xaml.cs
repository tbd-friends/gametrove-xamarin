using System;
using Gametrove.Core.Infrastructure.Cache;
using Gametrove.Core.Services.Models;
using Gametrove.Core.Views.GameDetails.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views.GameDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameCopiesPage : ContentPage
    {
        private GameCopiesViewModel _vm;

        public static readonly BindableProperty ModelProperty = BindableProperty.Create(
            nameof(Model),
            typeof(GameModel),
            typeof(GameCopiesPage),
            default(GameModel), propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                if (bindable is GameCopiesPage page &&
                    newvalue is GameModel model)
                {
                    page.Model = model;
                }
            });

        public GameModel Model
        {
            get => (GameModel)BindingContext;
            set => BindingContext = _vm = new GameCopiesViewModel(value) { Navigation = Navigation };
        }

        public GameCopiesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.LoadCopiesCommand.Execute(null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCopyPage(_vm.Id));
        }
    }
}