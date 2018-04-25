using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using tNext.Common.Core.Helpers;
using tNext.Microservices.Environment.Api.Models;

namespace tNext.Microservices.Environment.Api.Service
{
    public class AgreementsService
    {
        public Agreement GetConfidentialityAgreement()
        {
            var conStr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_GetItemFromDictionary]");
            db.AddInParameter(command, "@Key", DbType.String, "confidentiality-agreement");


            Agreement agreement = new Agreement();
            agreement.Name = "Confidentiality Agreement";
            agreement.Text = db.ExecuteScalar(command).ToString();

            return agreement;
        }
    }
}