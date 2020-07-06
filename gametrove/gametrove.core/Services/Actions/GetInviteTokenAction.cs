using System.Threading.Tasks;

namespace Gametrove.Core.Services.Actions
{
    public class GetInviteTokenAction : IApiAction<string>
    {
        public async Task<string> DoAsync(APIActionService service)
        {
            var response = await service.Client.GetAsync("users/invite");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }
    }
}