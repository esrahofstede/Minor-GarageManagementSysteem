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
    public class BSVoegOnderhoudsopdrachtToeTest
    {
        [TestMethod]
        public void VoegOnderhoudsopdrachtToeHappyFlowTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>(MockBehavior.Strict);
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<AgentSchema.Onderhoudsopdracht>()));
            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                APK = true,
                Kilometerstand = 10000,
                Onderhoudsomschrijving = "uitlaat kapot",
                Aanmeldingsdatum = DateTime.Now,
                Voertuig = new Schema.Voertuig
                {
                    ID = 111111,
                    Kenteken = "14-TT-KJ",
                    Merk = "Ford",
                    Type = "Focus",
                    Eigenaar = new Schema.Persoon(),
                    Bestuurder = new Schema.Persoon()
                }
            };

            //Act
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);

            //Assert
            factoryMock.Verify(factory => factory.CreateAgent(), Times.Once());
            serviceMock.Verify(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<AgentSchema.Onderhoudsopdracht>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void VoegOnderhoudsopdrachtToeThrowsFuncExcTest()
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
            serviceMock.Setup(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<AgentSchema.Onderhoudsopdracht>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                APK = true,
                Kilometerstand = 10000,
                Onderhoudsomschrijving = "uitlaat kapot",
                Aanmeldingsdatum = DateTime.Now,
                Voertuig = new Schema.Voertuig
                {
                    ID = 111111,
                    Kenteken = "14-TT-KJ",
                    Merk = "Ford",
                    Type = "Focus",
                    Eigenaar = new Schema.Persoon(),
                    Bestuurder = new Schema.Persoon()
                }
            };

            //Act
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void VoegOnderhoudsopdrachtToeThrowsFuncExcMessageTest()
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
            serviceMock.Setup(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<AgentSchema.Onderhoudsopdracht>())).Throws(new FaultException<FunctionalErrorDetail[]>(details));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                APK = true,
                Kilometerstand = 10000,
                Onderhoudsomschrijving = "uitlaat kapot",
                Aanmeldingsdatum = DateTime.Now,
                Voertuig = new Schema.Voertuig
                {
                    ID = 111111,
                    Kenteken = "14-TT-KJ",
                    Merk = "Ford",
                    Type = "Focus",
                    Eigenaar = new Schema.Persoon(),
                    Bestuurder = new Schema.Persoon()
                }
            };

            try
            {
                //Act
                agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
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
        public void VoegOnderhoudsopdrachtToeThrowsTechnicalExcTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<AgentSchema.Onderhoudsopdracht>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object);
            var onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                APK = true,
                Kilometerstand = 10000,
                Onderhoudsomschrijving = "uitlaat kapot",
                Aanmeldingsdatum = DateTime.Now,
                Voertuig = new Schema.Voertuig
                {
                    ID = 111111,
                    Kenteken = "14-TT-KJ",
                    Merk = "Ford",
                    Type = "Focus",
                    Eigenaar = new Schema.Persoon(),
                    Bestuurder = new Schema.Persoon()
                }
            };

            //Act
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void VoegOnderhoudsopdrachtToeThrowsTechnicalExceptionAndLogsExceptionTest()
        {
            //Arrange
            var serviceMock = new Mock<IBSVoertuigEnKlantbeheer>();
            var factoryMock = new Mock<ServiceFactory<IBSVoertuigEnKlantbeheer>>(MockBehavior.Strict);
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            factoryMock.Setup(factory => factory.CreateAgent()).Returns(serviceMock.Object);
            serviceMock.Setup(service => service.VoegOnderhoudsopdrachtToe(It.IsAny<AgentSchema.Onderhoudsopdracht>())).Throws(new InvalidOperationException());
            logMock.Setup(log => log.Fatal(It.IsAny<string>()));

            var agent = new AgentBSVoertuigEnKlantBeheer(factoryMock.Object, logMock.Object);
            var onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                APK = true,
                Kilometerstand = 10000,
                Onderhoudsomschrijving = "uitlaat kapot",
                Aanmeldingsdatum = DateTime.Now,
                Voertuig = new Schema.Voertuig
                {
                    ID = 111111,
                    Kenteken = "14-TT-KJ",
                    Merk = "Ford",
                    Type = "Focus",
                    Eigenaar = new Schema.Persoon(),
                    Bestuurder = new Schema.Persoon()
                }
            };

            //Act
            try
            {
                agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
            }
            catch (TechnicalException){}
            

            //Assert
            logMock.Verify(service => service.Fatal(It.IsAny<string>()), Times.Once());
        }

    }
}
