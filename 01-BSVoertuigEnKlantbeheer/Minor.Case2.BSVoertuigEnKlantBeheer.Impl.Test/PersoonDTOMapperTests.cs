using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class PersoonDTOMapperTests
    {
        [TestMethod]
        public void MapPersoonDTOToPersoonEntityTest()
        {
            // Arange
            Persoon dto = new Persoon
            {
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
            var result = PersoonDTOMapper.MapDTOToEntity(dto);

            // Assert
            Assert.AreEqual(100, result.Klantnummer);
            Assert.AreEqual("Gerard", result.Voornaam);

        }

        [TestMethod]
        public void MapPersoonEntityToPersoonDTOTest()
        {
            // Arange
            Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Persoon entity = new Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Persoon
            {
                Klantnummer = 100,
                Voornaam = "Gerard",
                Tussenvoegsel = "",
                Achternaam = "Vos",
                Emailadres = "gerard@vos.nl",
                Adres = "Utrechtseweg 10",
                Postcode = "1234AB",
                Telefoonnummer = "040-1234567",
                Woonplaats = "Utrecht"
            };

            // Act
            var result = PersoonDTOMapper.MapEntityToDTO(entity);

            // Assert
            Assert.AreEqual(100, result.klantnummer);
            Assert.AreEqual("Gerard", result.Voornaam);
        }
    }
}
