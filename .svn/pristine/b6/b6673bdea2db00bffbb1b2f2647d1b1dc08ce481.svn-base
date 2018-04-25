using System;
using tNext.Common.Core.Model;

namespace tNext.Common.Core.Helpers
{
    public class HealthHelper
    {
        public static HealthData CheckGrayLog()
        {
            var healthData = new HealthData
            {
                Name = "Gray Log",
                ImportanceLevel = "LOW",

            };

            try
            {
                tNextLogManager.Log("TEST");
                healthData.Status = "OK";
                healthData.Message = "Log Server is Available";
            }
            catch (Exception ex)
            {
                healthData.Status = "NOK";
                healthData.Message = ex.ToString();
            }

            return healthData;
        }
    }
}
