using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using tNext.Common.Model;

namespace tNext.Common.Core.Helpers
{
    public class ConfigurationHelper
    {
        public static bool TryGetConfiguration(string key, out string value)
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                value=ConfigurationManager.AppSettings[key].ToString();
                return true;
            }
            var configItem = tNextMicroservice.Configuration.Where(c => c.Key == key && c.Environment.ToUpper() == tNextMicroservice.Environment.ToUpper()).FirstOrDefault();
            if (configItem == null)
            {
                value = null;
                return false;
            }
            value = configItem.Value;
            return true;
        }

        public static string GetConfiguration(string key)
        {
            string value = string.Empty;
            if (TryGetConfiguration(key,out value))
            {
                return value;
            }

            throw new KeyNotFoundException($"{key} not found in the configuration repository");
        }

        public static IEnumerable<ConfigurationItem> GetConfigurationGroup(string groupName)
        {
            var configItems = tNextMicroservice.Configuration.Where(c => c.Group == groupName && c.Environment == tNextMicroservice.Environment);
            return configItems;
        }
    }
}
