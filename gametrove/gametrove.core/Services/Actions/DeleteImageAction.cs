using System.Net.Http;
using System.Threading.Tasks;

namespace Gametrove.Core.Services.Actions
{
    public class DeleteImageAction : IApiAction<bool>
    {
        private readonly string _url;

        public DeleteImageAction(string url)
        {
            _url = url;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client
                .SendAsync(new HttpRequestMessage(HttpMethod.Delete, _url))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}