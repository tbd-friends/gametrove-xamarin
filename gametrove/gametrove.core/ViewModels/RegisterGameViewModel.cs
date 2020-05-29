using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels.Results;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
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

        private readonly APIService _service;
        private bool IsValid => Platform != Guid.Empty && !string.IsNullOrEmpty(Name);

        public RegisterGameViewModel()
        {
            _service = DependencyService.Resolve<APIService>();

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
            var added = await _service.RegisterNewGame(Name, Subtitle, Code, Platform);

            MessagingCenter.Send(this, "Game:Registered", new RegistrationResult { Model = added, ShouldScan = scan });
        }

        private async Task GetPlatforms()
        {
            var platforms = await _service.GetPlatforms();

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