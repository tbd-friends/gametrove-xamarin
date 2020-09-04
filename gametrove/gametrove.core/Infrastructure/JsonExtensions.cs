using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gametrove.Core.Infrastructure
{
    public static class JsonExtensions
    {
        public static StringContent AsStringContent<T>(this T @object, Encoding encoding, string mediaType = "application/json") 
            where T : class
        {
            StringContent result;

            switch (mediaType)
            {
                case "application/json":
                default:
                    result = new StringContent(@object.AsJson(), encoding, mediaType);
                    break;
            }

            return result;
        }
        public static string AsJson<T>(this T @object)
        {
            return JsonConvert.SerializeObject(@object,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}