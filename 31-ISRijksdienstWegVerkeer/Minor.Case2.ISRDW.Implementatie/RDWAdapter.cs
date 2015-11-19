using Minor.Case2.ISRDW.Implementation.RDWIntegration;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Minor.Case2.ISRDW.Implementation
{
    /// <summary>
    /// The adapter for the connection with the RDW API
    /// </summary>
    public class RDWAdapter
    {
        private IRDWService _rdwService;

        /// <summary>
        /// Submits the APK verzoek to the RDW API
        /// </summary>
        /// <param name="message">Requestmessage for APK verzoek</param>
        /// <returns>Responsemessage of the APK verzoek</returns>
        public apkKeuringsverzoekResponseMessage SubmitAPKVerzoek(apkKeuringsverzoekRequestMessage message)
        {
            var response = _rdwService.SubmitAPKVerzoek(Utility.SerializeToXML(message));
            return Utility.DeserializeFromXML<apkKeuringsverzoekResponseMessage>(response);
        }

        public RDWAdapter()
        {
            _rdwService = new RDWService();
        }

        /// <summary>
        /// Creates an instance of the RDWAdapter and can be injected with an IRDWService
        /// </summary>
        /// <param name="rdwService">Injectable IRDWService</param>
        public RDWAdapter(IRDWService rdwService)
        {
            _rdwService = rdwService;
        }
    }
}