using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class VerifyUserAction : IApiAction<bool>
    {
        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync($"users/verification");

            return response.IsSuccessStatusCode;
        }
    }
}