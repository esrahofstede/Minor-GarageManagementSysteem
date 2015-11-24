using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using Moq;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    [TestClass]
    public class BSVoertuigEnKlantbeheerHandlerTests
    {
        /// <summary>
        /// Test if the right ID is set for the Klant and Eigenaar
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetPersoonTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);          
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object, leaseMock.Object, voertuigMock.Object);

            persoonMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Persoon>())).Returns(1);
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);


            Voertuig voertuig = new Voertuig
            {
                Kenteken = "12-AA-AB",
                Bestuurder = new Persoon(),
                Eigenaar = new Persoon(),
                Merk = "Ford",
                Type = "Focus",
            };


            // Act
            handler.VoegVoertuigMetKlantToe(voertuig);

            // Assert
            voertuigMock.Verify(mock => mock.Insert((It.Is<Entities.Voertuig>(v => v.Bestuurder.ID == 1 && v.Eigenaar.ID == 1))));
        }

        /// <summary>
        /// Test if the right persoonID and leasemaatschappijID are set
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetPersoonEnLeasemaatschappijTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object, leaseMock.Object, voertuigMock.Object);

            persoonMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Persoon>())).Returns(1);
            leaseMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Leasemaatschappij>())).Returns(2);
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);


            Voertuig voertuig = new Voertuig
            {
                Kenteken = "12-AA-AA",
                Bestuurder = new Persoon(),
                Eigenaar = new Leasemaatschappij(),
                Merk = "Ford",
                Type = "Focus",
            };
            
            // Act
            handler.VoegVoertuigMetKlantToe(voertuig);

            // Assert
            voertuigMock.Verify(mock => mock.Insert((It.Is<Entities.Voertuig>(v => v.Bestuurder.ID == 1 && v.Eigenaar.ID == 2))));
        }

        /// <summary>
        /// Test if the right voertuig is returned by known kenteken
        /// </summary>
        [TestMethod]
        public void FilterVoertuigByKnownKentekenTest()
        {
            // Arrange
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>();

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(voertuigMock.Object);

            voertuigMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyVoertuigCollection());

            VoertuigenSearchCriteria zoekCriteria = new VoertuigenSearchCriteria
            {
                Kenteken = "NL-123-1",
            };

            // Act
            var result = handler.GetVoertuigBy(zoekCriteria);

            // Assert
            Assert.AreEqual(1, result.ToArray().Length);
        }

        /// <summary>
        /// Test if the right voertuig is returned by a unknown kenteken
        /// </summary>
        [TestMethod]
        public void FilterVoertuigByUnknownKentekenTest()
        {
            // Arrange
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>();

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(voertuigMock.Object);

            voertuigMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyVoertuigCollection());

            VoertuigenSearchCriteria zoekCriteria = new VoertuigenSearchCriteria
            {
                Kenteken = "12-AA-AA",
            };

            // Act
            var result = handler.GetVoertuigBy(zoekCriteria);

            // Assert
            Assert.AreEqual(0, result.ToArray().Length);
        }

        /// <summary>
        /// Test if the right voertuig is returned by merk
        /// </summary>
        [TestMethod]
        public void FilterVoertuigByMerkTest()
        {
            // Arrange
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>();

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(voertuigMock.Object);

            voertuigMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyVoertuigCollection());

            VoertuigenSearchCriteria zoekCriteria = new VoertuigenSearchCriteria
            {
                Merk = "Volkswagen"
            };

            // Act
            var result = handler.GetVoertuigBy(zoekCriteria);

            // Assert
            Assert.AreEqual(2, result.ToArray().Length);
        }

        /// <summary>
        /// Test if the right voertuig is returned by type
        /// </summary>
        [TestMethod]
        public void FilterVoertuigByTypeTest()
        {
            // Arrange
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>();

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(voertuigMock.Object);

            voertuigMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyVoertuigCollection());

            VoertuigenSearchCriteria zoekCriteria = new VoertuigenSearchCriteria
            {
                Type = "C3"
            };

            // Act
            var result = handler.GetVoertuigBy(zoekCriteria);

            // Assert
            Assert.AreEqual(1, result.ToArray().Length);
        }

        /// <summary>
        /// Test if the right voertuig is returned by ID
        /// </summary>
        [TestMethod]
        public void FilterVoertuigByIDTest()
        {
            // Arrange
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>();

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(voertuigMock.Object);

            voertuigMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyVoertuigCollection());

            VoertuigenSearchCriteria zoekCriteria = new VoertuigenSearchCriteria
            {
                Type = "C3",
                ID = 3,
            };

            // Act
            var result = handler.GetVoertuigBy(zoekCriteria);

            // Assert
            Assert.AreEqual(1, result.ToArray().Length);
        }

        /// <summary>
        /// Test if the right voertuig is returned by ID
        /// </summary>
        [TestMethod]
        public void GetAllKlantenTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object,leaseMock.Object,null);

            persoonMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyPersoonCollection());
            leaseMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyLeasemaatschappijCollection());

            // Act
            var result = handler.GetAllKlanten();

            // Assert
            Assert.AreEqual(3, result.ToArray().Length);
        }
    }
}
