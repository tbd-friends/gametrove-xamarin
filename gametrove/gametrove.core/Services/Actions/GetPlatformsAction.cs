using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetPlatformsAction : IApiAction<IEnumerable<PlatformModel>>
    {
        public async Task<IEnumerable<PlatformModel>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync("platforms").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<IEnumerable<PlatformModel>>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}
