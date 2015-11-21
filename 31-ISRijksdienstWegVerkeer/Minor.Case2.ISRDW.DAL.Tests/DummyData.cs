using Minor.Case2.ISRDW.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.DAL.Tests
{
    internal static class DummyData
    {
        internal static Keuringsregistratie GetKeuringsregistratie()
        {
            return new Keuringsregistratie
            {
                CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                Kenteken = "BV-01-EG",
                Keuringsdatum = new DateTime(2008, 11, 19),
                Steekproef = null,
            };
        }

        internal static Keuringsverzoek GetKeuringsverzoek()
        {
            return new Keuringsverzoek
            {
                CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                Kenteken = "BV-01-EG",
                Keuringsdatum = new DateTime(2008, 11, 19),
                KeuringsinstantieNaam = "Garage Voorbeeld B.V",
                KeuringsinstantiePlaats = "Wijk bij Voorbeeld",
                KeuringsinstantieType = "garage",
                KVK = "3013 5370",
                NaamEigenaar = "A. Eigenaar",
                VoertuigType = "personenauto",
            };
        }
    }
}
