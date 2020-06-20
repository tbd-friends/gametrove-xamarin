using System;
using System.Net.Http;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Xamarin.Essentials;

namespace Gametrove.Core.Services
{

    public class APIActionService 
    {
        public HttpClient Client { get; }

        public APIActionService()
        {
            Client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(AppSettings.Configuration.Api.Url),
            };

            Client.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {Preferences.Get(AppPreferences.IdentityToken, "invalid")}");
        }

        public async Task<TResult> Execute<TResult>(IApiAction<TResult> action)
        {
            return await action.DoAsync(this);
        }
    }
}