using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class RegisterGameViewModel : BaseViewModel
    {
        private string _name;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (value == _isBusy) return;

                _isBusy = value;

                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (!value.Equals(_name))
                {
                    _name = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (!value.Equals(_description))
                {
                    _description = value;

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
                }
            }
        }

        public ObservableCollection<PlatformModel> Platforms { get; set; }

        public Command RegisterGame { get; }
        public Command GetPlatformsCommand { get; }

        private readonly APIService _service;

        public RegisterGameViewModel()
        {
            _service = DependencyService.Resolve<APIService>();

            Platforms = new ObservableCollection<PlatformModel>();

            RegisterGame = new Command(async () => await RegisterNewGame());
            GetPlatformsCommand = new Command(async () => await GetPlatforms());
        }

        public RegisterGameViewModel(string code) : this()
        {
            Code = code;
        }

        private async Task RegisterNewGame()
        {
            var added = await _service.RegisterNewGame(Name, Description, Code, Platform);

            MessagingCenter.Send(this, "Game:Registered", added);
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