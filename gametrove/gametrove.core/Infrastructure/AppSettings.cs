using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gametrove.Core.Infrastructure
{
    public class AppSettings
    {
        public static Settings Configuration { get; }

        static AppSettings()
        {
            using (var reader =
                new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Gametrove.Core.settings.json")))
            {
                Configuration = JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd(),
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
        }

        public class Settings
        {
            public string Scandit { get; set; }
            public string Syncfusion { get; set; }
            public ApiConfiguration Api { get; set; }

            public class ApiConfiguration
            {
                public string Url { get; set; }
            }
        }
    }
}