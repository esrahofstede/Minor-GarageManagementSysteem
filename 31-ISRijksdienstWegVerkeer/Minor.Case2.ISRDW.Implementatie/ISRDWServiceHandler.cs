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
            var keuringsverzoek = message.Keuringsverzoek;
            //var keuringsverzoek = new Keuringsverzoek
            //{
            //    CorrolatieId = message.Keuringsverzoek.CorrolatieId,
            //    Date = message.
            //    Type = message.Keuringsverzoek.Type,
            //};


            var apkKeuringsverzoek = new keuringsverzoek()
            {
                correlatieId = keuringsverzoek.CorrolatieId,
                keuringsdatum = keuringsverzoek.Date,
                keuringsinstantie = new keuringsinstantie
                {
                    kvk = message.Garage.Kvk,
                    naam = message.Garage.Naam,
                    plaats = message.Garage.Plaats,
                    type = "garage",
                },
                voertuig = new keuringsverzoekVoertuig
                {
                    kenteken = message.Voertuig.kenteken,
                    kilometerstand = 0,
                    naam = message.Voertuig.kenteken,
                    type = Util.ParseEnum<voertuigtype>(keuringsverzoek.Type),  
                }
            };

            var apkKeuringsverzoekRequestMessage = new apkKeuringsverzoekRequestMessage
            {
                keuringsverzoek = apkKeuringsverzoek, 
            };

            var apkKeuringsverzoekResponseMessage = new RDWAdapter().SubmitAPKVerzoek(apkKeuringsverzoekRequestMessage);
            var keuringsRegistratie = apkKeuringsverzoekResponseMessage.keuringsregistratie;

            return new SendRdwKeuringsverzoekResponseMessage
            {
                Kenteken = keuringsRegistratie.kenteken,
                Keuringsverzoek = new Keuringsverzoek
                {
                    CorrolatieId = keuringsRegistratie.correlatieId,
                    Date = keuringsRegistratie.keuringsdatum,
                    Type = message.Keuringsverzoek.Type,
                },
                Steekproef = keuringsRegistratie.steekproef.HasValue
            };
        }
    }
}
