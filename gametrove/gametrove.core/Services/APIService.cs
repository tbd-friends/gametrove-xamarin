using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Models;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Gametrove.Core.Services
{
    public class APIService
    {
        private readonly HttpClient _client;

        public APIService()
        {
            _client = new HttpClient(new HttpClientHandler() { })
            {
                BaseAddress = new Uri(AppSettings.Configuration.Api.Url)
            };
        }

        public async Task<GameModel> RegisterNewGame(string name, string description, string code, Guid platform)
        {
            await CheckIfICanUseTheInternet();

            var response =
                await _client.PostAsync("games", new StringContent(new
                {
                    Name = name,
                    Description = description,
                    Code = code,
                    Platform = platform
                }.AsJson(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync());

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<PlatformModel>> GetPlatforms()
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"platforms").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<IEnumerable<PlatformModel>>(await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<GameModel> GetGameById(Guid id)
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"games/{id}").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<GameModel> GetGameByCode(string code)
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"games/codes/{code}").ConfigureAwait(false);

            return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync())
                    : null;
        }

        public async Task<GameModel> UpdateGame(GameModel model)
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.PutAsync($"games/{model.Id}",
                    model.AsStringContent(Encoding.UTF8))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<GameModel>(await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<IEnumerable<SearchResultItem>> SearchForGame(string text)
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"search/games?text={text}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<SearchResultItem>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }

        public async Task<IEnumerable<GameModel>> GetRecentlyAddedGames()
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"games/last/10").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<GameModel>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
        }

        public async Task<bool> UploadImageForGame(Guid id, Stream image, string fileName)
        {
            HttpContent fileStreamContent = new StreamContent(image);

            fileStreamContent.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = fileName
                };

            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);

                var response = await _client.PostAsync($"images/{id}", formData);

                return response.IsSuccessStatusCode;
            }
        }

        public async Task<IEnumerable<string>> GetImagesForGame(Guid id)
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"games/images/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync());

                return results.Select(i => $"{i}?size=Large");
            }

            return null;
        }

        public async Task<Guid> RegisterGameCopy(Guid gameId, string[] tags, decimal? cost, DateTime? purchased)
        {
            await CheckIfICanUseTheInternet();

            var response =
                await _client.PostAsync($"copies/{gameId}", new StringContent(new
                {
                    Tags = tags,
                    Cost = cost,
                    Purchased = purchased
                }.AsJson(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            }

            return Guid.Empty;
        }

        public async Task<IEnumerable<CopyModel>> GetCopies(Guid gameId)
        {
            await CheckIfICanUseTheInternet();

            var response = await _client.GetAsync($"copies/{gameId}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var results =
                    JsonConvert.DeserializeObject<IEnumerable<CopyModel>>(await response.Content.ReadAsStringAsync());

                return results;
            }

            return null;
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