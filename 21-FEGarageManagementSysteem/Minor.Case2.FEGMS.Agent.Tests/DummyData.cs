using System;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.FEGMS.Agent.Tests
{
    internal static class DummyData
    {
        public static Persoon GetDummyPersoon()
        {
            return new Persoon
            {
                Voornaam = "Piet",
                Achternaam = "Pietersen",
                Adres = "Kerkstraat 12",
                Emailadres = "piet@gmail.com",
                Postcode = "5124 DJ",
                Telefoonnummer = "0612345678",
                Woonplaats = "Utrecht",
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
                Naam = "Sixt",
                Telefoonnummer = "0621448522",
            };
        }

        public static Onderhoudsopdracht GetDummyOnderhoudsopdracht()
        {
            return new Onderhoudsopdracht
            {
                ID = 1,
                APK = true,
                Kilometerstand = 12000,
                Aanmeldingsdatum = new DateTime(2015, 11, 11),
                Onderhoudsomschrijving = "APK Keuren",
                Voertuig = GetDummyVoertuig()
            };
        }

        public static Onderhoudswerkzaamheden GetDummyOnderhoudsoerkzaamheden()
        {
            return new Onderhoudswerkzaamheden
            {
                Kilometerstand = 12000,
                Afmeldingsdatum = new DateTime(2015, 11, 13),
                Onderhoudswerkzaamhedenomschrijving = "Lampen vervangen",
            };
        }

        internal static VoertuigenSearchCriteria GetSearchCriteria()
        {
            return new VoertuigenSearchCriteria
            {
                Kenteken = "DS-344-S",
            };
        }
        public static VoertuigenCollection GetVoertuigenCollection()
        {
            var voertuigen = new VoertuigenCollection();
            voertuigen.Add(new Voertuig
            {
                Kenteken = "DS-344-S",
            });


            return voertuigen;

        }

        public static KlantenCollection GetAllLeasemaatschappijen()
        {
            var klanten = new KlantenCollection();

            klanten.Add(new Leasemaatschappij
            {
                ID = 1,
                Klantnummer = 123,
                Naam = "LeasePlan",
                Telefoonnummer = "0612345678"
            });

            klanten.Add(new Leasemaatschappij
            {
                ID = 2,
                Klantnummer = 141,
                Naam = "Sixt",
                Telefoonnummer = "065641235"
            });

            klanten.Add(new Leasemaatschappij
            {
                ID = 1,
                Klantnummer = 23645,
                Naam = "AutoLease",
                Telefoonnummer = "0612354845"
            });
            return klanten;
        }
    }
}