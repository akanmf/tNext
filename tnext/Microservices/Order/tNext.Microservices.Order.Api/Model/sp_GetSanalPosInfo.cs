using System;

namespace tNext.Microservices.Order.Api.Model
{
    public class sp_GetSanalPosInfo
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string OrderId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Installment { get; set; }
        public string Bank { get; set; }
        public string ChargeType { get; set; }
        public string CardNumber { get; set; }
        public string Hash { get; set; }
        public string Retval { get; set; }
        public string RefNo { get; set; }
        public string Err { get; set; }
        public string ErrMsg { get; set; }
        public string ExceptionMsg { get; set; }
        public string ServerIp { get; set; }
        public string AuthCode { get; set; }
        public int? Bloke { get; set; }
        public string Oid { get; set; }
        public decimal? Puan { get; set; }
        public bool? Is3D { get; set; }
        public string _3DResponse { get; set; }
        public bool? IsBKM { get; set; }
        public bool? IsPayU { get; set; }
        public bool IsPayPal { get; set; }
        public bool IsIngTeknoKredi { get; set; }
        public bool? IsOneClick { get; set; }
        public string FraudStatus { get; set; }
        public int? FraudScore { get; set; }
    }

}