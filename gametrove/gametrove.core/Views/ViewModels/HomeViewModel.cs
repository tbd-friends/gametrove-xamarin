using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Infrastructure.Cache;
using Gametrove.Core.Infrastructure.Results;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Gametrove.Core.Views.GameDetails.ViewModels;
using Syncfusion.DataSource.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core.Views.ViewModels
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
        private readonly RecentGamesList _listing;

        public HomeViewModel()
        {
            Games = new ObservableCollection<GameModel>();

            LoadGamesCommand = new Command(async () => await ExecuteLoadGamesCommand(), () => true);

            _api = DependencyService.Resolve<APIActionService>();
            _listing = DependencyService.Resolve<RecentGamesList>();

            _scanButtonOrientation = Preferences.Get(AppPreferences.ScanButtonOrientation, "Right");

            MessagingCenter.Unsubscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered");
            MessagingCenter.Subscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered",
                async (vm, result) =>
                {
                    await _listing.Track(result.Model);
                });

            MessagingCenter.Unsubscribe<ConfigurationViewModel>(this, "Preferences:Changed");
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
            Games.Clear();

            var recentGames = (await _listing.Recent()).ToList<GameModel>();

            foreach (var game in recentGames)
            {
                Games.Add(game);
            }

            IsBusy = false;
        }
    }
}