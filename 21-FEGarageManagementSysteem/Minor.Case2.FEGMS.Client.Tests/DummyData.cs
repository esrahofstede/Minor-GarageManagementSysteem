using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.FEGMS.Client.ViewModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.Web.Mvc;

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

        internal static InsertLeasemaatschappijGegevensVM GetLeasemaatschappijGegevens(bool exists)
        {
            return new InsertLeasemaatschappijGegevensVM
            {
                Naam = "Sixt",
                Telefoonnummer = "0687654321",
                Exist = exists,
                Leasemaatschappijen = GetAllLeasemaatschappijen().Select(lease => new SelectListItem { Value = lease.ID.ToString(), Text = lease.Naam }),
                SelectedLeasemaatschappijID = exists ? 1 : 0,
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

        internal static IEnumerable<Leasemaatschappij> GetAllLeasemaatschappijen()
        {
            return new List<Leasemaatschappij>()
            {
                new Leasemaatschappij
                {
                    ID = 1,
                    Naam = "Sixt",
                    Klantnummer = 123456,
                    Telefoonnummer = "0621345678",
                },
                new Leasemaatschappij
                {
                    ID = 2,
                    Naam = "DutchLease",
                    Klantnummer = 561456,
                    Telefoonnummer = "0612431536",
                },
                new Leasemaatschappij
                {
                    ID = 3,
                    Naam = "LeasePlanDirect",
                    Klantnummer = 2135126,
                    Telefoonnummer = "0645786542",
                },
                new Leasemaatschappij
                {
                    ID = 4,
                    Naam = "DirectLease",
                    Klantnummer = 879435,
                    Telefoonnummer = "0625495321",
                },
            };
        }
    }
}
