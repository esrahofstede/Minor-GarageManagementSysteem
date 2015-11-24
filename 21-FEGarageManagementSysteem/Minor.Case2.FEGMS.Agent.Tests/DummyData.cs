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
    }
}