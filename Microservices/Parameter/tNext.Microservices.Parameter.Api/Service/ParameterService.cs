using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using tNext.Common.Core.Helpers;
using tNext.Common.Model;

namespace tNext.Microservices.Parameter.Api.Service
{
    public class ParameterService
    {
        public List<ParameterItem> GetParameters(string group, string key)
        {
            List<ParameterItem> ParameterRepository = new List<ParameterItem>();
            string connectionString = ConfigurationHelper.GetConfiguration("TnextDatabaseConnectionStrings");
            Database Db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);

            using (DbCommand sprocCmd = Db.GetStoredProcCommand("tNext_sp_GetParameter", group, key))
            {
                var dataSet = Db.ExecuteDataSet(sprocCmd);
                ParameterRepository = dataSet.Tables[0].CreateListFromTable<ParameterItem>();
            }
            return ParameterRepository;
        }
    }
}