using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;

namespace tNext.Microservices.Campaign.Api.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : ApiController
    {
        [Route("Ping")]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return tNextResponseCreator.OK("Pong");
        }

        [Route("ClearLocalCache")]
        [HttpGet]
        public HttpResponseMessage ClearLocalCache()
        {
            var result = CacheHelper.Local.ClearAllCachedItems();
            return tNextResponseCreator.OK(result);
        }

        [Route("Health")]
        [HttpGet]
        public HttpResponseMessage Health()
        {
            var healthResult = new List<HealthData>();

            var grayLog = HealthHelper.CheckGrayLog();
            healthResult.Add(grayLog);

            var task1 = Task.Run(() =>
            {
                var configurationServiceHealthData = CheckConfigurationService();
                healthResult.Add(configurationServiceHealthData);
            });
            var task2 = Task.Run(() =>
            {
                var hddHealthData = CheckHDD();
                healthResult.Add(hddHealthData);
            });
            var task3 = Task.Run(() =>
            {
                var ramHealthData = CheckRAM();
                healthResult.Add(ramHealthData);
            });
            var task4 = Task.Run(() =>
            {
                var cpuHealthData = CheckCPU();
                healthResult.Add(cpuHealthData);
            });
            var task5 = Task.Run(() =>
            {
                HealthData parameterServiceCheck = CheckParameterService();
                healthResult.Add(parameterServiceCheck);
            });
            var task6 = Task.Run(() =>
            {
                var databaseHealthData = CheckDatabaseConnection();
                healthResult.Add(databaseHealthData);
            });

            task1.Wait();
            task2.Wait();
            task3.Wait();
            task4.Wait();
            task5.Wait();
            task6.Wait();

            return tNextResponseCreator.OK(healthResult);
        }

        private HealthData CheckHDD()
        {
            try
            {
                PerformanceCounter diskFreeSpace = new PerformanceCounter("LogicalDisk", "% Free Space", "C:");
                double diskspace = diskFreeSpace.NextValue();
                diskspace = (Math.Round(diskspace, 2));
                if (diskspace > 50.00)
                {
                    return new HealthData()
                    {
                        Name = "Harddisk",
                        Status = "OK",
                        Message = "Disc space is greater than %50",
                        ImportanceLevel = "HIGH"
                    };
                }
                else
                {
                    return new HealthData()
                    {
                        Name = "Harddisk",
                        Status = "NOK",
                        Message = "Disc space is smaller than %50",
                        ImportanceLevel = "HIGH"
                    };
                }
            }
            catch (Exception ex)
            {
                return new HealthData()
                {
                    Name = "Harddisk",
                    Status = "NOK",
                    Message = ex.Message,
                    ImportanceLevel = "HIGH"
                };
            }
        }

        private HealthData CheckRAM()
        {
            try
            {
                PerformanceCounter ramCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use", null);
                double ram = ramCounter.NextValue();
                ram = (Math.Round(ram, 2));
                if (ram > 80.00)
                {
                    return new HealthData()
                    {
                        Name = "RAM",
                        Status = "NOK",
                        Message = "Ram usage is greather than %80",
                        ImportanceLevel = "HIGH"
                    };
                }
                else
                {
                    return new HealthData()
                    {
                        Name = "RAM",
                        Status = "OK",
                        Message = "Ram usage is smaller than %80",
                        ImportanceLevel = "HIGH"
                    };
                }
            }
            catch (Exception ex)
            {
                return new HealthData()
                {
                    Name = "RAM",
                    Status = "NOK",
                    Message = ex.Message,
                    ImportanceLevel = "HIGH"
                };
            }
        }

        private HealthData CheckCPU()
        {
            try
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
                double cpu = cpuCounter.NextValue();
                Thread.Sleep(50);
                cpu = cpuCounter.NextValue();
                cpu = (Math.Round(cpu, 2));
                if (cpu < 75.00)
                {
                    return new HealthData()
                    {
                        Name = "CPU",
                        Status = "OK",
                        Message = "CPU percentage is smaller than %75",
                        ImportanceLevel = "HIGH"
                    };
                }
                else
                {
                    return new HealthData()
                    {
                        Name = "CPU",
                        Status = "NOK",
                        Message = "CPU percentage is greater than %75",
                        ImportanceLevel = "HIGH"
                    };
                }
            }
            catch (Exception ex)
            {
                return new HealthData()
                {
                    Name = "CPU",
                    Status = "NOK",
                    Message = ex.Message,
                    ImportanceLevel = "HIGH"
                };
            }
        }

        private HealthData CheckConfigurationService()
        {
            try
            {
                string configURl = $"{ConfigurationManager.AppSettings["ConfigurationMicroserviceUrl"]}/Admin/Ping";
                var restCall = new Common.Core.Rest.RestCall(configURl).Get();
                var result = restCall.SendAndGetResponse();
                var pingResponse = result.Data.ToString();

                return new HealthData()
                {
                    Name = "Configuration Microservice Connection",
                    Status = (pingResponse == "Pong") ? "OK" : "NOK",
                    Message = "Configuration Service is Accessible",
                    ImportanceLevel = "HIGH"
                };
            }
            catch (Exception ex)
            {
                return new HealthData()
                {
                    Name = "Configuration Microservice Connection",
                    Status = "NOK",
                    Message = ex.Message,
                    ImportanceLevel = "HIGH"
                };
            }
        }

        private HealthData CheckParameterService()
        {
            try
            {
                var parameterServiceUrl = ConfigurationHelper.GetConfiguration("tNext.Microservices.Parameter.Api.Url");
                parameterServiceUrl = parameterServiceUrl + "/admin/ping";
                var restCall = new Common.Core.Rest.RestCall(parameterServiceUrl).Get();

                var result = restCall.SendAndGetResponse();

                var response = result.Data.ToString();

                return new HealthData
                {
                    Name = "Parameter Service",
                    ImportanceLevel = "HIGH",
                    Status = (response.ToLower() == "pong") ? "OK" : "NOK",
                    Message = "Parameter Service is Accessible"
                };
            }
            catch (Exception ex)
            {
                return new HealthData()
                {
                    Name = "Parameter Service",
                    ImportanceLevel = "HIGH",
                    Status = "NOK",
                    Message = ex.Message
                };
            }
        }

        private HealthData CheckDatabaseConnection()
        {
            try
            {
                string status = "OK";
                string message = "Database is Accessible";
                string connectionString = ConfigurationHelper.GetConfiguration("TnextDatabaseConnectionStrings");
                connectionString += ConfigurationHelper.GetConfiguration("TnextDatabaseConnectionStringsHealthTimeOut");
                using (var dbCheckConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        dbCheckConnection.Open();
                    }
                    catch (SqlException ex)
                    {
                        status = "NOK";
                        message = ex.Message;
                    }
                    dbCheckConnection.Close();
                }
                return new HealthData
                {
                    Name = "Database Connection Check",
                    ImportanceLevel = "HIGH",
                    Status = status,
                    Message = message
                };
            }
            catch (Exception ex)
            {
                return new HealthData
                {
                    Name = "Database Connection Check",
                    ImportanceLevel = "HIGH",
                    Status = "NOK",
                    Message = ex.Message
                };
            }
        }
    }
}
