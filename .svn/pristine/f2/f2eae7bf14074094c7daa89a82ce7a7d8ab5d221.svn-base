using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Core.Model
{
    /// <summary>
    /// Default serializer settings
    /// </summary>
    public static class DefaultSerializerSettings
    {
        static DefaultSerializerSettings()
        {
            Settings = new JsonSerializerSettings()
            {
                // ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateParseHandling = DateParseHandling.DateTime,
                DateFormatString = "o",
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include
            };
            Settings.Converters.Add(new StringEnumConverter()
            {
                CamelCaseText = true
            });
        }

        /// <summary>
        /// Settings
        /// </summary>
        public static JsonSerializerSettings Settings { get; set; }

    }
}
