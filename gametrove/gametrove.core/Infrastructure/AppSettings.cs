using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gametrove.Core.Infrastructure
{
    public class AppSettings
    {
        public static Settings Configuration { get; }

#if RELEASE
        public static string ConfiguredResourceString = "Gametrove.Core.settings.json";
#elif DEBUG
        public static string ConfiguredResourceString = $"Gametrove.Core.settings.Debug.json";
#endif

        static AppSettings()
        {
            using (var reader =
                new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(ConfiguredResourceString) ??
                                 throw new InvalidOperationException("Application Configuration Unavailable")))
            {
                Configuration = JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd(),
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
        }

        public sealed class Settings
        {
            public string Syncfusion { get; set; }
            public ApiConfiguration Api { get; set; }
            public AuthConfiguration Auth { get; set; }

            public sealed class ApiConfiguration
            {
                public string Url { get; set; }
            }

            public sealed class AuthConfiguration
            {
                public string Domain { get; set; }
                public string ClientId { get; set; }
                public string Audience { get; set; }
            }
        }
    }
}