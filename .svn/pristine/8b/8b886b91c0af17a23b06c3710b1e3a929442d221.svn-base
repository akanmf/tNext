using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Model;
using tNext.Microservices.Order.Api.Service;

namespace tNext.Microservices.Order.Api.Controllers
{
    public class OrderController : ApiController
    {
        OrderService _orderservice = new OrderService();


        [Route("{OrderGuid}/Detail")]
        [RequestFilter(customerRequired: true)]
        [HttpGet]
        public HttpResponseMessage GetUserOrderDetails(Guid OrderGuid)
        {
            var response = _orderservice.getPurchaseOrderV2(OrderGuid);
            var trackingNumber = response.purchaseOrdersList.Select(p => p.TrackingNumber).FirstOrDefault();
            var orderdate = response.purchaseOrdersList.Select(k => k.Created).FirstOrDefault();
            string orderyear = Convert.ToDateTime(orderdate).Year.ToString();
            var productid = response.lineItemsList.Select(k => k.ProductId).FirstOrDefault();
            var raporAlanları = _orderservice.getTeknosaRaporAlanları(trackingNumber);
            var responseoracle = _orderservice.getCargoInfo(orderyear + trackingNumber);
            var parameterList = Common.Core.Helpers.ParameterHelper.GetParameter("ALL", string.Empty);

            var deliveryaddress = response.orderAddressesList.Where(m => m.Type == 1).Select(k => new
            {
                ID = response.lineItemsList.Select(l => l.ShippingAddressId).FirstOrDefault(),
                Name = k.Line1 + " " + k.State + "/" + k.City

            }).FirstOrDefault();

            var invoiceaddress = response.purchaseOrdersList.Select(n => new
            {
                ID = n.BillingAddressId,
                Name = response.orderAddressesList.Where(k => k.OrderAddressId == n.BillingAddressId).Select(p => p.Line1 + " " + p.RegionName + "/" + p.City).FirstOrDefault()
            }).FirstOrDefault();

            string cargoShipFirm = string.Empty;
            if (responseoracle.Count > 0)
                cargoShipFirm = responseoracle.Where(x => x.URUN_KODU == response.lineItemsList.Where(y => y.ProductId == x.URUN_KODU)?.FirstOrDefault()?.ProductId)?.FirstOrDefault()?.NAKLIYECI?.ToLower();

            var ShippingInformation = response.lineItemsList.Select(k => new
            {
                StartActionDate = responseoracle.Where(l => l.URUN_KODU == k.ProductId).Select(l => l.ISLEM_SAATI).FirstOrDefault(),
                ShipLogoPath = string.IsNullOrEmpty(cargoShipFirm) ? "http://cdn.teknosa.com/oracle_tarafindan_kargo_sirketi_gelmedi.jpeg" : parameterList.Where(x => x.Group == "ShipmentCompanyLogo" && x.Key == cargoShipFirm).FirstOrDefault().Value,
                ShipName = !string.IsNullOrEmpty(cargoShipFirm) ? cargoShipFirm.ToUpper() + " KARGO" : "-",
                ShipNumber = responseoracle.Where(l => l.URUN_KODU == k.ProductId)?.Select(l => l.TAKIP_NO)?.FirstOrDefault(),
                TrackingPage = string.IsNullOrEmpty(cargoShipFirm) ? string.Empty : parameterList.Where(x => x.Group == "ShipmentCompanyTrackingPage" && x.Key == cargoShipFirm).FirstOrDefault().Value + responseoracle.Where(l => l.URUN_KODU == k.ProductId).Select(l => l.TAKIP_NO).FirstOrDefault(),
                ShipWebAddres = string.IsNullOrEmpty(cargoShipFirm) ? string.Empty : parameterList.Where(x => x.Group == "ShipmentCompanyWebAddress" && x.Key == cargoShipFirm).FirstOrDefault().Value,
                ShipPhoneNumber = string.IsNullOrEmpty(cargoShipFirm) ? string.Empty : parameterList.Where(x => x.Group == "ShipmentCompanyPhoneNumber" && x.Key == cargoShipFirm).FirstOrDefault().Value,
                ShipFaxNumber = string.IsNullOrEmpty(cargoShipFirm) ? string.Empty : parameterList.Where(x => x.Group == "ShipmentCompanyFaxNumber" && x.Key == cargoShipFirm).FirstOrDefault().Value,
                DeliveryPerson = response.orderAddressesList.Where(m => m.OrderAddressId == k.ShippingAddressId.ToUpper()).Select(c => c.FirstName + " " + c.LastName).FirstOrDefault(),
                ShowShippingLogo = string.IsNullOrEmpty(responseoracle.Where(l => l.URUN_KODU == k.ProductId)?.Select(l => l.TAKIP_NO)?.FirstOrDefault()) ? false : true,
                OrderStatusCodeDescription = parameterList.Where(x => x.Group == "OrderStatus" && x.Key == response.purchaseOrdersList.Where(m => m.TrackingNumber == trackingNumber).Select(m => m.Status).FirstOrDefault()).FirstOrDefault().Value,
                LastActionDate = responseoracle.Where(l => l.URUN_KODU == k.ProductId)?.Select(l => l.ISLEM_SAATI)?.FirstOrDefault(),
                ProductID = k.ProductId,
                ProductName = k.DisplayName,
                DeliveryPlaceType = k.StoreId == null ? 1 : 0,
                DeliveryTime = k.StoreId == null ? string.Empty : (response.lineItemsList.Where(x => x.ProductId == k.ProductId)?.Select(y => (y.CCStatusType == 0) && y.StoreId != null ? "Hemen Teslim" : "1-5 Gün"))?.FirstOrDefault(),
                Store = _orderservice.GetStoreInformations(k.StoreId == null ? "0" : k.StoreId.ToString()),
                Quantity = k.Quantity,
                Amount = k.ListPrice,
                DeliveryAddress = new
                {
                    ID = k.ShippingAddressId,
                    Name = response.orderAddressesList.Where(m => m.OrderAddressId == k.ShippingAddressId.ToUpper())?.Select(m => m.Line1 + " " + m.State + "/" + m.City)?.FirstOrDefault()
                }
            });


            var productListDetail = response.lineItemsList.Select(k => new
            {
                ProductCode = k.ProductId,
                ProductName = k.DisplayName,
                ProductQuantity = k.Quantity,
                ProductAmount = k.ListPrice,
                ProductImage = (Convert.ToInt32(k.ProductId) > 140000000 && Convert.ToInt32(k.ProductId) < 141000000) ? $"https://img-teknosa.mncdn.com/TeknosaImg/productImages/93x71/teknogaranti.jpg" : $"https://img-teknosa.mncdn.com/TeknosaImg/productImages/93x71/{k.ProductId}-1-" + GetUserFriendlyUri(k.DisplayName) + ".jpg",
                ShippingInformation = ShippingInformation.FirstOrDefault(s => s.ProductID == k.ProductId),
            });
            var paymentInformation = response.purchaseOrdersList.Select(k => new
            {
                ShippingAmount = k.ShippingTotal,
                CCNumber = raporAlanları.Select(m => string.IsNullOrEmpty(m.saved_cc_number) ? string.Empty : m.saved_cc_number).FirstOrDefault(),
                Installment = raporAlanları.Select(m => m.installmentCount == null ? string.Empty : string.Format("{0} Taksit", m.installmentCount.ToString())).FirstOrDefault(),
                DiscountAmount = raporAlanları.Select(m => m.indirimtutari == null ? 0 : m.indirimtutari).FirstOrDefault(),
                SubTotalAmount = raporAlanları.Select(m => m.banktotalamount + m.indirimtutari - k.ShippingTotal).FirstOrDefault(),
                TotalAmount = raporAlanları.Select(m => m.banktotalamount == null ? 0 : m.banktotalamount).FirstOrDefault(),
                BankName = raporAlanları.Select(m => m.buying_org_name).FirstOrDefault(),
                PaymentType = raporAlanları.Select(a => string.IsNullOrEmpty(a.HavaleBankaBilgileri) ? string.Empty : a.HavaleBankaBilgileri).FirstOrDefault(),
            });

            var purchaseorder = response.purchaseOrdersList.Select(k => new
            {
                ProductList = productListDetail,
                PaymentInformation = paymentInformation.FirstOrDefault(),
                OrderId = k.TrackingNumber,
                DeliveryAddress = deliveryaddress,
                InvoiceAddress = invoiceaddress,
                OrderDate = orderdate,
                Status = k.Status,
                OrderContract = _orderservice.GetOrderContractByOrderID(trackingNumber).OrderContractText

            }).FirstOrDefault();

            return tNextResponseCreator.OK(purchaseorder);
        }

