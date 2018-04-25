using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using tNext.Common.Core.Helpers;
using tNext.Microservices.Health.Api.Models;

namespace tNext.Microservices.Health.Api.Service
{
    public class CategoryCatalogService
    {
        public List<tNext_sp_GetAllCategoryAndCatalog> GetAllCategoryAndCatalog()
        {
            List<tNext_sp_GetAllCategoryAndCatalog> CategoryCatalogRepository = new List<tNext_sp_GetAllCategoryAndCatalog>();
            string connectionString = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database Db = new SqlDatabase(connectionString);

            using (DbCommand sprocCmd = Db.GetStoredProcCommand("[dbo].[tNext_sp_GetAllCategoryAndCatalog]"))
            {
                var dataSet = Db.ExecuteDataSet(sprocCmd);
                CategoryCatalogRepository = dataSet.Tables[0].CreateListFromTable<tNext_sp_GetAllCategoryAndCatalog>();
            }

            return CategoryCatalogRepository;
        }
        

    }
}