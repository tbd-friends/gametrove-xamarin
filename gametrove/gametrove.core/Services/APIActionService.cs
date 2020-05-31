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
                BaseAddress = new Uri(AppSettings.Configuration.Api.Url)
            };
        }

        public async Task<TResult> Execute<TResult>(IApiAction<TResult> action)
        {
            return await CheckIfICanUseTheInternet() == PermissionStatus.Granted ? await action.DoAsync(this) : default;
        }

        private async Task<PermissionStatus> CheckIfICanUseTheInternet()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.NetworkState>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.NetworkState>();
            }

            return status;
        }
    }
}