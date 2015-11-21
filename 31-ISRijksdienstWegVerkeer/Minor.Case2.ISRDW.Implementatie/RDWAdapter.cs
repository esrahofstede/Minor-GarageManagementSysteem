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
        private ILoggingManager _loggingManager;

        /// <summary>
        /// Submits the APK verzoek to the RDW API
        /// </summary>
        /// <param name="message">Requestmessage for APK verzoek</param>
        /// <returns>Responsemessage of the APK verzoek</returns>
        public apkKeuringsverzoekResponseMessage SubmitAPKVerzoek(apkKeuringsverzoekRequestMessage message)
        {
            _loggingManager.Log(message, DateTime.Now);
            var responseXML = _rdwService.SubmitAPKVerzoek(Utility.SerializeToXML(message));
            var response = Utility.DeserializeFromXML<apkKeuringsverzoekResponseMessage>(responseXML);
            _loggingManager.Log(response, DateTime.Now);
            return response;
        }

        public RDWAdapter()
        {
            _rdwService = new RDWService();
            _loggingManager = new LoggingManager();
        }

        /// <summary>
        /// Creates an instance of the RDWAdapter and can be injected with an IRDWService
        /// </summary>
        /// <param name="rdwService">Injectable IRDWService</param>
        public RDWAdapter(IRDWService rdwService, ILoggingManager loggingManager)
        {
            _rdwService = rdwService;
            _loggingManager = loggingManager;
        }
    }
}