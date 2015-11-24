using System;

using Minor.ServiceBus.Agent.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Messages.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent.Tests
{
    [TestClass]
    public class AgentISRDWTest
    {
        [TestMethod]
        public void RDWAgentHasSendReqMethodTest()
        {
            //Arrange
            AgentISRDW agent = new AgentISRDW();

            //Act
            var result = agent.SendAPKKeuringsverzoek(new AgentSchema.Voertuig(), new Garage(), new Keuringsverzoek());

            //Assert
            Assert.AreEqual(typeof(SendRdwKeuringsverzoekResponseMessage), result.GetType());
        }


        [TestMethod]
        public void RDWAgentSendReq_CallsFactory_Test()
        {
            var serviceMock = new Mock<IISRDWService>();
            var factoryMock = new Mock<ServiceFactory<IISRDWService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            //Arrange
            AgentISRDW agent = new AgentISRDW(factoryMock.Object);

            //Act
            agent.SendAPKKeuringsverzoek(new AgentSchema.Voertuig(), new Garage(), new Keuringsverzoek());

            //Assert
            Assert.IsTrue(true);
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            //Test kan niet worden uitgevoerd omdat de CreateAgent method niet virtual is.
        }

        [TestMethod]
        public void RDWAgentSendReq_ServiceGetsCalled_Test()
        {
            //Arrange
            var keuringsverzoek = new SendRdwKeuringsverzoekRequestMessage();
            var serviceMock = new Mock<IISRDWService>();
            serviceMock
                .Setup(service => service
                    .RequestKeuringsverzoek(It.IsAny<SendRdwKeuringsverzoekRequestMessage>()))
                .Returns(new SendRdwKeuringsverzoekResponseMessage());
            var factoryMock = new Mock<ServiceFactory<IISRDWService>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            
            var agent = new AgentISRDW(factoryMock.Object);

            //Act
            agent.SendAPKKeuringsverzoek(new AgentSchema.Voertuig(), new Garage(), new Keuringsverzoek());

            //Assert
            Assert.IsTrue(true);
            serviceMock.Verify(service => service.RequestKeuringsverzoek(It.IsAny<SendRdwKeuringsverzoekRequestMessage>()), Times.Once());
        }

        [Ignore]
        [TestMethod]
        public void IntegratieTest()
        {
            //Arrange
            var keuringsreq = new SendRdwKeuringsverzoekRequestMessage();
            var voertuig = new AgentSchema.Voertuig
            {
                Kenteken = "12-AA-AA",
                Merk = "ford",
                Type = "focus",
                Eigenaar = new AgentSchema.Persoon
                {
                    Voornaam = "Jan",
                    Achternaam = "Jansen"
                },
                Bestuurder = new AgentSchema.Persoon
                {
                    Voornaam = "Jan",
                    Achternaam = "Jansen"
                }
            };
            var garage = new Garage
            {
                Kvk = "1234 1234"
            };
            var keuringsverzoek = new Keuringsverzoek
            {
                Date = DateTime.Now,
                CorrolatieId = Guid.NewGuid().ToString()
            };

            AgentISRDW agent = new AgentISRDW();

            //Act
            var result = agent.SendAPKKeuringsverzoek(voertuig, garage, keuringsverzoek);

            //Assert
            Assert.IsTrue(true);
        }

    }
}
