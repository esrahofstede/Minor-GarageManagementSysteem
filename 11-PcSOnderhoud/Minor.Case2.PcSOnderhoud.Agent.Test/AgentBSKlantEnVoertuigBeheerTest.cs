﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
    public class AgentBSKlantEnVoertuigBeheerTest
    {
        [TestMethod]
        public void VoegVoertuigMetKlantToeHappyFlowTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegVoertuigMetKlantToe(It.IsAny<AgentSchema.Voertuig>()));

            var agent = new AgentBSKlantEnVoertuigBeheer(factoryMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };

            //Act
            agent.VoegVoertuigMetKlantToe(voertuig);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.VoegVoertuigMetKlantToe(It.IsAny<AgentSchema.Voertuig>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void VoegVoertuigMetKlantToeThrowsFuncExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            FunctionalErrorDetail error = new FunctionalErrorDetail
            {
                Message = "Deze error wordt gegooid door de BS"
            };
            FunctionalErrorDetail[] details = new[] {error,};
            serviceMock.Setup(service => service.VoegVoertuigMetKlantToe(It.IsAny<AgentSchema.Voertuig>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSKlantEnVoertuigBeheer(factoryMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };

            //Act
            agent.VoegVoertuigMetKlantToe(voertuig);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void VoegVoertuigMetKlantToeThrowsFuncExcMessageTest()
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
            serviceMock.Setup(service => service.VoegVoertuigMetKlantToe(It.IsAny<AgentSchema.Voertuig>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSKlantEnVoertuigBeheer(factoryMock.Object);
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };

            try
            {
                //Act
                agent.VoegVoertuigMetKlantToe(voertuig);
            }
            catch (FunctionalException ex)
            {
                //Assert
                Assert.AreEqual(true, ex.Errors.HasErrors);
                Assert.AreEqual(error.Message, ex.Errors.Details[0].Message);
            }

            
        }
    }
}
