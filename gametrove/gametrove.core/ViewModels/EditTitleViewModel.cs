using System;
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

        public Command UpdateTitleCommand { get; set; }

        private readonly APIActionService _api;
        private Guid _id;

        public EditTitleViewModel(Guid gameId)
        {
            _api = DependencyService.Get<APIActionService>();

            UpdateTitleCommand = new Command(async () => await UpdateTitle());

            LoadTitleFromGameId(gameId);
        }

        private async Task UpdateTitle()
        {
            var result = await _api.Execute(new UpdateTitleAction(new TitleModel
            {
                Id = _id,
                Name = Name,
                Subtitle = Subtitle
            }));

            MessagingCenter.Send(this, "Title:Updated", result);
        }

        private void LoadTitleFromGameId(Guid id)
        {
            Task.Run(async () =>
            {
                var title = await _api.Execute(new GetTitleForGameAction(id));

                _id = title.Id;
                Name = title.Name;
                Subtitle = title.Subtitle;
            });
        }
    }
}