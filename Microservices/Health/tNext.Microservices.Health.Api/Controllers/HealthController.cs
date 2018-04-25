using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Microservices.Health.Api.Models;
using tNext.Microservices.Health.Api.Service;

namespace tNext.Microservices.Health.Api.Controllers
{
    public class HealthController : ApiController
    {
        CategoryCatalogService ccservice = new CategoryCatalogService();

        public HttpResponseMessage Get()
        {
            var servicefail = new ServiceFail();
            Dictionary<string, List<HealthData>> allHealthData = new Dictionary<string, List<HealthData>>();
            Dictionary<string, ServiceFail> AllServiceFail = new Dictionary<string, ServiceFail>();

            var services = GetServiceInformationList();

            foreach (var service in services)
            {
                try
                {
                    List<HealthData> healthData = GetHealthData(service);
                    allHealthData.Add(service.Name, healthData);
                }
                catch (Exception ex)
                {

                    servicefail = new ServiceFail { HealthUrl = service.HealthUrl, Message = "Service is not available:" + (ex.InnerException ?? ex).ToString() };
                    AllServiceFail.Add(service.Name, servicefail);
                }
            }

            var content = new
            {
                allHealthData = allHealthData.Select(s => new { Key = s.Key, List = s.Value }),
                AllServiceFail = AllServiceFail.Select(f => new { Key = f.Key, List = f.Value })
            };

            //TODO: tüm dataları al ve yorumla.

            return tNextResponseCreator.OK(content);
        }


        private List<HealthData> GetHealthData(ServiceInformation service)
        {
            if (service.Protochol == "REST")
            {
                var restCall = new Common.Core.Rest.RestCall(service.HealthUrl).Get();

                var result = restCall.SendAndGetResponse();

                var healthData = JsonConvert.DeserializeObject<List<HealthData>>(result.Data.ToString());

                return healthData;
            }
            else
            {
                return new List<HealthData>();
            }
        }


        private List<ServiceInformation> GetServiceInformationList()
        {
            var serviceList = new List<ServiceInformation>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationHelper.GetConfiguration("TnextDatabaseConnectionStrings");
                SqlCommand command = new SqlCommand(@"tNext_sp_GetHealthData", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                serviceList = dataTable.CreateListFromTable<ServiceInformation>();
            }
            return serviceList;
        }

        [Route("CategoryBooster")]
        public string CategoryBooster()
        {
            try
            {

                List <tNext_sp_GetAllCategoryAndCatalog> allCategoryCatalog = ccservice.GetAllCategoryAndCatalog();
                var productServiceUrl = String.Empty;
                var restCall = String.Empty;
                int successfulCount = 0;

                for (int i = 0; i < allCategoryCatalog.Count; i++)
                {
                    try
                    {
                        productServiceUrl = ConfigurationHelper.GetConfiguration("tNext.Microservices.Product.Api.Url");
                        productServiceUrl = productServiceUrl + "/filtered" + "?catalogName=" + $"{allCategoryCatalog.Select(k => k.Catalog).ElementAt(i)}" + "&categoryName="
                            + $"{allCategoryCatalog.Select(k => k.Category).ElementAt(i)}" + "&productZone=Normal&orderBy=0&startIndex=0&rowCount=20";

                        restCall = new Common.Core.Rest.RestCall(productServiceUrl).Get().SendAndGetResponse().Data.ToString();
                        successfulCount++;
                    }
                    catch (Exception ex)
                    {
                           
                    }
                }

                return $"{allCategoryCatalog.Count} called, {allCategoryCatalog.Count - successfulCount} of them got an errror!";
               
            }
            catch (Exception ex)
            {
                return "Category booster not working!!";
            }
        }

    }
}
