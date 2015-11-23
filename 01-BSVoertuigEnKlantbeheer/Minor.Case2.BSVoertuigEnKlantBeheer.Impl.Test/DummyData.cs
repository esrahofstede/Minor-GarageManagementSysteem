using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    internal static class DummyData
    {
        public static Persoon GetDummyPersoon()
        {
            return new Persoon
            {
                ID = 1,
                Klantnummer = 1,
                Voornaam = "Piet",
                Achternaam = "Pietersen",
            };
        }

        public static Leasemaatschappij GetDummyLeasemaatschappij()
        {
            return new Leasemaatschappij
            {
                ID = 2,
                Klantnummer = 2,
                Naam = "Sixt",
            };
        }

        public static Voertuig GetDummyVoertuig()
        {
            return new Voertuig
            {
                ID = 1,
                Kenteken = "NL-123-G",
                Merk = "Citroen",
                Type = "C3",
                EigenaarID = 1,
                BestuurderID = 1,
            };
        }

    }
}
