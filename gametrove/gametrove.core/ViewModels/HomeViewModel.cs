using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels.Results;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<GameModel> Games { get; set; }
        public Command LoadGamesCommand { get; set; }

        private readonly APIService _api;

        public HomeViewModel()
        {
            Games = new ObservableCollection<GameModel>();

            LoadGamesCommand = new Command(async () => await ExecuteLoadGamesCommand(), () => true);

            _api = DependencyService.Resolve<APIService>();

            MessagingCenter.Subscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered",
                (vm, result) =>
                {
                    Games.Insert(0, result.Model);
                });
        }

        public async Task<GameModel> LoadGameByCode(string code)
        {
            return await _api.GetGameByCode(code);
        }

        private async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            Games.Clear();

            foreach (var game in await _api.GetRecentlyAddedGames())
            {
                Games.Add(game);
            }

            IsBusy = false;
        }
    }
}