using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace app.Services
{
    public class APIService
    {
        private HttpClient _client;

        public APIService()
        {
            _client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri("http://10.0.2.2:5000")
            };
        }

        public async Task<bool> RegisterNewGame(string name, string description, string code)
        {
            var response =
                await _client.PostAsync("games", new StringContent(new
                {
                    Name = name,
                    Description = description,
                    Code = code
                }.AsJson(), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
    }

    public static class JsonExtensions
    {
        public static string AsJson<T>(this T @object)
        {
            return JsonConvert.SerializeObject(@object,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}