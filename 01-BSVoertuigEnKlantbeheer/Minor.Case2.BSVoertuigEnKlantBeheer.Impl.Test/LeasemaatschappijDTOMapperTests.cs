using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class LeasemaatschappijDTOMapperTests
    {
        [TestMethod]
        public void MapLeasemaatschappijDTOToLeasemaatschappijEntityTest()
        {
            // Arange
            Leasemaatschappij dto = new Leasemaatschappij
            {
                klantnummer = 100,
                Naam = "Sixt",
                Emailadres = "info@sixt.nl",
                Adres = "Utrechtseweg 10",
                Postcode = "1234AB",
                Telefoonnummer = "040-1234567",
                Woonplaats = "Utrecht"
            };

            // Act
            var result = LeasemaatschappijDTOMapper.MapDTOToEntity(dto);

            // Assert
            Assert.AreEqual(100, result.Klantnummer);
            Assert.AreEqual("Sixt", result.Naam);

        }

        [TestMethod]
        public void MapLeasemaatschappijEntityToLeasemaatschappijDTOTest()
        {
            // Arange
            Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Leasemaatschappij entity = new Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Leasemaatschappij
            {
                Klantnummer = 100,
                Naam = "Sixt",
                Emailadres = "info@sixt.nl",
                Adres = "Utrechtseweg 10",
                Postcode = "1234AB",
                Telefoonnummer = "040-1234567",
                Woonplaats = "Utrecht"
            };

            // Act
            var result = LeasemaatschappijDTOMapper.MapEntityToDTO(entity);

            // Assert
            Assert.AreEqual(100, result.klantnummer);
            Assert.AreEqual("Sixt", result.Naam);
        }
    }
}
