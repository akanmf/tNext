using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Microservices.Customer.Api.Models;

namespace tNext.Microservices.Customer.Api.Services
{
    public class CustomerService
    {
        public UserObject GetUserByDeviceIDAndToken(string deviceId, string token)
        {

            var conStr = ConfigurationHelper.GetConfiguration("Teknosa_MSCS_Profiles");
            var db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tnext_sp_GetUserByDeviceAndToken]");

            db.AddInParameter(command, "@DeviceID", DbType.String, deviceId);
            db.AddInParameter(command, "@Token", DbType.String, token);
            var dataset = db.ExecuteDataSet(command);

            if (dataset.Tables[0].Rows.Count != 0)
            {
                return dataset.Tables[0].Rows[0].CreateItemFromRow<UserObject>();
            }
            else
            {
                return null;
            }
        }

        public String InsertDeviceInformations(string userID, string PlatformType, string operatingSystem, string operatingSystemVersion, string deviceManufacturer, string deviceModel, string pushNotificationToken)
        {
            var conStr = ConfigurationHelper.GetConfiguration("Teknosa_MSCS_Profiles");
            Database db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tNext_sp_InsertOrUpdateUserDevices]");
            db.AddInParameter(command, "@UserID", DbType.String, userID);
            db.AddInParameter(command, "@PlatformType", DbType.String, PlatformType);
            db.AddInParameter(command, "@OperatingSystem", DbType.String, operatingSystem);
            db.AddInParameter(command, "@OperatingSystemVersion", DbType.String, operatingSystemVersion);
            db.AddInParameter(command, "@DeviceManufacturer", DbType.String, deviceManufacturer);
            db.AddInParameter(command, "@DeviceModel", DbType.String, deviceModel);
            db.AddInParameter(command, "@PushNotificationToken", DbType.String, pushNotificationToken);


            var ds = db.ExecuteDataSet(command);

            return null;
        }

        public FirebaseInfo GetFirebaseInfo(string userID)
        {
            var fireBase = new FirebaseInfo();
            var conStr = ConfigurationHelper.GetConfiguration("TeknosaServicesDB");
            var db = new SqlDatabase(conStr);
            var command = db.GetStoredProcCommand("[dbo].[tnext_sp_GetUserFirebaseInfo]");

            db.AddInParameter(command, "@userId", DbType.String, userID);
            var dataset = db.ExecuteDataSet(command);

            if (dataset != null && dataset.Tables[0].Rows.Count != 0)
            {
                fireBase = dataset.Tables[0].Rows[0].CreateItemFromRow<FirebaseInfo>();
            }
            return fireBase;
        }

    }
}