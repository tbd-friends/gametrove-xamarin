using System;
using System.Linq;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private UserAuthentication _user;

        public LoginPage()
        {
            InitializeComponent();

            _user = DependencyService.Get<UserAuthentication>();
        }

        public async void Login_Clicked(object sender, EventArgs e)
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            var authenticationResult = await authenticationService.Authenticate();

            if (!authenticationResult.IsError)
            {
                var apiService = DependencyService.Get<APIActionService>();

                if (await apiService.Execute(new VerifyUserAction()))
                {
                    _user.Initialize(authenticationResult);

                    Application.Current.MainPage = new AppShell();
                }
            }
        }
    }
}