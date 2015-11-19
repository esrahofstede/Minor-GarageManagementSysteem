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
    }
}
