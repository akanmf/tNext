using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using tNext.Common.Core.Handlers;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model;

namespace tNext.Common.Core
{
    public class tNextMicroservice
    {
        internal static List<ConfigurationItem> Configuration;

        public static string Environment { get; internal set; }

        public static string ApplicationName
        {
            get { return ConfigurationManager.AppSettings["ApplicationName"]; }
        }

        static tNextMicroservice()
        {
            System.Net.ServicePointManager.SecurityProtocol =
                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            CheckConfigurationFile();

            GetConfigurationFromConfigurationMicroservice();
            SetEnvironment();
            var requestLoging = string.Empty;

            if (ConfigurationHelper.TryGetConfiguration("IsRequestLoggingEnabled", out requestLoging) && string.Compare(requestLoging, "true", true) == 0)
            {
                GlobalConfiguration.Configuration.MessageHandlers.Add(new LoggingMessageHandler());
            }

            tNextLogManager.SetGrayLog();

        }

        private static void CheckConfigurationFile()
        {
            if (ConfigurationManager.AppSettings["ApplicationName"] == null)
            {
                throw new KeyNotFoundException("ApplicationName not found in the configuration file");
            }

            if (ConfigurationManager.AppSettings["Password"] == null)
            {
                throw new KeyNotFoundException("Password not found in the configuration file");
            }

            if (ConfigurationManager.AppSettings["ConfigurationMicroserviceUrl"] == null)
            {
                throw new KeyNotFoundException("ConfigurationMicroserviceUrl not found in the configuration file");
            }

            if (ConfigurationManager.AppSettings["Environment"] == null)
            {
                throw new KeyNotFoundException("Environment not found in the configuration file");
            }
        }

        private static void SetEnvironment()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            switch ((env ?? "").ToUpper())
            {
                case "PROD":
                    Environment = Common.Model.Enums.Environments.Prod;
                    break;
                case "DEV":
                    Environment = Common.Model.Enums.Environments.Dev;
                    break;
                case "LOCAL":
                    Environment = Common.Model.Enums.Environments.Local;
                    break;
                case "TEST":
                    Environment = Common.Model.Enums.Environments.Test;
                    break;
                default:
                    Environment = Common.Model.Enums.Environments.Dev;
                    break;
            }
        }

        public static void Start() { }

        public static void GetConfigurationFromConfigurationMicroservice()
        {
            try
            {
                var configurationServiceUrl = ConfigurationManager.AppSettings["ConfigurationMicroserviceUrl"];

                var restCall = new Common.Core.Rest.RestCall(configurationServiceUrl).Get().IncludingQueryParameter("ApplicationName", ConfigurationManager.AppSettings["ApplicationName"]);

                var result = restCall.SendAndGetResponse();

                var configurationItems = JsonConvert.DeserializeObject<List<ConfigurationItem>>(result.Data.ToString());

                Configuration = configurationItems;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Configurasyon servis erişimde hata", ex);
            }
        }

    }
}
