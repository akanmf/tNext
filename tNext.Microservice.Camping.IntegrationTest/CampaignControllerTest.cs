using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Linq;
using tNext.Microservices.Campaign.Api.Controllers;


namespace tNext.Microservices.Campaign.IntegrationTest
{
    [TestClass]
    public class CampaignControllerTest
    {
        CampaignController controller;

        [TestInitialize]
        public void InıtTestContainer()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            controller = new CampaignController();
        }

        [TestMethod]
        public void GetCampaignsTest()
        {
            var resp = controller.Get();
            var task = resp.Content.ReadAsStringAsync();
            task.Wait();

            dynamic tnextResp = Json.Decode(task.Result);

            Assert.IsNotNull(tnextResp.data);
            for (int i = 0; i < tnextResp.data.Length; i++)
            {
                Assert.IsNotNull(tnextResp.data[i].Id, "Geçersiz -Id- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].PairId, "Geçersiz -PairId- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].Name, "Geçersiz -Name- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].Description, "Geçersiz -Description- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].HtmlContent, "Geçersiz -HtmlContent- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].Image, "Geçersiz -Image- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "StartDate"), "Geçersiz -StartDate- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "EndDate"), "Geçersiz -EndDate- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "CampaignLink"), "Geçersiz -CampaignLink- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i],"SearchKeyword"), "Geçersiz -SearchKeyword- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "CampaignShowTypeId"), "Geçersiz -CampaignShowTypeId- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "OrderNumber"), "Geçersiz -OrderNumber- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "IsActive"), "Geçersiz -IsActive- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "IsWeb"), "Geçersiz -IsWeb- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "IsMobile"), "Geçersiz -IsMobile- bilgisi.");
            }
        }

        [TestMethod]
        public void GetCampaignMasterTest()
        {
            //TODO : bu respons da link alanı geliyor fakat null olduğu için burada kontrolünü yapamıyoruz.JsonSerializer kontrol edilecek.
            var resp = controller.Get(12);
            var task = resp.Content.ReadAsStringAsync();
            task.Wait();

            dynamic tnextResp = Json.Decode(task.Result);

            Assert.IsNotNull(tnextResp.data);
            for (int i = 0; i < tnextResp.data.Length; i++)
            {
                Assert.IsNotNull(tnextResp.data[i].Id, "Geçersiz -Id- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].PairId, "Geçersiz -PairId- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].Name, "Geçersiz -Name- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].Description, "Geçersiz -Description- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].HtmlContent, "Geçersiz -HtmlContent- bilgisi.");
                Assert.IsNotNull(tnextResp.data[i].Image, "Geçersiz -Image- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "CampaignStartDate"), "Geçersiz -CampaignStartDate- bilgisi.");
                Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "CampaignEndDate"), "Geçersiz -CampaignEndDate- bilgisi.");
                //Assert.IsTrue(IsPropertyExist(tnextResp.data[i], "CampaignLink"), "Geçersiz -CampaignLink- bilgisi.");
            }
        }

        /// <summary>
        /// Nullable Parameter Validation
        /// </summary>
        /// <param name="dynObj"></param>
        /// <param name="name"></param>
        /// <returns>True/False</returns>
        public static bool IsPropertyExist(dynamic dynObj, string name)
        {
            var members = (IEnumerable<string>)dynObj.GetDynamicMemberNames();
            return members.Contains(name);
        }
    }
}
