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
    }
}
