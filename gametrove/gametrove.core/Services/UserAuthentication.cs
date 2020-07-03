using Gametrove.Core.Infrastructure;
using Gametrove.Core.Model;
using Xamarin.Essentials;

namespace Gametrove.Core.Services
{
    public class UserAuthentication
    {
        private static AuthenticationResult _authentication;

        public bool IsValid => _authentication != null;
        public AuthenticationResult Authentication => _authentication;

        public void Initialize(AuthenticationResult result)
        {
            _authentication = result;

            Preferences.Set(AppPreferences.IdentityToken, result.IdToken);
            Preferences.Set(AppPreferences.AccessToken, result.AccessToken);
        }
    }
}