using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class HandlerTest
    {
        [TestMethod]
        public void InsertVoertuig()
        {
            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler();

            Persoon p = new Persoon
            {
                Voornaam = "Joop",
                Tussenvoegsel = "en",
                Achternaam = "Jopie",
                Adres = "J",
                Emailadres = "j@j.nl",
                Postcode = "1234AB",
                Telefoonnummer = "0305556767",
                Woonplaats = "Utrecht"
            };

            Voertuig v = new Voertuig
            {
                Kenteken = "12-AA-AA",
                Bestuurder = p,
                Eigenaar = p,
                Merk = "Ford",
                Type = "Focus",
            };

            handler.VoegVoertuigMetKlantToe(v);
        }
    }
}
