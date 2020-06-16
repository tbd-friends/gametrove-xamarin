using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class GameCopiesViewModel : BaseViewModel
    {
        public Guid Id { get; }
        public ObservableCollection<CopyModel> Copies { get; }

        public Command LoadCopiesCommand { get; }

        private readonly APIActionService _api;

        public GameCopiesViewModel(GameModel model)
        {
            Id = model.Id;
            Copies = new ObservableCollection<CopyModel>();
            LoadCopiesCommand = new Command(async () => await LoadCopies());

            _api = DependencyService.Get<APIActionService>();
        }

        public async Task LoadCopies()
        {
            IsBusy = true;

            var copies = await _api.Execute(new GetCopiesAction(Id));

            Copies.Clear();

            foreach (var copy in copies)
            {
                Copies.Add(copy);
            }

            IsBusy = false;
        }

    }
}