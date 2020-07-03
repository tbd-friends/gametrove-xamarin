using System;
using System.Net.Http;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Xamarin.Essentials;

namespace Gametrove.Core.Services
{

    public class APIActionService
    {
        private HttpClient _client;
        public HttpClient Client => _client;

        private void Initialize()
        {
            _client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(AppSettings.Configuration.Api.Url),
            };

            _client.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {Preferences.Get(AppPreferences.IdentityToken, "invalid")}");
        }

        public async Task<TResult> Execute<TResult>(IApiAction<TResult> action)
        {
            if (_client == null)
            {
                Initialize();
            }

            return await action.DoAsync(this);
        }
    }
}