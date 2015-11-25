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
    public class BSGetAllPersonenTest
    {
        [TestMethod]
        public void GetAllPersonenHappyFlowTest()
        {
            //Arrange
            var personen = new AgentSchema.KlantenCollection();
            personen.Add(new AgentSchema.Persoon());
            personen.Add(new AgentSchema.Persoon());
            personen.Add(new AgentSchema.Persoon());
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetAllPersonen()).Returns(personen);

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);

            //Act
            var result = agent.GetAllPersonen();

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.GetAllPersonen(), Times.Once());
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void GetAllPersonenThrowsFuncExcTest()
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
            serviceMock.Setup(service => service.GetAllPersonen()).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);

            //Act
            agent.GetAllPersonen();

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void GetAllPersonenThrowsFuncExcMessageTest()
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
            serviceMock.Setup(service => service.GetAllPersonen()).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            
            try
            {
                //Act
                agent.GetAllPersonen();
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
        public void GetAllPersonenThrowsTechnicalExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetAllPersonen()).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            
            //Act
            agent.GetAllPersonen();

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void GetAllPersonenThrowsTechnicalExceptionAndLogsExceptionTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.GetAllPersonen()).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);

            //Act
            try
            {
                agent.GetAllPersonen();
            }
            catch (TechnicalException) { }


            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

    }
}
