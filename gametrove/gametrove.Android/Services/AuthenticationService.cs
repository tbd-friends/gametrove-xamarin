using System.Threading.Tasks;
using Auth0.OidcClient;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Model;
using Gametrove.Core.Services.Interfaces;
using gametrove.Droid.Services;
using IdentityModel.OidcClient.Browser;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace gametrove.Droid.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string RefreshToken = "RefreshToken";
        private readonly Auth0Client _auth0Client;

        public AuthenticationService()
        {
            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = AppSettings.Configuration.Auth.Domain,
                ClientId = AppSettings.Configuration.Auth.ClientId,
                Scope = "openid email profile offline_access",
                LoadProfile = true
            });
        }

        public async Task<bool> ShouldRefresh()
        {
            return !string.IsNullOrEmpty(await SecureStorage.GetAsync(RefreshToken));
        }

        public async Task<AuthenticationResult> Refresh()
        {
            string refreshToken = await SecureStorage.GetAsync(RefreshToken);

            if (!string.IsNullOrEmpty(refreshToken))
            {
                var refreshResult = await _auth0Client.RefreshTokenAsync(refreshToken);

                if (!refreshResult.IsError)
                {
                    var authenticationResult = new AuthenticationResult()
                    {
                        AccessToken = refreshResult.AccessToken,
                        IdToken = refreshResult.IdentityToken,
                    };

                    await SecureStorage.SetAsync(RefreshToken, refreshResult.RefreshToken);

                    return authenticationResult;
                }
            }

            return null;
        }

        public async Task<AuthenticationResult> Authenticate()
        {
            var auth0LoginResult = await _auth0Client.LoginAsync(new
            {
                audience = AppSettings.Configuration.Auth.Audience,
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

                await SecureStorage.SetAsync(RefreshToken, auth0LoginResult.RefreshToken);
            }
            else
            {
                authenticationResult = new AuthenticationResult(auth0LoginResult.IsError, auth0LoginResult.Error);
            }

            return authenticationResult;
        }

        public async Task<bool> Logout()
        {
            SecureStorage.Remove(RefreshToken);

            var result = await _auth0Client.LogoutAsync();

            return result == BrowserResultType.Success;
        }
    }
}