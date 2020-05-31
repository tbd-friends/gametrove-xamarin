using System;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetTitleForGameAction : IApiAction<TitleModel>
    {
        private readonly Guid _id;

        public GetTitleForGameAction(Guid id)
        {
            _id = id;
        }

        public async Task<TitleModel> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"games/{_id}/title").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<TitleModel>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}