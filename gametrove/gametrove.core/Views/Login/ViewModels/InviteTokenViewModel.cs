using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Xamarin.Forms;

namespace Gametrove.Core.Views.Login.ViewModels
{
    public class InviteTokenViewModel : BaseViewModel
    {
        private APIActionService _api;

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

        public Command GetCurrentInviteTokenCommand { get; }

        public InviteTokenViewModel()
        {
            _api = DependencyService.Get<APIActionService>();

            GetCurrentInviteTokenCommand = new Command(async () => { Token = await _api.Execute(new GetInviteTokenAction()); });
        }
    }
}