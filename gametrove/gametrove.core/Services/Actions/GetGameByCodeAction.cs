using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetGameByCodeAction : IApiAction<GameModel>
    {
        private readonly string _code;

        public GetGameByCodeAction(string code)
        {
            _code = code;
        }

        public async Task<GameModel> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"games/codes/{_code}").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}