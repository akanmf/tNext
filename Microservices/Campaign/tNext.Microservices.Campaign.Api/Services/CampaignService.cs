using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using tNext.Common.Core.Helpers;
using tNext.Microservices.Campaign.Api.Model;

namespace tNext.Microservices.Campaign.Api.Services
{
    public class CampaignService
    {
        public List<UX_CampaignMaster> GetCampaigns(bool isWeb, bool isMobile)
        {
            var conStr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_UX_GetCampaigns]");
            db.AddInParameter(command, "@IsWeb", DbType.Boolean, isWeb);
            db.AddInParameter(command, "@IsMobile", DbType.Boolean, isMobile);
            db.AddInParameter(command, "@IsActive", DbType.Boolean, true);
            var ds = db.ExecuteDataSet(command);
            var resp = new List<UX_CampaignMaster>();
            if (ds != null && ds.Tables[0] != null)
            {
                resp = ds.Tables[0].CreateListFromTable<UX_CampaignMaster>();
                if (resp != null && resp.Count > 0)
                {
                    foreach (var item in resp)
                    {
                        item.Image = "http:" + item.Image;
                    }
                }
            }
            return resp;
        }

        public UX_CampaignMaster GetCampaignMaster(int CapaignMasterId)
        {
            var constr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database db = new SqlDatabase(constr);
            var command = db.GetSqlStringCommand($@"SELECT * FROM [Application].UX_CampaignMaster with (nolock) WHERE 
                    Id=@Id");
            db.AddInParameter(command, "@Id", DbType.Int32, CapaignMasterId);

            var dataset = db.ExecuteDataSet(command);

            var result = dataset.Tables[0].Rows[0].CreateItemFromRow<UX_CampaignMaster>();

            return result;
        }


    }
}