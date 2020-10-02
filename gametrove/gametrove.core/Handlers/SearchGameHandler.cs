using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Gametrove.Core.Views;
using Gametrove.Core.Views.GameDetails;
using Xamarin.Forms;

namespace Gametrove.Core.Handlers
{
    public class SearchGameHandler : SearchHandler
    {
        private readonly APIActionService _service;
        private readonly RecentGamesList _listing;

        public SearchGameHandler()
        {
            _service = DependencyService.Resolve<APIActionService>();

            _listing = DependencyService.Resolve<RecentGamesList>();
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

            if (item is GameSearchModel result)
            {
                Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    var game = await _service.Execute(new GetGameByIdAction(result.Id));

                    await _listing.Track(game);

                    await Shell.Current.Navigation.PushAsync(new GameDetailMainPage(game));
                });
            }
        }
    }
}