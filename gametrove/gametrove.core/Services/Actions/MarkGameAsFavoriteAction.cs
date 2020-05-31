using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gametrove.Core.Services.Actions
{
    public class MarkGameAsFavoriteAction : IApiAction<bool>
    {
        private readonly Guid _id;

        public MarkGameAsFavoriteAction(Guid id)
        {
            _id = id;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client.PostAsync($"games/favorite/{_id}", new StringContent(string.Empty))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}