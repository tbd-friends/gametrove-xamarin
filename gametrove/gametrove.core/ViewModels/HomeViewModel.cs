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

        public ObservableCollection<GameModel> Games { get; set; }
        public Command LoadItemsCommand { get; set; }

        private readonly APIService _api;

        public HomeViewModel()
        {
            Games = new ObservableCollection<GameModel>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

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

        private async Task ExecuteLoadItemsCommand()
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