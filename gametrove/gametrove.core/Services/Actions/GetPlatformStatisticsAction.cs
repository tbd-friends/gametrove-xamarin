using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetPlatformStatisticsAction : IApiAction<IEnumerable<PlatformStatistic>>
    {
        public async Task<IEnumerable<PlatformStatistic>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"platforms/summary").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<PlatformStatistic>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }
    }
}