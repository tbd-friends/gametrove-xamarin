using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Interfaces;
using Xamarin.Forms;

namespace Gametrove.Core.Views.Login.ViewModels
{
    public class AcceptInvitationViewModel : BaseViewModel
    {
        private string _token;
        public string Token
        {
            get => _token;
            set
            {
                if (_token != value)
                {
                    _token = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _errorStatus;

        public string ErrorStatus
        {
            get => _errorStatus;
            set
            {
                if (_errorStatus != value)
                {
                    _errorStatus = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _email;
        public Command AcceptInvitationCommand { get; }
        public Command CancelInvitationCommand { get; }
        public INavigation Navigation { get; set; }

        public InvitationStatus Status { get; private set; }

        private readonly IAuthenticationService _authenticationService;

        public AcceptInvitationViewModel(string email)
        {
            _email = email;

            _authenticationService = DependencyService.Get<IAuthenticationService>();

            AcceptInvitationCommand = new Command(async () =>
            {
                var api = DependencyService.Get<APIActionService>();

                if (await api.Execute(new AcceptInviteAction(_email, Token)))
                {
                    Status = InvitationStatus.Accepted;

                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    ErrorStatus = "Unable to accept invite";
                }
            });

            CancelInvitationCommand = new Command(async () =>
            {
                Status = InvitationStatus.Rejected;

                await _authenticationService.Logout();

                await Navigation.PopModalAsync(true);
            });
        }
    }
}