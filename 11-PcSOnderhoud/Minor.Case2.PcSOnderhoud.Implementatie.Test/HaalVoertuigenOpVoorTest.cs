using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Minor.Case2.Exceptions.V1.Schema;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.PcSOnderhoud.Agent;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using Moq;

namespace Minor.Case2.PcSOnderhoud.Implementation.Tests
{
    [TestClass]
    public class HaalVoertuigenOpVoorTest
    {
        [TestMethod]
        public void ReturnsVoertuigenCollection()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };
            
            var personenCollection = new Schema.KlantenCollection
            {
                persoon,
                new Schema.Persoon
                {
                    Voornaam = "Esra",
                    Achternaam = "Hofstede",
                    Telefoonnummer = "069876543"
                },
                new Schema.Persoon
                {
                    Voornaam = "Caspar",
                    Achternaam = "Eldermans",
                    Telefoonnummer = "0645678912"
                },
            };
            var voertuigencollection = new Schema.VoertuigenCollection
            {
                new Schema.Voertuig(),
                new Schema.Voertuig(),
                new Schema.Voertuig()
            };

            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Returns(personenCollection);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(voertuigencollection);

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            Assert.AreEqual(typeof(Schema.VoertuigenCollection), result.GetType());
        }

        [TestMethod]
        public void ReturnCorrectDataWithVoertuigen()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };

            var personenCollection = new Schema.KlantenCollection
            {
                persoon,
                new Schema.Persoon
                {
                    Voornaam = "Esra",
                    Achternaam = "Hofstede",
                    Telefoonnummer = "069876543"
                },
                new Schema.Persoon
                {
                    Voornaam = "Caspar",
                    Achternaam = "Eldermans",
                    Telefoonnummer = "0645678912"
                },
            };
            var voertuigencollection = new Schema.VoertuigenCollection
            {
                new Schema.Voertuig(),
                new Schema.Voertuig(),
                new Schema.Voertuig()
            };

            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Returns(personenCollection);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(voertuigencollection);

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void ReturnCorrectDataWithoutVoertuigen()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };

            var personenCollection = new Schema.KlantenCollection
            {
                persoon,
                new Schema.Persoon
                {
                    Voornaam = "Esra",
                    Achternaam = "Hofstede",
                    Telefoonnummer = "069876543"
                },
                new Schema.Persoon
                {
                    Voornaam = "Caspar",
                    Achternaam = "Eldermans",
                    Telefoonnummer = "0645678912"
                },
            };
            
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Returns(personenCollection);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(new Schema.VoertuigenCollection());

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ReturnCorrectDataWithoutFoundPerson()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };

            var personenCollection = new Schema.KlantenCollection
            {
                new Schema.Persoon
                {
                    Voornaam = "Esra",
                    Achternaam = "Hofstede",
                    Telefoonnummer = "069876543"
                },
                new Schema.Persoon
                {
                    Voornaam = "Caspar",
                    Achternaam = "Eldermans",
                    Telefoonnummer = "0645678912"
                },
            };

            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Returns(personenCollection);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(new Schema.VoertuigenCollection());

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ReturnCorrectDataFoundMultiplePersonen()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };

            var personenCollection = new Schema.KlantenCollection
            {
                persoon,
                persoon,
                persoon
            };

            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Returns(personenCollection);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(new Schema.VoertuigenCollection());

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            Assert.AreEqual(null, result);
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException<FunctionalErrorDetail[]>))]
        public void ThrowsFunctionalErrorDetailArrayException()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(new Schema.VoertuigenCollection());
            var error = new FunctionalErrorDetail();
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsFunctionalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail {Message = "error gegooid"};
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.HaalVoertuigenOpVoor(persoon);
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                //Assert   
                Assert.AreEqual(1, ex.Detail.Length);
                Assert.AreEqual(error.Message, ex.Detail[0].Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalErrorDetailArrayException()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(
                new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(persoon);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var persoon = new Schema.Persoon
            {
                Voornaam = "Pim",
                Achternaam = "Verheij",
                Telefoonnummer = "0612345678"
            };
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(
                 new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                var result = target.HaalVoertuigenOpVoor(persoon);
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("error", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalExceptionIfPersoonIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(
                new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.HaalVoertuigenOpVoor(null);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalExceptionIfPersoonIsNullExceptionMessage()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(
                 new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.HaalVoertuigenOpVoor(null);
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("Persoon mag niet null zijn", ex.Message);
            }
        }
    }
}
