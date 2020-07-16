using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Gametrove.Core.Views;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class GameCopiesViewModel : BaseViewModel
    {
        public Guid Id { get; }
        public GameModel Game { get; }
        public ObservableCollection<CopyModel> Copies { get; }

        public Command LoadCopiesCommand { get; }
        public Command EditCopyCommand { get; }
        public Command DeleteCopyCommand { get; }
        public INavigation Navigation { get; set; }
        public IConfirmationService ConfirmationService { get; }

        private readonly APIActionService _api;

        public GameCopiesViewModel(GameModel model)
        {
            Id = model.Id;
            Copies = new ObservableCollection<CopyModel>();
            Game = model;
            LoadCopiesCommand = new Command(async () => await LoadCopies());
            EditCopyCommand = new Command<CopyModel>(async (m) => await EditCopy(m));
            DeleteCopyCommand = new Command<CopyModel>(async (m) => await DeleteCopy(m));

            ConfirmationService = DependencyService.Get<IConfirmationService>();

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

        public async Task EditCopy(CopyModel model)
        {
            await Navigation.PushModalAsync(new EditCopyPage(Id, model));
        }

        public async Task DeleteCopy(CopyModel model)
        {
            if (await ConfirmationService.Confirm("Are you sure you would like to delete this copy?"))
            {
                await _api.Execute(new DeleteCopyAction(Id, model));

                await LoadCopies();
            }
        }
    }
}