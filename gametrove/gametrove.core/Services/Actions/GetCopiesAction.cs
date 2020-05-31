using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetCopiesAction : IApiAction<IEnumerable<CopyModel>>
    {
        private readonly Guid _gameId;

        public GetCopiesAction(Guid gameId)
        {
            _gameId = gameId;
        }

        public async Task<IEnumerable<CopyModel>> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"copies/{_gameId}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<CopyModel>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }
    }
}