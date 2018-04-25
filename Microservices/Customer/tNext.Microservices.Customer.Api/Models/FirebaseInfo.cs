using System;

namespace tNext.Microservices.Customer.Api.Models
{
    public class FirebaseInfo
    {
        public string UserId { get; set; }
        public DateTime? SignupDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? FirstOrderDate { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public int TransactionCount { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? AvgOrderValue { get; set; }
    }
}