using System;
using app.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterGame : ContentPage
    {
        private RegisterGameViewModel _vm;

        public RegisterGame()
        {
            InitializeComponent();

            BindingContext = _vm = new RegisterGameViewModel();
        }

    }
}