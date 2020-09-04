using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;

namespace Gametrove.Core.Services.Actions
{
    public class ToggleCoverArtAction : IApiAction<bool>
    {
        private readonly Guid _imageId;

        public ToggleCoverArtAction(Guid imageId)
        {
            _imageId = imageId;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client
                .SendAsync(new HttpRequestMessage(HttpMethod.Post, $"images/coverart")
                {
                    Content = new { ImageId = _imageId }.AsStringContent(Encoding.UTF8)
                })
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

    }
}