using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.FEGMS.Client.ViewModel;

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
    }
}
