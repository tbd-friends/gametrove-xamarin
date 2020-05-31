using System;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetGameByIdAction : IApiAction<GameModel>
    {
        private readonly Guid _id;

        public GetGameByIdAction(Guid id)
        {
            _id = id;
        }

        public async Task<GameModel> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"games/{_id}").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}