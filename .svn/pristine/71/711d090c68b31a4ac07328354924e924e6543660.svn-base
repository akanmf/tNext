using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace tNext.Common.Proxy.TeknosaMobileServiceProxy
{

    public partial class WebServiceStructure : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public WebServiceStructure()
        {
            applicationNameHeader = new ApplicationNameHeader
            {
                ClientName = "Application",
                Password = "Password"
            };
        }
        public ApplicationNameHeader applicationNameHeader;

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://service.teknosa.com/")]
        public class ApplicationNameHeader : SoapHeader
        {
            public string ClientName { get; set; }

            public string Password { get; set; }
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest req = (HttpWebRequest)base.GetWebRequest(uri);
            req.Headers.Add("Deneme", "12345678901234567890");

            return req;
        }


    }

}
