using Gametrove.Core.Models;
using Gametrove.Core.Services;
using Gametrove.Core.ViewModels;
using Gametrove.Core.Views;
using Xamarin.Forms;

namespace Gametrove.Core.Handlers
{
    public class SearchGameHandler : SearchHandler
    {
        private readonly APIService _service;

        public SearchGameHandler()
        {
            _service = DependencyService.Resolve<APIService>();
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue) || newValue.Length <= 2)
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = _service.SearchForGame(newValue).GetAwaiter().GetResult();
            }
        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            if (item is SearchResultItem result)
            {
                Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    var game = await _service.GetGameById(result.Id);

                    await Shell.Current.Navigation.PushAsync(new GameDetailPage(new GameViewModel(game)));
                });
            }
        }
    }
}