using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetImagesForGameAction : IApiAction<IEnumerable<string>>
    {
        private readonly Guid _id;

        public GetImagesForGameAction(Guid id)
        {
            _id = id;
        }

        public async Task<IEnumerable<string>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"games/{_id}/images").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync());

                return results.Select(i => $"{i}?size=Large");
            }

            return null;
        }
    }
}