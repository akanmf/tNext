using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using System.Threading.Tasks;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;

namespace tNext.ApiGateway.Api.Services
{
    public class RequestLogService
    {

        public void InsertReqeustLogAsync(RequestLog requestLog)
        {
            var isRequestLogEnable = ConfigurationHelper.GetConfiguration("isDatabaseRequestLoggingEnabled");
            if (string.Compare(isRequestLogEnable, "true", true) == 0 && requestLog.Url != "/admin/ping")
            {
                Task.Run(() => InsertRequestLog(requestLog));
            }
        }

        void InsertRequestLog(RequestLog requestLog)
        {
            try
            {
                var conStr = ConfigurationHelper.GetConfiguration("TnextDatabaseConnectionStrings");
                Database db = new SqlDatabase(conStr);
                var command = db.GetSqlStringCommand(@"
INSERT INTO[dbo].[RequestLog]
           (
           [RequestId]
           ,[Host]
           ,[Url]
           ,[ClientIp]
           ,[RequestHeaders]
           ,[RequestBody]
           ,[ResponseHeaders]
           ,[ResponseBody]
           ,[RequestTime]
           ,[ResponseTime]
           ,[ResponseStatus]
           ,[Description]
           ,[IsSuccess])
     VALUES
           (
            @RequestId
           , @Host
           , @Url
           , @ClientIp
           , @RequestHeaders
           , @RequestBody
           , @ResponseHeaders
           , @ResponseBody
           , @RequestTime
           , @ResponseTime
           , @ResponseStatus
           , @Description
           , @IsSuccess         )");

                db.AddInParameter(command, "@RequestId", DbType.String, requestLog.RequestId);
                db.AddInParameter(command, "@Host", DbType.String, requestLog.Host);
                db.AddInParameter(command, "@Url", DbType.String, requestLog.Url);
                db.AddInParameter(command, "@ClientIp", DbType.String, requestLog.ClientIp);
                db.AddInParameter(command, "@RequestHeaders", DbType.String, requestLog.RequestHeaders);
                db.AddInParameter(command, "@RequestBody", DbType.String, requestLog.RequestBody);
                db.AddInParameter(command, "@ResponseHeaders", DbType.String, requestLog.ResponseHeaders);
                db.AddInParameter(command, "@ResponseBody", DbType.String, requestLog.ResponseBody);
                db.AddInParameter(command, "@RequestTime", DbType.DateTime, requestLog.RequestTime);
                db.AddInParameter(command, "@ResponseTime", DbType.DateTime, requestLog.ResponseTime);
                db.AddInParameter(command, "@ResponseStatus", DbType.String, requestLog.ResponseStatus);
                db.AddInParameter(command, "@Description", DbType.String, requestLog.Description);
                db.AddInParameter(command, "@IsSuccess       ", DbType.Boolean, requestLog.IsSuccess);

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
            }
        }

    }
}