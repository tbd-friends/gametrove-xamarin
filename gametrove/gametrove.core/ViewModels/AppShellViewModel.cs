using Gametrove.Core.Services.Interfaces;
using Gametrove.Core.Views;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class AppShellViewModel
    {
        private IAuthenticationService _authenticationService;

        public Command LogoutCommand { get; }

        public AppShellViewModel()
        {
            _authenticationService = DependencyService.Get<IAuthenticationService>();

            LogoutCommand = new Command(async () =>
            {
                if (await _authenticationService.Logout())
                {
                    Application.Current.MainPage = new LoginPage();
                }
            });
        }
    }
}