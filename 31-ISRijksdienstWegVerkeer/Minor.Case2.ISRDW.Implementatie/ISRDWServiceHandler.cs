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
            var apkKeuringsverzoekRequestMessage = MapToRDWRequestMessage(message);

            var apkKeuringsverzoekResponseMessage = new RDWAdapter().SubmitAPKVerzoek(apkKeuringsverzoekRequestMessage);

            return MapToResponseMessage(apkKeuringsverzoekResponseMessage);
        }

        /// <summary>
        /// Map the keuringverzoekRequest object to the ApkKeuringverzoek request
        /// </summary>
        /// <param name="message"></param>
        /// <returns>apkKeuringsverzoekRequestMessage object to send to the RDW service</returns>
        public apkKeuringsverzoekRequestMessage MapToRDWRequestMessage(SendRdwKeuringsverzoekRequestMessage message)
        {
            var keuringsverzoek = message.Keuringsverzoek;

            var apkKeuringsverzoek = new keuringsverzoek()
            {
                correlatieId = keuringsverzoek.CorrolatieId,
                keuringsdatum = keuringsverzoek.Date,
                keuringsinstantie = new keuringsinstantie
                {
                    kvk = message.Garage.Kvk,
                    naam = message.Garage.Naam,
                    plaats = message.Garage.Plaats,
                    type = message.Garage.Type,
                },
                voertuig = new keuringsverzoekVoertuig
                {
                    kenteken = message.Voertuig.kenteken,
                    kilometerstand = 0,
                    naam = message.Voertuig.bestuurder.achternaam,
                    type = voertuigtype.personenauto,
                }
            };

            return new apkKeuringsverzoekRequestMessage
            {
                keuringsverzoek = apkKeuringsverzoek,
            };
        }

        /// <summary>
        /// Map the ApkKeuringverzoek  object to the keuringverzoekRequest request
        /// </summary>
        /// <param name="reponse">apkKeuringsverzoekRequestMessage object from the RDW service</param>
        public SendRdwKeuringsverzoekResponseMessage MapToResponseMessage(apkKeuringsverzoekResponseMessage reponse)
        {
            var keuringsRegistratie = reponse.keuringsregistratie;

            return new SendRdwKeuringsverzoekResponseMessage
            {
                Kenteken = keuringsRegistratie.kenteken,
                Keuringsverzoek = new Keuringsverzoek
                {
                    CorrolatieId = keuringsRegistratie.correlatieId,
                    Date = keuringsRegistratie.keuringsdatum
                },
                Steekproef = keuringsRegistratie.steekproef.HasValue
            };
        }
    }
}
