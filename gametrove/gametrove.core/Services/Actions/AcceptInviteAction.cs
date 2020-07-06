using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;

namespace Gametrove.Core.Services.Actions
{
    public class AcceptInviteAction : IApiAction<bool>
    {
        private readonly string _email;
        private readonly string _token;

        public AcceptInviteAction(string email, string token)
        {
            _email = email;
            _token = token;
        }

        public async Task<bool> DoAsync(APIActionService service)
        {
            var response = await service.Client.PutAsync("users/invite", new
            {
                Email = _email,
                Token = _token
            }.AsStringContent(Encoding.UTF8));

            return response.IsSuccessStatusCode;
        }
    }
}