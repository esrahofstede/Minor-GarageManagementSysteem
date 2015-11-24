using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Moq;
using Minor.ServiceBus.Agent.Implementation;

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
            agent.AddOnderhoudsOpdrachtWithKlantAndVoertuig(onderhoudsopdracht);

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
        public void MeldVoertuigKlaarWithoutSteekproefTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.MeldVoertuigKlaar(It.IsAny<Voertuig>())).Returns(false);
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);
            var voertuig = DummyData.GetDummyVoertuig();

            //Act
            var resultSteekproef = agent.MeldVoertuigKlaar(voertuig);

            //Assert
            Assert.IsFalse(resultSteekproef);
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.MeldVoertuigKlaar(It.IsAny<Voertuig>()));
        }


        [TestMethod]
        public void MeldVoertuigKlaarWithSteekproefTest()
        {
            //Arrange
            var serviceMock = new Mock<IPcSOnderhoudService>(MockBehavior.Strict);
            serviceMock.Setup(service => service.MeldVoertuigKlaar(It.IsAny<Voertuig>())).Returns(true);
            var factoryMock = new Mock<ServiceFactory<IPcSOnderhoudService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);

            AgentPcSOnderhoud agent = new AgentPcSOnderhoud(factoryMock.Object);
            var voertuig = DummyData.GetDummyVoertuig();

            //Act
            var resultSteekproef = agent.MeldVoertuigKlaar(voertuig);

            //Assert
            Assert.IsTrue(resultSteekproef);
            factoryMock.Verify(factory => factory.CreateAgent());
            serviceMock.Verify(service => service.MeldVoertuigKlaar(It.IsAny<Voertuig>()));
        }
    }
}
