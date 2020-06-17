using System;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class UpdateCopyAction : IApiAction<CopyModel>
    {
        private readonly Guid _gameId;
        private readonly CopyModel _model;

        public UpdateCopyAction(Guid gameId, CopyModel model)
        {
            _gameId = gameId;
            _model = model;
        }

        public async Task<CopyModel> DoAsync(APIActionService service)
        {
            var response = await service.Client.PutAsync($"games/{_gameId}/copies",
                    _model.AsStringContent(Encoding.UTF8))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<CopyModel>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}