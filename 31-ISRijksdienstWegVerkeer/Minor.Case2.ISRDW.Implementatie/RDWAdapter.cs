using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Minor.Case2.ISRDW.Implementation
{
    public class RDWAdapter
    {

        public string CreateMessage()
        {
            apkKeuringsverzoekRequestMessage message = new apkKeuringsverzoekRequestMessage
            {
                keuringsverzoek = new keuringsverzoek
                {
                    keuringsdatum = new DateTime(2008, 11, 19),
                    keuringsinstantie = new keuringsinstantie
                    {
                        kvk = "3013 5370",
                        type = "garage",
                        naam = "Garage Voorbeeld B.V.",
                        plaats = "Wijk bij Voorbeeld",
                    },
                    voertuig = new keuringsverzoekVoertuig
                    {
                        kenteken = "BV-01-EG",
                        kilometerstand = 12345,
                        naam = "A. Eigenaar",
                        type = "personenauto",
                    },
                    correlatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                }
            };

            return UsingStream(message);
        }


        private string UsingStream(apkKeuringsverzoekRequestMessage myClass)
        {
            using (var ms = new MemoryStream())
            {
                using (var xw = XmlWriter.Create(ms)) // Remember to stop using XmlTextWriter  
                {
                    var serializer = new XmlSerializer(myClass.GetType());
                    serializer.Serialize(xw, myClass);
                    xw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var sr = new StreamReader(ms, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        public RDWAdapter()
        {

        }

        private string GetMessage(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string returnMessage = reader.ReadToEnd();
            return returnMessage;
        }

        public string SubmitAPKVerzoek(string url, string message)
        {
            return PostMessage(url, message);
        }

        private string PostMessage(string url, string message)
        {
            byte[] bodyBytes = Encoding.UTF8.GetBytes(message);
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = bodyBytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bodyBytes, 0, bodyBytes.Length);

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string returnMessage = reader.ReadToEnd();
            return returnMessage;
        }
    }

    

}