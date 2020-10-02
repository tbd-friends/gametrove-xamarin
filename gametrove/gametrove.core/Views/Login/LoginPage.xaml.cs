using System;
using System.Threading.Tasks;
using Gametrove.Core.Model;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Interfaces;
using Gametrove.Core.Views.Login.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly UserAuthentication _user;
        private readonly IAuthenticationService _authenticationService;
        private readonly APIActionService _api;

        private readonly LoginViewModel _vm;

        public LoginPage()
        {
            InitializeComponent();

            _user = DependencyService.Get<UserAuthentication>();
            _authenticationService = DependencyService.Get<IAuthenticationService>();
            _api = DependencyService.Get<APIActionService>();

            BindingContext = _vm = new LoginViewModel();
        }

        public async void Login_Clicked(object sender, EventArgs e)
        {
            await DoLogin(async result =>
            {
                await VerifyUser();
            });
        }

        private async void UseToken_Clicked(object sender, EventArgs e)
        {
            await DoLogin(async result =>
            {
                Application.Current.ModalPopped += OnInviteClosed;

                await Navigation.PushModalAsync(new AcceptInvitationPage(result));
            });
        }

        private async void OnInviteClosed(object sender, ModalPoppedEventArgs e)
        {
            Application.Current.ModalPopped -= OnInviteClosed;

            if (e.Modal is AcceptInvitationPage invitation)
            {
                if (invitation.Status == InvitationStatus.Accepted)
                {
                    await VerifyUser();
                }
                else
                {
                    _vm.InvitationCancelled();
                }
            }
        }

        private async Task DoLogin(Func<AuthenticationResult, Task> onSuccess)
        {
            _vm.LoggingIn();

            var authenticationResult = await _authenticationService.Authenticate();

            if (authenticationResult != null && !authenticationResult.IsError)
            {
                _user.Initialize(authenticationResult);

                await onSuccess(authenticationResult);
            }
            else
            {
                _vm.AllowLogin();
            }
        }

        protected override async void OnAppearing()
        {
            if (_vm.Status == LoginStatus.Opening)
            {
                if (await _authenticationService.ShouldRefresh())
                {
                    var authenticationResult = await _authenticationService.Refresh();

                    if (authenticationResult != null)
                    {
                        _user.Initialize(authenticationResult);

                        await VerifyUser();
                    }
                    else
                    {
                        await DoLogin(async result =>
                        {
                            await VerifyUser();
                        });
                    }
                }
                else
                {
                    _vm.AllowLogin();
                }
            }
        }

        private async Task VerifyUser()
        {
            if (await _api.Execute(new VerifyUserAction()))
            {
                Application.Current.MainPage = new AppShell();
            }
        }
    }
}
