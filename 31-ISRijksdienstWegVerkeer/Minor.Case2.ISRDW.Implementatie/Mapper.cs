using Minor.Case2.ISRDW.DAL.Entities;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.Implementation
{
    /// <summary>
    /// A static helper class for mappings
    /// </summary>
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
                    kenteken = message.Voertuig.Kenteken,
                    kilometerstand = 0,
                    naam = message.Voertuig.Bestuurder.Achternaam,
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
                Keuringsverzoek = new ISRijksdienstWegVerkeer.V1.Schema.Keuringsverzoek
                {
                    CorrolatieId = keuringsRegistratie.correlatieId,
                    Date = keuringsRegistratie.keuringsdatum
                },
                Steekproef = keuringsRegistratie.steekproef.HasValue
            };
        }

        /// <summary>
        /// Map the apkKeuringsverzoekRequestMessage with a timestamp to a logging object
        /// </summary>
        /// <param name="message">an apkKeuringsverzoekRequestMessage to map</param>
        /// <param name="time">Time of the logging</param>
        /// <returns>The mapped logging object</returns>
        public static Logging MapToLogging(apkKeuringsverzoekRequestMessage message, DateTime time)
        {
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message), "The message that needs to be mapped, cannot be null");
            }

            return new Logging
            {
                Keuringsverzoek = new DAL.Entities.Keuringsverzoek
                {
                    CorrelatieId = message.keuringsverzoek.correlatieId,
                    Kenteken = message.keuringsverzoek.voertuig.kenteken,
                    NaamEigenaar = message.keuringsverzoek.voertuig.naam,
                    VoertuigType = message.keuringsverzoek.voertuig.type.ToString(),
                    Kilometerstand = message.keuringsverzoek.voertuig.kilometerstand,
                    Keuringsdatum = message.keuringsverzoek.keuringsdatum,
                    KeuringsinstantieNaam = message.keuringsverzoek.keuringsinstantie.naam,
                    KeuringsinstantiePlaats = message.keuringsverzoek.keuringsinstantie.plaats,
                    KeuringsinstantieType = message.keuringsverzoek.keuringsinstantie.type,
                    KVK = message.keuringsverzoek.keuringsinstantie.kvk,
                },
                Time = time,
            };
        }

        /// <summary>
        /// Map the apkKeuringsverzoekResponseMessage with a timestamp to a logging object
        /// </summary>
        /// <param name="message">an apkKeuringsverzoekResponseMessage to map</param>
        /// <param name="time">Time of the logging</param>
        /// <returns>The mapped logging object</returns>
        public static Logging MapToLogging(apkKeuringsverzoekResponseMessage message, DateTime time)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "The message that needs to be mapped, cannot be null");
            }

            return new Logging
            {
                Keuringsregistratie = new DAL.Entities.Keuringsregistratie
                {
                    CorrelatieId = message.keuringsregistratie.correlatieId,
                    Kenteken = message.keuringsregistratie.kenteken,
                    Keuringsdatum = message.keuringsregistratie.keuringsdatum,
                    Steekproef = message.keuringsregistratie.steekproef,
                },
                Time = time,
            };
        }
    }
}
