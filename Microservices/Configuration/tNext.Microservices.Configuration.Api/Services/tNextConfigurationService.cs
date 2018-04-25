using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using tNext.Common.Core;
using tNext.Common.Model;

namespace tNext.Microservices.Configuration.Api.Services
{
    public class tNextConfigurationService
    {
        public List<ConfigurationItem> GetConfigurationFromDB(string applicationName, string environment, string key = null)
        {
            List<ConfigurationItem> ConfigruationRepository = new List<ConfigurationItem>();
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database Db = factory.Create("TNEXT_DB");
            string storedProcName = "tNext_sp_GetConfiguration";
            using (DbCommand sprocCmd = Db.GetStoredProcCommand(storedProcName, applicationName, environment, key))
            {
                var dataSet = Db.ExecuteDataSet(sprocCmd);
                ConfigruationRepository = dataSet.Tables[0].CreateListFromTable<ConfigurationItem>();
            }
            return ConfigruationRepository;
        }


        public string GetValue(string key)
        {
            var list = GetConfigurationFromDB(tNextMicroservice.ApplicationName, tNextMicroservice.Environment, key);
            if (list.Count > 0)
            {
                return list[0].Value;
            }

            throw new KeyNotFoundException($"{key} does not exists in the configuration");
        }

    }
}