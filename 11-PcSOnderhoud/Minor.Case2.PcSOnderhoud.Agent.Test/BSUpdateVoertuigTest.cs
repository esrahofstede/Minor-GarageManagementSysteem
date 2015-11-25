﻿using System;
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
    public class BSUpdateVoertuigTest
    {
        [TestMethod]
        public void UpdateVoertuigHappyFlowTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.UpdateVoertuig(It.IsAny<AgentSchema.Voertuig>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            //Act
            agent.UpdateVoertuig(voertuig);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.UpdateVoertuig(It.IsAny<AgentSchema.Voertuig>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void UpdateVoertuigThrowsFuncExcTest()
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
            serviceMock.Setup(service => service.UpdateVoertuig(It.IsAny<AgentSchema.Voertuig>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Eigenaar = new Schema.Persoon(),
                Bestuurder = new Schema.Persoon()
            };

            //Act
            agent.UpdateVoertuig(voertuig);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void UpdateVoertuigThrowsFuncExcMessageTest()
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
            serviceMock.Setup(service => service.UpdateVoertuig(It.IsAny<AgentSchema.Voertuig>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Eigenaar = new Schema.Persoon(),
                Bestuurder = new Schema.Persoon()
            };

            try
            {
                //Act
                agent.UpdateVoertuig(voertuig);
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
        public void UpdateVoertuigThrowsTechnicalExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.UpdateVoertuig(It.IsAny<AgentSchema.Voertuig>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Eigenaar = new Schema.Persoon(),
                Bestuurder = new Schema.Persoon()
            };

            //Act
            agent.UpdateVoertuig(voertuig);

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void UpdateVoertuigThrowsTechnicalExceptionAndLogsExceptionTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.UpdateVoertuig(It.IsAny<AgentSchema.Voertuig>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Eigenaar = new Schema.Persoon(),
                Bestuurder = new Schema.Persoon()
            };

            //Act
            try
            {
                agent.UpdateVoertuig(voertuig);
            }
            catch (TechnicalException) { }


            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

    }
}
