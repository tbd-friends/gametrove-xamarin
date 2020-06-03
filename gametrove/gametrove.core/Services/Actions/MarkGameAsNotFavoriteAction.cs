using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;

namespace Gametrove.Core.Services.Actions
{
    public class MarkGameAsNotFavoriteAction : IApiAction<bool>
    {
        private readonly Guid _id;

        public MarkGameAsNotFavoriteAction(Guid id)
        {
            _id = id;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client
                .SendAsync(new HttpRequestMessage(HttpMethod.Delete, "games/favorites")
                {
                    Content = new { GameId = _id }.AsStringContent(Encoding.UTF8)
                })
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}