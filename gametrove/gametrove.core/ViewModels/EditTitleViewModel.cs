using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class EditTitleViewModel : BaseViewModel
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

        private string _subtitle;

        public string Subtitle
        {
            get => _subtitle;
            set
            {
                if (value != _subtitle)
                {
                    _subtitle = value;

                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Genres { get; set; }

        public Command UpdateTitleCommand { get; set; }

        private readonly APIActionService _api;
        private Guid _id;
        private Guid _gameId;

        public EditTitleViewModel(Guid gameId)
        {
            _gameId = gameId;
            _api = DependencyService.Get<APIActionService>();

            Genres = new ObservableCollection<string>();

            UpdateTitleCommand = new Command(async () => await UpdateTitle());
        }

        private async Task UpdateTitle()
        {
            var result = await _api.Execute(new UpdateTitleAction(new TitleModel
            {
                Id = _id,
                Name = Name,
                Subtitle = Subtitle,
                Genres = Genres
            }));

            MessagingCenter.Send(this, "Title:Updated", result);
        }

        public async Task LoadTitleFromGame()
        {
            var title = await _api.Execute(new GetTitleForGameAction(_gameId));

            _id = title.Id;
            Name = title.Name;
            Subtitle = title.Subtitle;

            Genres.Clear();

            foreach (var genre in title.Genres)
            {
                Genres.Add(genre);
            }
        }
    }
}