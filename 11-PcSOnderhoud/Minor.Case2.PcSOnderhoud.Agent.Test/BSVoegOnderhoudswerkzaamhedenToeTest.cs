using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
    public class BSVoegOnderhoudswerkzaamhedenToeTest
    {
        [TestMethod]
        public void VoegOnderhoudswerkzaamhedenToeHappyFlowTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<AgentSchema.Onderhoudswerkzaamheden>()));
            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Kilometerstand = 10000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                     APK = true,
                     Kilometerstand = 10000,
                     Onderhoudsomschrijving = "uitlaat kapot",
                     Aanmeldingsdatum = DateTime.Now,
                }
            };

            //Act
            agent.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<AgentSchema.Onderhoudswerkzaamheden>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void VoegOnderhoudswerkzaamhedenToeThrowsFuncExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            FunctionalErrorDetail error = new FunctionalErrorDetail
            {
                Message = "Deze error wordt gegooid door de BS"
            };
            FunctionalErrorDetail[] details = new[] {error,};
            serviceMock.Setup(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<AgentSchema.Onderhoudswerkzaamheden>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Kilometerstand = 10000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                    APK = true,
                    Kilometerstand = 10000,
                    Onderhoudsomschrijving = "uitlaat kapot",
                    Aanmeldingsdatum = DateTime.Now,
                }
            };

            //Act
            agent.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void VoegOnderhoudswerkzaamhedenToeThrowsFuncExcMessageTest()
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
            serviceMock.Setup(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<AgentSchema.Onderhoudswerkzaamheden>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Kilometerstand = 10000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                    APK = true,
                    Kilometerstand = 10000,
                    Onderhoudsomschrijving = "uitlaat kapot",
                    Aanmeldingsdatum = DateTime.Now,
                }
            };

            try
            {
                //Act
                agent.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);
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
        public void VoegOnderhoudswerkzaamhedenToeThrowsTechnicalExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<AgentSchema.Onderhoudswerkzaamheden>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Kilometerstand = 10000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                    APK = true,
                    Kilometerstand = 10000,
                    Onderhoudsomschrijving = "uitlaat kapot",
                    Aanmeldingsdatum = DateTime.Now,
                }
            };

            //Act
            agent.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void VoegOnderhoudswerkzaamhedenToeThrowsTechnicalExceptionAndLogsExceptionTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegOnderhoudswerkzaamhedenToe(It.IsAny<AgentSchema.Onderhoudswerkzaamheden>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Kilometerstand = 10000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                    APK = true,
                    Kilometerstand = 10000,
                    Onderhoudsomschrijving = "uitlaat kapot",
                    Aanmeldingsdatum = DateTime.Now,
                }
            };

            //Act
            try
            {
                agent.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);
            }
            catch (TechnicalException){}
            

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

    }
}
