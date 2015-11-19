using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.Implementation
{
    public static class Mapper
    {
        /// <summary>
        /// Map the keuringverzoekRequest object to the ApkKeuringverzoek request
        /// </summary>
        /// <param name="message"></param>
        /// <returns>apkKeuringsverzoekRequestMessage object to send to the RDW service</returns>
        public static apkKeuringsverzoekRequestMessage MapToRDWRequestMessage(SendRdwKeuringsverzoekRequestMessage message)
        {
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message), "The message that needs to be mapped, cannot be null");
            }

            var keuringsverzoek = message?.Keuringsverzoek;

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
        /// <param name="response">apkKeuringsverzoekRequestMessage object from the RDW service</param>
        public static SendRdwKeuringsverzoekResponseMessage MapToResponseMessage(apkKeuringsverzoekResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response), "The message that needs to be mapped, cannot be null");
            }

            var keuringsRegistratie = response.keuringsregistratie;

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
