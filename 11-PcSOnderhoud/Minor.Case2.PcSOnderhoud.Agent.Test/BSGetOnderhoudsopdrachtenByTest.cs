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
    public class BSGetOnderhoudsopdrachtenByTest
    {
        [TestMethod]
        public void GetOnderhoudsopdrachtenByHappyFlowTest()
        {
            //Arrange
            var onderhoudsopdrachten = new AgentSchema.OnderhoudsopdrachtenCollection();
            onderhoudsopdrachten.Add(new AgentSchema.Onderhoudsopdracht());
            onderhoudsopdrachten.Add(new AgentSchema.Onderhoudsopdracht());
            onderhoudsopdrachten.Add(new AgentSchema.Onderhoudsopdracht());
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetOnderhoudsopdrachtenBy(It.IsAny<AgentSchema.OnderhoudsopdrachtZoekCriteria>())).Returns(onderhoudsopdrachten);

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                Onderhoudsomschrijving = "Uitlaat kapot"
            };

            //Act
            var result = agent.GetOnderhoudsopdrachtenBy(searchCriteria);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.GetOnderhoudsopdrachtenBy(It.IsAny<AgentSchema.OnderhoudsopdrachtZoekCriteria>()), Times.Once());
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void GetOnderhoudsopdrachtenByThrowsFuncExcTest()
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
            serviceMock.Setup(service => service.GetOnderhoudsopdrachtenBy(It.IsAny<AgentSchema.OnderhoudsopdrachtZoekCriteria>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                Onderhoudsomschrijving = "Uitlaat kapot"
            };

            //Act
            agent.GetOnderhoudsopdrachtenBy(searchCriteria);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void GetOnderhoudsopdrachtenByThrowsFuncExcMessageTest()
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
            serviceMock.Setup(service => service.GetOnderhoudsopdrachtenBy(It.IsAny<AgentSchema.OnderhoudsopdrachtZoekCriteria>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                Onderhoudsomschrijving = "Uitlaat kapot"
            };

            try
            {
                //Act
                agent.GetOnderhoudsopdrachtenBy(searchCriteria);
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
        public void GetOnderhoudsopdrachtenByThrowsTechnicalExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetOnderhoudsopdrachtenBy(It.IsAny<AgentSchema.OnderhoudsopdrachtZoekCriteria>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            var searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                Onderhoudsomschrijving = "Uitlaat kapot"
            };

            //Act
            agent.GetOnderhoudsopdrachtenBy(searchCriteria);

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void GetOnderhoudsopdrachtenByThrowsTechnicalExceptionAndLogsExceptionTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetOnderhoudsopdrachtenBy(It.IsAny<AgentSchema.OnderhoudsopdrachtZoekCriteria>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            var searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                Onderhoudsomschrijving = "Uitlaat kapot"
            };

            //Act
            try
            {
                agent.GetOnderhoudsopdrachtenBy(searchCriteria);
            }
            catch (TechnicalException) { }


            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

    }
}
