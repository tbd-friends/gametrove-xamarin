using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;

namespace Gametrove.Core.Services.Actions
{
    public class DeleteCopyAction : IApiAction<bool>
    {
        private readonly Guid _gameId;
        private readonly CopyModel _model;

        public DeleteCopyAction(Guid gameId, CopyModel model)
        {
            _gameId = gameId;
            _model = model;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client
                .SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"games/{_gameId}/copies")
                {
                    Content = _model.AsStringContent(Encoding.UTF8)
                })
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}