using Gametrove.Core.Infrastructure;

namespace Gametrove.Core.ViewModels
{

    public enum LoginStatus
    {
        Opening = 0, 
        AllowingLogin, 
        LoggingIn,
        ShowingInvite
    }

    public class LoginViewModel : BaseViewModel
    {
        private LoginStatus _loginStatus;

        public LoginStatus Status
        {
            get => _loginStatus;
            set
            {
                if (_loginStatus != value)
                {
                    _loginStatus = value;

                    OnPropertyChanged();
                }
            }
        }

        public LoginViewModel()
        {
            Status = LoginStatus.Opening;
        }

        public void AllowLogin()
        {
            Status = LoginStatus.AllowingLogin;
        }

        public void LoggingIn()
        {
            Status = LoginStatus.LoggingIn;
        }

        public void InvitationCancelled()
        {
            Status = LoginStatus.AllowingLogin;
        }
    }
}