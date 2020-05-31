using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels.Results;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<GameModel> Games { get; set; }
        public Command LoadGamesCommand { get; set; }

        private string _scanButtonOrientation;

        public string ScanButtonOrientation
        {
            get => _scanButtonOrientation;
            set
            {
                if (value != _scanButtonOrientation)
                {
                    _scanButtonOrientation = value;

                    OnPropertyChanged();
                }
            }
        }

        private readonly APIActionService _api;

        public HomeViewModel()
        {
            Games = new ObservableCollection<GameModel>();

            LoadGamesCommand = new Command(async () => await ExecuteLoadGamesCommand(), () => true);

            _api = DependencyService.Resolve<APIActionService>();

            _scanButtonOrientation = Preferences.Get(AppPreferences.ScanButtonOrientation, "Right");

            MessagingCenter.Subscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered",
                (vm, result) =>
                {
                    Games.Insert(0, result.Model);
                });

            MessagingCenter.Subscribe<ConfigurationViewModel>(this, "Preferences:Changed", _ =>
                {
                    ScanButtonOrientation = Preferences.Get(AppPreferences.ScanButtonOrientation, "Right");
                });
        }

        public async Task<GameModel> LoadGameByCode(string code)
        {
            return await _api.Execute(new GetGameByCodeAction(code));
        }

        private async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            Games.Clear();

            foreach (var game in await _api.Execute(new GetRecentlyAddedGamesAction()))
            {
                Games.Add(game);
            }

            IsBusy = false;
        }
    }
}