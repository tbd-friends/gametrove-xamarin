using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetGenreLabelsAction : IApiAction<IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync("genres").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}