using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Moq;
using Minor.ServiceBus.Agent.Implementation;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema;
using System.Linq;

namespace Minor.Case2.FEGMS.Agent.Tests
{
    [TestClass]
    public class AgentPcSOnderhoudTest
    {
        [TestMethod]
        public void AddOnderhoudsOpdrachtWithKlantAndVoertuigTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<Onderhoudsopdracht>()));
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);
            var onderhoudsopdracht = DummyData.GetDummyOnderhoudsopdracht();

            //Act
            agent.AddOnderhoudsopdrachtWithKlantAndVoertuig(onderhoudsopdracht);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<Onderhoudsopdracht>()));
        }

        [TestMethod]
        public void VoegVoertuigMetKlantToeTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);
            var voertuig = DummyData.GetDummyVoertuig();

            //Act
            agent.VoegVoertuigMetKlantToe(voertuig);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
        }

        [TestMethod]
        public void FindVoertuigByTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>())).Returns(DummyData.GetVoertuigenCollection());
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);
            var searchCriteria = DummyData.GetSearchCriteria();

            //Act
            agent.GetVoertuigBy(searchCriteria);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>()));
        }


        [TestMethod]
        public void GetAllLeasemaatschappijenTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.GetAllLeasemaatschappijen()).Returns(DummyData.GetAllLeasemaatschappijen());
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);

            //Act
            var leasemaatschappijen = agent.GetAllLeasemaatschappijen();

            //Assert
            Assert.AreEqual(3, leasemaatschappijen.Count);
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.GetAllLeasemaatschappijen());
        }

        [TestMethod]
        public void GetOnderhoudsopdrachtByTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.GetHuidigeOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>())).Returns(DummyData.GetDummyOnderhoudsopdracht());
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);

            //Act
            var onderhoudsopdracht = agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>());

            //Assert
            Assert.AreEqual(12000, onderhoudsopdracht.Kilometerstand);
            Assert.AreEqual("APK Keuren", onderhoudsopdracht.Onderhoudsomschrijving);
            Assert.AreEqual(1, onderhoudsopdracht.ID);
            Assert.IsTrue(onderhoudsopdracht.APK);
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.GetHuidigeOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>()));
        }

        [TestMethod]
        public void VoegOnderhoudswerkzaamhedenToeTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>(), It.IsAny<Garage>())).Returns(true);
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);

            //Act
            var result = agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>());

            //Assert
            Assert.IsTrue(result.Value);
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>(), It.IsAny<Garage>()));
        }

        [TestMethod]
        public void HaalVoertuigenOpVoorTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.HaalVoertuigenOpVoor(It.IsAny<Persoon>())).Returns(DummyData.GetVoertuigenCollection());
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);

            //Act
            var voertuigen = agent.HaalVoertuigenOpVoor(It.IsAny<Persoon>());

            //Assert
            Assert.AreEqual(1, voertuigen.Count);
            Assert.AreEqual("DS-344-S", voertuigen.First().Kenteken);
            
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.HaalVoertuigenOpVoor(It.IsAny<Persoon>()));
        }
    }
}