        private string GetUserFriendlyUri(string rawUri)
        {

            if (!String.IsNullOrWhiteSpace(rawUri))
            {
                string underscore = "_";
                rawUri = HttpContext.Current.Server.HtmlDecode(rawUri);
                //Trim.
                while (rawUri.StartsWith(underscore))
                {
                    rawUri = rawUri.Remove(0, 1);
                }

                while (rawUri.EndsWith(underscore))
                {
                    rawUri = rawUri.Substring(0, rawUri.Length - 1);
                }

                //Replacements..
                rawUri = rawUri.ToLowerInvariant()
                    .Replace("ç", "c")
                    .Replace("ş", "s")
                    .Replace("ü", "u")
                    .Replace("ğ", "g")
                    .Replace("&", "ve")
                    .Replace("ö", "o")
                    .Replace("ı", "i")
                    .Replace("İ", "i");

                int maxLength = 50;

                List<string> exceptForList = new List<string>() { "a+", "a++", "a+++", "a++++", "a+++++", "a++++++" };

                if (!exceptForList.Contains(rawUri))
                {
                    rawUri = Regex.Replace(rawUri, @"[^a-z0-9\s_,-]*", "");
                }

                rawUri = Regex.Replace(rawUri, @"[\s-,_]+", " ").Trim();
                rawUri = rawUri.Substring(0, rawUri.Length <= maxLength ? rawUri.Length : maxLength).Trim();
                rawUri = Regex.Replace(rawUri, @"\s", "-");

                String newUri = rawUri;

                return newUri;
            }

            return null;
        }
    }
}