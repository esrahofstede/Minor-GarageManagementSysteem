using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class VoertuigDTOMapperTests
    {
        [TestMethod]
        public void MapVoertuigDTOToVoertuigEntityMetPersoonTest()
        {
            // Arange
            Voertuig dto = new Voertuig
            {
                Kenteken = "12-AA-AA",
                Bestuurder = new Persoon
                {
                    Voornaam = "Henk"
                },
                Eigenaar = new Persoon
                {
                    Voornaam = "Henk"
                },
                Merk = "Ford",
                Type = "Focus",
            };

            // Act
            var result = VoertuigDTOMapper.MapDTOToEntity(dto);

            // Assert
            Assert.AreEqual("12-AA-AA", result.Kenteken);
            Assert.AreEqual(typeof(Entities.Persoon), result.Bestuurder.GetType());
            Assert.AreEqual(typeof(Entities.Persoon), result.Eigenaar.GetType());
            Assert.AreEqual("Henk", ((Entities.Persoon)result.Eigenaar).Voornaam);

            Assert.AreNotEqual(typeof(Entities.Leasemaatschappij), result.Eigenaar.GetType());
        }

        [TestMethod]
        public void MapVoertuigDTOToVoertuigEntityMetLeasemaatschappijTest()
        {
            // Arange
            Voertuig dto = new Voertuig
            {
                Kenteken = "12-AA-AA",
                Bestuurder = new Persoon
                {
                    Voornaam = "Henk"
                },
                Eigenaar = new Leasemaatschappij
                {
                    Naam = "Sixt"
                },
                Merk = "Ford",
                Type = "Focus",
            };

            // Act
            var result = VoertuigDTOMapper.MapDTOToEntity(dto);

            // Assert
            Assert.AreEqual("12-AA-AA", result.Kenteken);
            Assert.AreEqual(typeof(Entities.Persoon), result.Bestuurder.GetType());
            Assert.AreEqual(typeof(Entities.Leasemaatschappij), result.Eigenaar.GetType());
            Assert.AreEqual("Sixt", ((Entities.Leasemaatschappij)result.Eigenaar).Naam);

            Assert.AreNotEqual(typeof(Entities.Persoon), result.Eigenaar.GetType());
        }
    }
}
