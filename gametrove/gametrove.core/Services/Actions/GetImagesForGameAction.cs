using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetImagesForGameAction : IApiAction<IEnumerable<ImageModel>>
    {
        private readonly Guid _id;

        public GetImagesForGameAction(Guid id)
        {
            _id = id;
        }

        public async Task<IEnumerable<ImageModel>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"games/{_id}/images").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<ImageModel>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }
    }
}