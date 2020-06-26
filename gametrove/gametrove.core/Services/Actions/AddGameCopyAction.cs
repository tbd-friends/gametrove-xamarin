using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class AddGameCopyAction : IApiAction<Guid>
    {
        private readonly Guid _gameId;
        private readonly CopyModel _model;

        public AddGameCopyAction(Guid gameId, CopyModel model)
        {
            _gameId = gameId;
            _model = model;
        }

        public async Task<Guid> DoAsync(APIActionService service)
        {
            var response =
                await service.Client.PostAsync($"games/{_gameId}/copies",
                    _model.AsStringContent(Encoding.UTF8));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            }

            return Guid.Empty;
        }
    }
}