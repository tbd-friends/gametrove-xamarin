using System;
using System.Net.Http;
using System.Threading.Tasks;

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
                .DeleteAsync($"games/favorite/{_id}")
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}