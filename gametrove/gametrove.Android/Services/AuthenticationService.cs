using System.Threading.Tasks;
using Auth0.OidcClient;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Model;
using Gametrove.Core.Services.Interfaces;
using gametrove.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace gametrove.Droid.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Auth0Client _auth0Client;

        public AuthenticationService()
        {
            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = AppSettings.Configuration.Auth.Domain,
                ClientId = AppSettings.Configuration.Auth.ClientId
            });
        }

        public AuthenticationResult AuthenticationResult { get; private set; }

        public async Task<AuthenticationResult> Authenticate()
        {
            var auth0LoginResult = await _auth0Client.LoginAsync(new
            {
                audience = AppSettings.Configuration.Auth.Audience,
                scope = "openid email profile"
            });

            AuthenticationResult authenticationResult;

            if (!auth0LoginResult.IsError)
            {
                authenticationResult = new AuthenticationResult()
                {
                    AccessToken = auth0LoginResult.AccessToken,
                    IdToken = auth0LoginResult.IdentityToken,
                    UserClaims = auth0LoginResult.User.Claims
                };
            }
            else
            {
                authenticationResult = new AuthenticationResult(auth0LoginResult.IsError, auth0LoginResult.Error);
            }

            AuthenticationResult = authenticationResult;

            return authenticationResult;
        }
    }
}