using System.Threading.Tasks;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Gametrove.Core.Views;
using Xamarin.Forms;

namespace Gametrove.Core.Handlers
{
    public class SearchGameHandler : SearchHandler
    {
        private readonly APIActionService _service;

        public SearchGameHandler()
        {
            _service = DependencyService.Resolve<APIActionService>();
        }

        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue) || newValue.Length <= 2)
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = await _service.Execute(new SearchForGameAction(newValue));
            }
        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            if (item is GameModel result)
            {
                Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    var game = await _service.Execute(new GetGameByIdAction(result.Id));

                    await Shell.Current.Navigation.PushAsync(new GameDetailPage(new GameDetailViewModel(game)));
                });
            }
        }
    }
}