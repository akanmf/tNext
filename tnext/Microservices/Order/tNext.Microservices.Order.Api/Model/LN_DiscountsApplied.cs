using System;

namespace tNext.Microservices.Order.Api.Model
{
    public class LN_DiscountsApplied
    {
        public Guid OrderGroupId { get; set; }
        public Guid DiscountId { get; set; }
        public Guid LineItemId { get; set; }
        public DateTime LastModified { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DiscountLevel { get; set; }
        public int DiscountType { get; set; }
        public int ValueType { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountName { get; set; }
        public string PromoCode { get; set; }
        public string DiscountDescription { get; set; }
    }
}