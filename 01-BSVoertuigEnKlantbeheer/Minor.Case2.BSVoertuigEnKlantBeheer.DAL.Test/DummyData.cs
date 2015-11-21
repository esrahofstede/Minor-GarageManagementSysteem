using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    internal static class DummyData
    {
        public static Persoon GetDummyPersoon()
        {
            return new Persoon
            {

                Klantnummer = 123123,
                Voornaam = "Piet",
                Achternaam = "Pietersen",
            };
        }

        public static Voertuig GetDummyVoertuig()
        {
            return new Voertuig
            {
                Kenteken = "NL-123-G",
                Merk = "Citroen",
                Type = "C3",
                Eigenaar = GetDummyLeasemaatschappij(),
                Bestuurder = GetDummyPersoon()
            };
        }

        public static Leasemaatschappij GetDummyLeasemaatschappij()
        {
            return new Leasemaatschappij
            {
                Klantnummer = 101010,
                Naam = "Sixt",
            };
        }

        public static OnderhoudsOpdracht GetDummyOnderhoudsOpdracht()
        {
            return new OnderhoudsOpdracht
            {
                APK = true,
                Kilometerstand = 12000,
                AanmeldingsDatum = new DateTime(2015, 11, 11),
                OnderhoudsOmschrijving = "APK Keuren",
                Voertuig = GetDummyVoertuig()
            };
        }

        public static OnderhoudsWerkzaamheden GetDummyOnderhoudsWerkzaamheden()
        {
            return new OnderhoudsWerkzaamheden
            {
                Kilometerstand = 12000,
                AfmeldingsDatum = new DateTime(2015, 11, 13),
                Omschrijving = "Lampen vervangen",  
                OnderhoudsOpdracht = GetDummyOnderhoudsOpdracht()
            };
        }
    }
}
