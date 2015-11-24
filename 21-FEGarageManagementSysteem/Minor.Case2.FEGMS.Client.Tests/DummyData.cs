using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.FEGMS.Client.ViewModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.FEGMS.Client.Tests
{
    internal class DummyData
    {
        /// <summary>
        /// Get dummy klantgegevens 
        /// </summary>
        /// <param name="lease">True if car is lease</param>
        /// <returns></returns>
        internal static InsertKlantgegevensVM GetKlantGegevens(bool lease)
        {
            return new InsertKlantgegevensVM
            {
                Voornaam = "Kees",
                Achternaam = "Caespi",
                Adres = "Akkerstraat 12",
                Postcode = "4322 AS",
                Woonplaats = "Utrecht",
                Lease = lease,
                Telefoonnummer = "0612345678",
                Emailadres = "info@caespi.nl",
            };
        }

        internal static InsertLeasemaatschappijGegevensVM GetLeasemaatschappijGegevens()
        {
            return new InsertLeasemaatschappijGegevensVM
            {
                Naam = "Sixt",
                Telefoonnummer = "0687654321",
            };
        }

        internal static InsertVoertuiggegevensVM GetVoertuiggegevens()
        {
            return new InsertVoertuiggegevensVM
            {
                Kenteken = "KS-23-GF",
                Merk = "Volkswagen",
                Type = "Polo",
            };
        }

        internal static KlaarmeldenVM GetKlaarmelden()
        {
            return new KlaarmeldenVM
            {
                Kenteken = "DS-344-S",
            };
        }

        internal static InsertOnderhoudsopdrachtVM GetOnderhoudsopdracht()
        {
            return new InsertOnderhoudsopdrachtVM
            {
                AanmeldingsDatum = new DateTime(2015, 11, 23),
                APK = true,
                Kilometerstand = 12345,
                Onderhoudsomschrijving = "APK + Koppeling vervangen",
            };
        }

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
