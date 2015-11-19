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
        public SendRdwKeuringsverzoekResponseMessage RequestKeuringsverzoek(SendRdwKeuringsverzoekRequestMessage message)
        {
            var keuringsverzoek = new Keuringsverzoek
            {
                CorrolatieId = message.Keuringsverzoek.CorrolatieId,
                Date = new DateTime(2015, 10, 10)
            };

            return new SendRdwKeuringsverzoekResponseMessage
            {
                Kenteken = "12-AA-BB",
                Keuringsverzoek = new Keuringsverzoek(),
                Steekproef = true
            };
        }
    }
}
