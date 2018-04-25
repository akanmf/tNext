using System;

namespace tNext.Microservices.Order.Api.Model
{
    public class LN_PurchaseOrders
    {
        public Guid OrderGroupId { get; set; }
        public string Name { get; set; }
        public Guid SoldToId { get; set; }
        public string SoldToAddressId { get; set; }
        public string BillingAddressId { get; set; }
        public int LineItemCount { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
    }
}