using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class RegisterNewGameAction : IApiAction<GameModel>
    {
        private readonly string _name;
        private readonly string _subtitle;
        private readonly string _code;
        private readonly Guid _platform;

        public RegisterNewGameAction(string name, string subtitle, string code, Guid platform)
        {
            _name = name;
            _subtitle = subtitle;
            _code = code;
            _platform = platform;
        }

        public GameModel Do(APIActionService service)
        {
            return null;
        }

        public async Task<GameModel> DoAsync(APIActionService service)
        {
            var response =
                await service.Client.PostAsync("games", new StringContent(new
                {
                    Name = _name,
                    Subtitle = _subtitle,
                    Code = _code,
                    Platform = _platform
                }.AsJson(), Encoding.UTF8, "application/json"));

            return !response.IsSuccessStatusCode
                ? null
                : JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync());
        }
    }
}