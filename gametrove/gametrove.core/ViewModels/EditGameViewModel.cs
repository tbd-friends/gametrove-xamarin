using System;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class EditGameViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
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
                if (value != _description)
                {
                    _description = value;

                    OnPropertyChanged();
                }
            }
        }

        public Command UpdateGameCommand { get; set; }
        public Command<Guid> LoadGameCommand { get; set; }

        private readonly APIService _api;
        private Guid _id;

        public EditGameViewModel()
        {
            _api = DependencyService.Get<APIService>();

            UpdateGameCommand = new Command(async () => await UpdateGame());
            LoadGameCommand = new Command<Guid>(async gameId => await LoadGameFromApi(gameId));
        }

        private async Task UpdateGame()
        {
            var result = await _api.UpdateGame(new GameModel
            {
                Id = _id,
                Name = Name,
                Description = Description
            });

            MessagingCenter.Send(this, "Game:Updated", result);
        }

        private async Task LoadGameFromApi(Guid id)
        {
            var game = await _api.GetGameById(id);

            _id = id;
            Name = game.Name;
            Description = game.Description;
        }
    }
}