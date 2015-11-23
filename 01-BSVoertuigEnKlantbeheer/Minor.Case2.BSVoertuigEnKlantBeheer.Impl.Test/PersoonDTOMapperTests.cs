using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minorcase2bsvoertuigenklantbeheer.v1.schema;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class PersoonDTOMapperTests
    {
        [TestMethod]
        public void MapPersoonDTOToPersoonEntityTest()
        {
            // Arange
            Persoon klantDTO = new Persoon
            {
                id = 1,
                klantnummer = 100,
                Voornaam = "Gerard",
                Tussenvoegsel = "",
                Achternaam = "Vos"  ,
                Emailadres = "gerard@vos.nl",
                Adres = "Utrechtseweg 10",
                Postcode = "1234AB",
                Telefoonnummer = "040-1234567",
                Woonplaats = "Utrecht"
            };

            // Act
            //PersoonDTOMapper
            // Assert
        }
    }
}
