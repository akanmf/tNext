using System;

namespace tNext.Microservices.Order.Api.Model
{
    public class LN_Shipments
    {
        public Guid ShipmentId { get; set; }
        public Guid OrderGroupId { get; set; }
        public Guid ShippingMethodId { get; set; }
        public string ShippingAddressId { get; set; }
        public decimal ShipmentTotal { get; set; }
        public decimal ShippingDiscountAmount { get; set; }
        public string ShippingMethodName { get; set; }
        public int ShippingMethodType { get; set; }
    }
}