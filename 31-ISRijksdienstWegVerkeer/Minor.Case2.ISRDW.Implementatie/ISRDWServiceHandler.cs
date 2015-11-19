using Minor.Case2.ISRDW.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Schema;

namespace Minor.Case2.ISRDW.Implementation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    public class ISRDWServiceHandler : IISRDWService
    {
        /// <summary>
        /// Send an apk request to the RDW service 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public SendRdwKeuringsverzoekResponseMessage RequestKeuringsverzoek(SendRdwKeuringsverzoekRequestMessage message)
        {
            var apkKeuringsverzoekRequestMessage = Mapper.MapToRDWRequestMessage(message);

            var apkKeuringsverzoekResponseMessage = new RDWAdapter().SubmitAPKVerzoek(apkKeuringsverzoekRequestMessage);

            return Mapper.MapToResponseMessage(apkKeuringsverzoekResponseMessage);
        }
    }
}
