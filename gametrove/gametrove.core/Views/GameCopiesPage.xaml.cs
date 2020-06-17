using System;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
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

            MessagingCenter.Subscribe<RegisterCopyViewModel>(this, "Copy:Added", _ =>
            {
                Navigation.PopAsync(true);

                _vm.LoadCopiesCommand.Execute(this);
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.LoadCopiesCommand.Execute(null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterCopyPage(_vm.Id));
        }
    }
}