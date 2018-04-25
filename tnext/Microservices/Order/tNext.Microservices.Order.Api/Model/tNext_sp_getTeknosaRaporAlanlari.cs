using System;

namespace tNext.Microservices.Order.Api.Model
{
    public class tNext_sp_getTeknosaRaporAlanlari
    {
        public string TrackingNumber { get; set; }
        public string saved_cc_number { get; set; }
        public string saved_cc_expiration { get; set; }
        public string cc_auth_number { get; set; }
        public string cc_posref_no { get; set; }
        public string cc_type { get; set; }
        public string buying_org_name { get; set; }
        public string posbankcode { get; set; }
        public string ceptelefonu { get; set; }
        public string il { get; set; }
        public string sirketfaturasi { get; set; }
        public string sirketadi { get; set; }
        public string kredikartisahibiadisoyadi { get; set; }
        public bool? faturaadresi { get; set; }
        public string hediyecekleri { get; set; }
        public string orderleveldiscounts { get; set; }
        public decimal? banktotalamount { get; set; }
        public string HavaleBankaBilgileri { get; set; }
        public bool? Is3D { get; set; }
        public string RemoteIp { get; set; }
        public decimal? indirimtutari { get; set; }
        public decimal? hediyecekitutari { get; set; }
        public string RedAciklama { get; set; }
        public DateTime? OrderSubmittedDate { get; set; }
        public string MagazadanTeslimat { get; set; }
        public string UserLogonName { get; set; }
        public DateTime? OrderCreateDate { get; set; }
        public string affiliatevalue { get; set; }
        public decimal? lufthansatutar { get; set; }
        public string ProductID_NotWinPromocode { get; set; }
        public decimal? KurumsalHediyeCekiTutari { get; set; }
        public string KurumsalHediyeCeki { get; set; }
        public decimal? TeknosaKartIndirimTutari { get; set; }
        public DateTime? OrderApprovedDate { get; set; }
        public string CallCenterUser { get; set; }
        public int? installmentCount { get; set; }
        public string TeknosaKartPuanCek { get; set; }
        public decimal? TeknosaKartPuanTutarUsed { get; set; }
        public bool? IsBKMExpress { get; set; }
        public string PlatformType { get; set; }
        public bool? IsPayU { get; set; }
        public bool? IsTaksitliCek { get; set; }
        public string DeliveryAddressID { get; set; }
        public string InvoiceAddressID { get; set; }
        public string UserID { get; set; }
        public bool IsPayPal { get; set; }
        public bool IsIngTeknoKredi { get; set; }
        public bool? IsOneClick { get; set; }
        public string Store { get; set; }
        public string PaymentPos { get; set; }
        public string StoreOrderId { get; set; }
        public decimal? used_cc_point { get; set; }
        public bool? PrintInvoice { get; set; }
        public string InvoiceEmail { get; set; }
    }

}