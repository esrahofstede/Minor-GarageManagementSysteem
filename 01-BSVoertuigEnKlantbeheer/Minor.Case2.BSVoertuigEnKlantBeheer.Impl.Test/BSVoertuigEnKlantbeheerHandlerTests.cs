using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using Moq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

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
            persoonMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Persoon, bool>>>())).Returns(new List<Entities.Persoon>());
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);
            voertuigMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Voertuig, bool>>>())).Returns(new List<Entities.Voertuig>());


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
            persoonMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Persoon, bool>>>())).Returns(new List<Entities.Persoon>());
            leaseMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Leasemaatschappij>())).Returns(2);
            leaseMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Leasemaatschappij, bool>>>())).Returns(new List<Entities.Leasemaatschappij>());
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);
            voertuigMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Voertuig, bool>>>())).Returns(new List<Entities.Voertuig>());


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
        /// Test if the method returns klanten
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

        /// <summary>
        /// Test if the returns leasemaatschappijen
        /// </summary>
        [TestMethod]
        public void GetAllLeasemaatschappijenTest()
        {
            // Arrange
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(null, leaseMock.Object, null);

            leaseMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyLeasemaatschappijCollection());

            // Act
            var result = handler.GetAllLeasemaatschappijen();

            // Assert
            Assert.AreEqual(1, result.ToArray().Length);
        }

        /// <summary>
        /// Test if the method returns the good exception when null is passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException<All.V1.Schema.FunctionalErrorDetail[]>))]
        public void UpdateVoertuigWithNullTest()
        {
            // Arrange
            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler();
            // Act
            handler.UpdateVoertuig(null);

            // Assert

        }

        /// <summary>
        /// Test if the method returns the good exception when null is passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException<All.V1.Schema.FunctionalErrorDetail[]>))]
        public void InsertVoertuigWithNullTest()
        {
            // Arrange
            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler();
            // Act
            handler.VoegVoertuigMetKlantToe(null);

            // Assert
        }

        /// <summary>
        /// Test if the method returns the good exception when null is passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException<All.V1.Schema.FunctionalErrorDetail[]>))]
        public void InsertOnderhoudsopdrachtWithNullTest()
        {
            // Arrange
            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler();
            // Act
            handler.VoegOnderhoudsopdrachtToe(null);

            // Assert
        }

        /// <summary>
        /// Test if the method returns the good exception when null is passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException<All.V1.Schema.FunctionalErrorDetail[]>))]
        public void InsertOnderhoudswerkzaamhedenWithNullTest()
        {
            // Arrange
            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler();
            // Act
            handler.VoegOnderhoudswerkzaamhedenToe(null);

            // Assert
        }

        /// <summary>
        /// Test if the right onderhoudsopdrachten are returned by voertuig kenteken
        /// </summary>
        [TestMethod]
        public void FilterOnderhoudsopdrachtByVoertuigKentekenTest()
        {
            // Arrange
            var onderhoudsopdrachtMock = new Mock<IDataMapper<Entities.Onderhoudsopdracht, long>>();

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(onderhoudsopdrachtMock.Object);

            onderhoudsopdrachtMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyOnderhoudsopdrachtenCollection);

            OnderhoudsopdrachtZoekCriteria zoekCriteria = new OnderhoudsopdrachtZoekCriteria
            {
                VoertuigenSearchCriteria = new VoertuigenSearchCriteria
                {
                    Kenteken = "GG-WP-13"
                },
            };

            // Act
            var result = handler.GetOnderhoudsopdrachtenBy(zoekCriteria);

            // Assert
            Assert.AreEqual(2, result.ToArray().Length);
        }
    }
}
