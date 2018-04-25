using System;

namespace tNext.Microservices.Order.Api.Model
{
    public class XXWW_SIPARIS_KARGO_BILGILER_V
    {
        public string URUN_KODU { get; set; }
        public string ISLEM { get; set; }
        public DateTime? ISLEM_SAATI { get; set; }
        public string NAKLIYECI { get; set; }
        public string DURUM_KODU { get; set; }
        public string TAKIP_NO { get; set; }
        public string ORDER_NUMBER { get; set; }

    }
}