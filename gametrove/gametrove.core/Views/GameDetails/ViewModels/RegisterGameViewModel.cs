using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Infrastructure.Results;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.Views.GameDetails.ViewModels
{
    public class RegisterGameViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (!value.Equals(_name))
                {
                    _name = value;

                    OnPropertyChanged();

                    IsRegistrationPermitted = IsValid;
                }
            }
        }

        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set
            {
                if (!value.Equals(_subtitle))
                {
                    _subtitle = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                if (!value.Equals(_code))
                {
                    _code = value;

                    OnPropertyChanged();
                }
            }
        }

        private Guid _platform;

        public Guid Platform
        {
            get => _platform;
            set
            {
                if (!value.Equals(_platform))
                {
                    _platform = value;

                    OnPropertyChanged();

                    IsRegistrationPermitted = IsValid;
                }
            }
        }

        private bool _isRegistrationPermitted;

        public bool IsRegistrationPermitted
        {
            get => _isRegistrationPermitted;
            set
            {
                _isRegistrationPermitted = value;

                RegisterGame.ChangeCanExecute();

                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlatformModel> Platforms { get; set; }

        public Command<string> RegisterGame { get; }
        public Command GetPlatformsCommand { get; }

        private readonly APIActionService _service;
        private bool IsValid => Platform != Guid.Empty && !string.IsNullOrEmpty(Name);

        public RegisterGameViewModel()
        {
            _service = DependencyService.Resolve<APIActionService>();

            Platforms = new ObservableCollection<PlatformModel>();

            RegisterGame = new Command<string>(async (scan) =>
                    await RegisterNewGame(bool.Parse(scan)),
                _ => IsValid);

            GetPlatformsCommand = new Command(async () => await GetPlatforms());
        }

        public RegisterGameViewModel(string code) : this()
        {
            Code = code;
        }

        private async Task RegisterNewGame(bool scan)
        {
            var added = await _service.Execute(new RegisterNewGameAction(Name, Subtitle, Code, Platform));

            if (added != null)
            {
                MessagingCenter.Send(this, "Game:Registered",
                    new RegistrationResult
                    {
                        Model = added,
                        ShouldScan = scan
                    });
            }
        }

        private async Task GetPlatforms()
        {
            var platforms = await _service.Execute(new GetPlatformsAction());

            IsBusy = true;

            Platforms.Clear();

            foreach (var platform in platforms)
            {
                Platforms.Add(platform);
            }

            IsBusy = false;
        }
    }
}