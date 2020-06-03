using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class SearchForGameAction : IApiAction<IEnumerable<GameModel>>
    {
        private readonly string _term;
        private readonly int? _recentlyAdded;

        public SearchForGameAction(string term, int? recentlyAdded = null)
        {
            _term = term;
            _recentlyAdded = recentlyAdded;
        }

        public async Task<IEnumerable<GameModel>> DoAsync(APIActionService service)
        {
            var response = await service.Client.PostAsync("search/games",
                    new
                    {
                        Text = _term,
                        MostRecentlyAdded = _recentlyAdded
                    }.AsStringContent(Encoding.UTF8))
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<GameModel>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }
    }
}