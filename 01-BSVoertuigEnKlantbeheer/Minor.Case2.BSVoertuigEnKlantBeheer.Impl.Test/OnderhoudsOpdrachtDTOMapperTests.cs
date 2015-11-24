using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class OnderhoudsOpdrachtDTOMapperTests
    {
        [TestMethod]
        public void MapOnderhoudsOpdrachtDTOToOnderhoudsOpdrachtEntityTest()
        {
            // Arange
            BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht dto = new BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht
            {
                APK = false,
                Aanmeldingsdatum = new DateTime(2013, 08, 08),
                Kilometerstand = 12000,
                Onderhoudsomschrijving = "Olie vervangen",
                Voertuig = new BSVoertuigEnKlantbeheer.V1.Schema.Voertuig()
            };

            // Act
            var result = OnderhoudsOpdrachtDTOMapper.MapDTOToEntity(dto);

            // Assert
            Assert.AreEqual(false, result.APK);
            Assert.AreEqual(new DateTime(2013, 08, 08), result.Aanmeldingsdatum);
            Assert.AreEqual(12000, result.Kilometerstand);
            Assert.AreEqual("Olie vervangen", result.Onderhoudsomschrijving);

        }

        [TestMethod]
        public void MapOnderhoudsOpdrachtEntityToOnderhoudsOpdrachtDTOTest()
        {
            // Arange
            Entities.Onderhoudsopdracht entity = new Entities.Onderhoudsopdracht
            {
                APK = false,
                Aanmeldingsdatum = new DateTime(2013, 08, 08),
                Kilometerstand = 12000,
                Onderhoudsomschrijving = "Olie vervangen",
                Voertuig = new Entities.Voertuig()
            };

            // Act
            var result = OnderhoudsOpdrachtDTOMapper.MapEntityToDTO(entity);

            // Assert
            Assert.AreEqual(false, result.APK);
            Assert.AreEqual(new DateTime(2013, 08, 08), result.Aanmeldingsdatum);
            Assert.AreEqual(12000, result.Kilometerstand);
            Assert.AreEqual("Olie vervangen", result.Onderhoudsomschrijving);
        }
    }
}
