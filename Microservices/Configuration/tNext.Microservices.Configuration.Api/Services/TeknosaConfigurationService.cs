using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Data;
using tNext.Common.Model;

namespace tNext.Microservices.Configuration.Api.Services
{
    public class TeknosaConfigurationService
    {
        tNextConfigurationService _tNextconfService = new tNextConfigurationService();

        public TeknosaConfigurationItem GetConfigurationByCode(string code)
        {
            var constr = _tNextconfService.GetValue("TeknosaServicesDB");
            Database db = new SqlDatabase(constr);

            string query = @"SELECT 
							C.*,
							L.LibParameterId as 'ValueType.Id',
							L.Code as 'ValueType.Code',
							L.Name as 'ValueType.Name',
							L.IsActive as 'ValueType.IsActive'
							FROM
							[Application].[Configuration] C WITH(NOLOCK)
							JOIN [Application].[LibParameter] L WITH(NOLOCK) ON C.ValueTypeId = L.LibParameterId
							WHERE C.Code=@ParameterCode";
            var cmd = db.GetSqlStringCommand(query);
            db.AddInParameter(cmd, "@ParameterCode", DbType.AnsiString, code);

            var data = db.ExecuteDataSet(cmd).Tables[0];

            if (data != null && data.Rows.Count > 0)
            {
                var c = data.Rows[0].CreateItemFromRow<TeknosaConfigurationItem>();
                return c;
            }

            throw new KeyNotFoundException($"{code} does not exists in the TeknosaConfiguration");

        }
    }
}