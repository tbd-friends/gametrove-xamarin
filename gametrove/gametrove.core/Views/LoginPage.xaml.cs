using System;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void Login_Clicked(object sender, EventArgs e)
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            var authenticationResult = await authenticationService.Authenticate();

            if (!authenticationResult.IsError)
            {
                Preferences.Set(AppPreferences.IdentityToken, authenticationResult.IdToken);
                Preferences.Set(AppPreferences.AccessToken, authenticationResult.AccessToken);

                Application.Current.MainPage = new AppShell();
            }
        }
    }
}