using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test.Mappers
{
    [TestClass]
    public class OnderhoudswerkzaamhedenDTOMapperTests
    {
        [TestMethod]
        public void MapOnderhoudswerkzaamhedentDTOToOnderhoudswerkzaamhedenEntityTest()
        {
            // Arange
            BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudswerkzaamheden dto = new BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudswerkzaamheden
            {
                ID = 1,
                Afmeldingsdatum = new DateTime(2014,07,07),
                Kilometerstand = 1300,
                Onderhoudswerkzaamhedenomschrijving = "omschrijving",
                Onderhoudsopdracht = new BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht{}
                
            };

            // Act
            var result = OnderhoudswerkzaamhedenDTOMapper.MapDTOToEntity(dto);

            // Assert
            Assert.AreEqual(1, result.ID);
            Assert.AreEqual(new DateTime(2014, 07, 07), result.Afmeldingsdatum);
            Assert.AreEqual(1300, result.Kilometerstand);
            Assert.AreEqual("omschrijving", result.Omschrijving);


        }

        [TestMethod]
        public void MapOnderhoudswerkzaamhedenEntityToOnderhoudswerkzaamhedenDTOTest()
        {
            // Arange
            Entities.Onderhoudswerkzaamheden entity = new Entities.Onderhoudswerkzaamheden
            {
                ID = 1,
                Afmeldingsdatum = new DateTime(2014, 07, 07),
                Kilometerstand = 1300,
                Omschrijving = "omschrijving",
                Onderhoudsopdracht = new Entities.Onderhoudsopdracht { }
            };

            // Act
            var result = OnderhoudswerkzaamhedenDTOMapper.MapEntityToDTO(entity);

            // Assert
            Assert.AreEqual(1, result.ID);
            Assert.AreEqual(new DateTime(2014, 07, 07), result.Afmeldingsdatum);
            Assert.AreEqual(1300, result.Kilometerstand);
            Assert.AreEqual("omschrijving", result.Onderhoudswerkzaamhedenomschrijving);
        }
    }
}
