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
                Kenteken = "AZ-AZ-AZ",
                Merk = "Citroen",
                Type = "C3",
                Eigenaar = new Persoon
                {
                    ID = 2
                },
                Bestuurder = new Persoon
                {
                    ID = 2
                },
                EigenaarID = 2,
                BestuurderID = 2,
                Status = "Aangemeld"
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

        public static Onderhoudsopdracht GetDummyOnderhoudsOpdracht()
        {
            return new Onderhoudsopdracht
            {
                APK = true,
                Kilometerstand = 12000,
                Aanmeldingsdatum = new DateTime(2015, 11, 11),
                Onderhoudsomschrijving = "APK Keuren",
                Voertuig = GetDummyVoertuig()
            };
        }

        public static Onderhoudswerkzaamheden GetDummyOnderhoudsWerkzaamheden()
        {
            return new Onderhoudswerkzaamheden
            {
                Kilometerstand = 12000,
                Afmeldingsdatum = new DateTime(2015, 11, 13),
                Omschrijving = "Lampen vervangen",  
                Onderhoudsopdracht = GetDummyOnderhoudsOpdracht()
            };
        }
    }
}
