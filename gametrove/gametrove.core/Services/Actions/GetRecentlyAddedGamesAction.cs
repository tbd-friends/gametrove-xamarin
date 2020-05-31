using System.Collections.Generic;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetRecentlyAddedGamesAction : IApiAction<IEnumerable<GameModel>>
    {
        public async Task<IEnumerable<GameModel>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"games/last/10").ConfigureAwait(false);

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