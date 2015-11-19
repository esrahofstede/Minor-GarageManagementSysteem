using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Schema;
using minorcase2bsvoertuigenklantbeheer.v1.schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.Implementation.Tests
{
    internal static class DummyData
    {
        internal static apkKeuringsverzoekRequestMessage GetMessage()
        {
            return new apkKeuringsverzoekRequestMessage
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
                        type = voertuigtype.personenauto
                    },
                    correlatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                }
            };
        }

        internal static SendRdwKeuringsverzoekRequestMessage GetRequestMessage()
        {
            return new SendRdwKeuringsverzoekRequestMessage
            {
                Garage = new Garage
                {
                    Naam = "test",
                    Kvk = "123112344",
                    Plaats = "Utrecht",
                    Type = "Garage",
                },
                Voertuig = new Voertuig
                {
                    kenteken = "12-AA-BB",
                    merk = "ford",
                    type = "focus",
                    bestuurder = new Persoon
                    {
                        voornaam = "jan",
                        achternaam = "jansen"
                    },
                    eigenaar = new Persoon
                    {
                        voornaam = "jan",
                        achternaam = "jansen"
                    },
                    id = 1
                },
                Keuringsverzoek = new Keuringsverzoek
                {
                    CorrolatieId = new Guid().ToString(),
                    Date = DateTime.Now
                }
            };
        }

        internal static apkKeuringsverzoekResponseMessage GetReponseMessage()
        {
            return new apkKeuringsverzoekResponseMessage
            {
                keuringsregistratie = new keuringsregistratie()
                {
                    correlatieId = new Guid().ToString(),
                    kenteken = "12-AA-BB",
                    steekproef = DateTime.Parse("2014-01-10"),
                    keuringsdatum = DateTime.Parse("2014-01-01"),
                    steekproefSpecified = true
                }
            };
        }
    }
}
