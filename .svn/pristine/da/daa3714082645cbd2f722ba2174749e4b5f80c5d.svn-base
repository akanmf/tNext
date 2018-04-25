using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using tNext.Common.Core.Model;
using tNext.Microservices.Order.Api.Controllers;

namespace tNext.Microservices.Order.IntegrationTest
{
    [TestClass]
    public class AdminControllerTests
    {
        AdminController adminController = new AdminController();

        [TestMethod]
        public void OrderPingTest()
        {
            var resp = adminController.Ping();

            var task = resp.Content.ReadAsStringAsync();
            task.Wait();

            var tnextResp = JsonConvert.DeserializeObject<tNextResponse>(task.Result);

            Assert.IsTrue(tnextResp.Data.ToString() == "Pong");
        }
    }
}