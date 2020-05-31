using System.Collections.Generic;
using System.Threading.Tasks;
using Gametrove.Core.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class SearchForGameAction : IApiAction<IEnumerable<SearchResultItem>>
    {
        private readonly string _term;

        public SearchForGameAction(string term)
        {
            _term = term;
        }

        public async Task<IEnumerable<SearchResultItem>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"search/games?text={_term}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<SearchResultItem>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }
    }
}