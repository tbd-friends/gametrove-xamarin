using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class RegisterGameCopyAction : IApiAction<Guid>
    {
        private readonly Guid _gameId;
        private readonly string[] _tags;
        private readonly decimal? _cost;
        private readonly DateTime? _purchased;

        public RegisterGameCopyAction(Guid gameId, string[] tags, decimal? cost, DateTime? purchased)
        {
            _gameId = gameId;
            _tags = tags;
            _cost = cost;
            _purchased = purchased;
        }

        public async Task<Guid> DoAsync(APIActionService service)
        {
            var response =
                await service.Client.PostAsync($"copies/{_gameId}", new StringContent(new
                {
                    Tags = _tags,
                    Cost = _cost,
                    Purchased = _purchased
                }.AsJson(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            }

            return Guid.Empty;
        }
    }
}