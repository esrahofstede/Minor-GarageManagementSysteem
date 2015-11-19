using System;
using System.IO;
using System.Net;
using System.Text;

namespace Minor.Case2.ISRDW.Implementation.RDWIntegration
{
    public class RDWService : IRDWService
    {
        public string SubmitAPKVerzoek(string message)
        {
            string urlRDW = @"http://rdwserver:18423/rdw/APKKeuringsverzoek";
            return PostMessage(urlRDW, message);
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