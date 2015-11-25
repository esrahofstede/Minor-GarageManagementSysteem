using System;
using System.ServiceModel;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Minor.ServiceBus.Agent.Implementation;
using Moq;

namespace Minor.Case2.PcSOnderhoud.Agent.Tests
{
    [TestClass]
    public class BSGetVoertuigByTest
    {
        [TestMethod]
        public void GetVoertuigByHappyFlowTest()
        {
            //Arrange
            var voertuigen = new AgentSchema.VoertuigenCollection();
            voertuigen.Add(new AgentSchema.Voertuig());
            voertuigen.Add(new AgentSchema.Voertuig());
            voertuigen.Add(new AgentSchema.Voertuig());
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetVoertuigBy(It.IsAny<AgentSchema.VoertuigenSearchCriteria>())).Returns(voertuigen);

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var searchCriteria = new Schema.VoertuigenSearchCriteria
            {
                Merk = "Ford",
            };

            //Act
            var result = agent.GetVoertuigBy(searchCriteria);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.GetVoertuigBy(It.IsAny<AgentSchema.VoertuigenSearchCriteria>()), Times.Once());
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void GetVoertuigByThrowsFuncExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            FunctionalErrorDetail error = new FunctionalErrorDetail
            {
                Message = "Deze error wordt gegooid door de BS"
            };
            FunctionalErrorDetail[] details = new[] { error, };
            serviceMock.Setup(service => service.GetVoertuigBy(It.IsAny<AgentSchema.VoertuigenSearchCriteria>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var voertuig = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            //Act
            agent.GetVoertuigBy(voertuig);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void GetVoertuigByThrowsFuncExcMessageTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            FunctionalErrorDetail error = new FunctionalErrorDetail
            {
                Message = "Deze error wordt gegooid door de BS"
            };
            FunctionalErrorDetail[] details = new[] { error, };
            serviceMock.Setup(service => service.GetVoertuigBy(It.IsAny<AgentSchema.VoertuigenSearchCriteria>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var voertuig = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            try
            {
                //Act
                agent.GetVoertuigBy(voertuig);
            }
            catch (FunctionalException ex)
            {
                //Assert
                Assert.AreEqual(true, ex.Errors.HasErrors);
                Assert.AreEqual(error.Message, ex.Errors.Details[0].Message);
            }


        }

        [TestMethod]
        [ExpectedException(typeof(TechnicalException))]
        public void GetVoertuigByThrowsTechnicalExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetVoertuigBy(It.IsAny<AgentSchema.VoertuigenSearchCriteria>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            var voertuig = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            //Act
            agent.GetVoertuigBy(voertuig);

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void GetVoertuigByThrowsTechnicalExceptionAndLogsExceptionTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetVoertuigBy(It.IsAny<AgentSchema.VoertuigenSearchCriteria>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            var voertuig = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            //Act
            try
            {
                agent.GetVoertuigBy(voertuig);
            }
            catch (TechnicalException) { }


            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

    }
}
