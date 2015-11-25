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
    public class BSVoertuigEnKlantbeheerFilterHandlerTests
    {
        

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
