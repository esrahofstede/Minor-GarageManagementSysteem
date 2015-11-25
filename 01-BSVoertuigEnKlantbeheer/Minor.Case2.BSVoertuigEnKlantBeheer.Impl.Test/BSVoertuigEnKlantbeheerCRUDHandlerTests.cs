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
    public class BSVoertuigEnKlantbeheerCRUDHandlerTests
    {
        /// <summary>
        /// Test if the method returns klanten
        /// </summary>
        [TestMethod]
        public void GetAllPersonenTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object, null, null);

            persoonMock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetDummyPersoonCollection());

            // Act
            var result = handler.GetAllPersonen();

            // Assert
            Assert.AreEqual(2, result.ToArray().Length);
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
        /// Test if the right ID is set for the Klant and Eigenaar
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetNewPersoonTest()
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
        /// Test if the right ID is set for the existing Klant
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetExistingPersoonTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object, leaseMock.Object, voertuigMock.Object);

            persoonMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Persoon>())).Returns(1);
            persoonMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Persoon, bool>>>())).Returns(new List<Entities.Persoon>() { new Entities.Persoon { ID = 10 } });
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);
            voertuigMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Voertuig, bool>>>())).Returns(new List<Entities.Voertuig>());


            Voertuig voertuig = new Voertuig
            {
                Kenteken = "12-AA-AB",
                Bestuurder = new Persoon()
                {
                    ID = 10,
                },
                Eigenaar = new Persoon()
                {
                    ID = 10,
                },
                Merk = "Ford",
                Type = "Focus",
            };


            // Act
            handler.VoegVoertuigMetKlantToe(voertuig);

            // Assert
            voertuigMock.Verify(mock => mock.Insert((It.Is<Entities.Voertuig>(v => v.Bestuurder.ID == 10 && v.Eigenaar.ID == 10))));
        }

        /// <summary>
        /// Test if the right ID is set for the existing Leasemaatschappij
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetExistingLeasemaatschappijTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object, leaseMock.Object, voertuigMock.Object);

            persoonMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Persoon>())).Returns(1);
            persoonMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Persoon, bool>>>())).Returns(new List<Entities.Persoon>());
            leaseMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Leasemaatschappij, bool>>>())).Returns(new List<Entities.Leasemaatschappij>() { new Entities.Leasemaatschappij { ID = 20 } });
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);
            voertuigMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Voertuig, bool>>>())).Returns(new List<Entities.Voertuig>());


            Voertuig voertuig = new Voertuig
            {
                Kenteken = "12-AA-AA",
                Bestuurder = new Persoon(),
                Eigenaar = new Leasemaatschappij
                {
                    ID = 20
                },
                Merk = "Ford",
                Type = "Focus",
            };

            // Act
            handler.VoegVoertuigMetKlantToe(voertuig);

            // Assert
            voertuigMock.Verify(mock => mock.Insert((It.Is<Entities.Voertuig>(v => v.Bestuurder.ID == 1 && v.Eigenaar.ID == 20))));
        }

        /// <summary>
        /// Test if the right ID is set for the existing bestuurder and Leasemaatschappij
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetExistingPersoonAndExistingLeasemaatschappijTest()
        {
            // Arrange
            var persoonMock = new Mock<IDataMapper<Entities.Persoon, long>>(MockBehavior.Strict);
            var leaseMock = new Mock<IDataMapper<Entities.Leasemaatschappij, long>>(MockBehavior.Strict);
            var voertuigMock = new Mock<IDataMapper<Entities.Voertuig, long>>(MockBehavior.Strict);

            BSVoertuigEnKlantbeheerHandler handler = new BSVoertuigEnKlantbeheerHandler(persoonMock.Object, leaseMock.Object, voertuigMock.Object);

            persoonMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Persoon, bool>>>())).Returns(new List<Entities.Persoon>() { new Entities.Persoon { ID = 10 } });
            leaseMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Leasemaatschappij, bool>>>())).Returns(new List<Entities.Leasemaatschappij>() { new Entities.Leasemaatschappij { ID = 20 } });
            voertuigMock.Setup(datamapper => datamapper.Insert(It.IsAny<Entities.Voertuig>())).Returns(0);
            voertuigMock.Setup(datamapper => datamapper.FindAllBy(It.IsAny<Expression<Func<Entities.Voertuig, bool>>>())).Returns(new List<Entities.Voertuig>());


            Voertuig voertuig = new Voertuig
            {
                Kenteken = "12-AA-AA",
                Bestuurder = new Persoon
                {
                    ID = 10,
                }
                    ,
                Eigenaar = new Leasemaatschappij
                {
                    ID = 20
                },
                Merk = "Ford",
                Type = "Focus",
            };

            // Act
            handler.VoegVoertuigMetKlantToe(voertuig);

            // Assert
            voertuigMock.Verify(mock => mock.Insert((It.Is<Entities.Voertuig>(v => v.Bestuurder.ID == 10 && v.Eigenaar.ID == 20))));
        }

        /// <summary>
        /// Test if the right persoonID and leasemaatschappijID are set
        /// </summary>
        [TestMethod]
        public void InsertVoertuigMetNewPersoonEnNewLeasemaatschappijTest()
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
    }
}
