using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Messages.Agent;
using AgentSchema = Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.PcSOnderhoud.Agent;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using Moq;


namespace Minor.Case2.PcSOnderhoud.Implementation.Tests
{
    [TestClass]
    public class VoegOnderhoudswerkzaamhedenToe
    {
        [TestMethod]
        public void ReturnsNullableBool()
        {
            //Arrange
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Kilometerstand = 10000,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                    ID = 1,
                    Voertuig = new Schema.Voertuig()
                }
            };

            var voertuigen = new Schema.VoertuigenCollection
            {
                new Schema.Voertuig
                {
                    ID = 1,
                    Bestuurder = new Schema.Persoon(),
                    Eigenaar = new Schema.Persoon()
                },
                new Schema.Voertuig
                {
                    ID = 1,
                    Bestuurder = new Schema.Persoon(),
                    Eigenaar = new Schema.Persoon()
                },

            };
            var onderhoudsopdrachten = new Schema.OnderhoudsopdrachtenCollection
            {
                new Schema.Onderhoudsopdracht
                {
                    ID = 1,
                    Voertuig = new Schema.Voertuig()
                },
                 new Schema.Onderhoudsopdracht
                {
                    ID = 2,
                    Voertuig = new Schema.Voertuig()
                }
            };
            var agentBSMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var agentISMock = new Mock<IAgentISRDW>(MockBehavior.Strict);
            agentBSMock.Setup(agent => agent.GetOnderhoudsopdrachtenBy(It.IsAny<Schema.OnderhoudsopdrachtZoekCriteria>())).Returns(onderhoudsopdrachten);
            agentBSMock.Setup(agent => agent.UpdateVoertuig(It.IsAny<Schema.Voertuig>()));
            agentBSMock.Setup(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Schema.Onderhoudswerkzaamheden>()));
            agentBSMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(voertuigen);
            agentISMock.Setup(
                agent => agent.SendAPKKeuringsverzoek(It.IsAny<Schema.Voertuig>(), It.IsAny<AgentSchema.Garage>(),
                    It.IsAny<AgentSchema.Keuringsverzoek>())).Returns(new SendRdwKeuringsverzoekResponseMessage
                    {
                        Steekproef = true
                    });

            var target = new PcSOnderhoudServiceHandler(agentBSMock.Object, agentISMock.Object);

            //Act
            var result = target.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden, new AgentSchema.Garage {Kvk = "1324 1345", Naam = "Caespi BV.", Plaats = "Utrecht" });

            //Assert
            
        }

        [TestMethod]
        public void ReturnsNullableBoolWithAPK()
        {
            //Arrange
            var onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                Kilometerstand = 10000,
                Onderhoudsopdracht = new Schema.Onderhoudsopdracht
                {
                    ID = 1,
                    Voertuig = new Schema.Voertuig(),
                    APK = true
                }
            };

            var voertuigen = new Schema.VoertuigenCollection
            {
                new Schema.Voertuig
                {
                    ID = 1,
                    Bestuurder = new Schema.Persoon(),
                    Eigenaar = new Schema.Persoon()
                },
                new Schema.Voertuig
                {
                    ID = 1,
                    Bestuurder = new Schema.Persoon(),
                    Eigenaar = new Schema.Persoon()
                },

            };
            var onderhoudsopdrachten = new Schema.OnderhoudsopdrachtenCollection
            {
                new Schema.Onderhoudsopdracht
                {
                    ID = 1,
                    Voertuig = new Schema.Voertuig(),
                    APK = true
                },
                 new Schema.Onderhoudsopdracht
                {
                    ID = 2,
                    Voertuig = new Schema.Voertuig(),
                    APK = true
                }
            };
            var agentBSMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var agentISMock = new Mock<IAgentISRDW>(MockBehavior.Strict);
            agentBSMock.Setup(agent => agent.GetOnderhoudsopdrachtenBy(It.IsAny<Schema.OnderhoudsopdrachtZoekCriteria>())).Returns(onderhoudsopdrachten);
            agentBSMock.Setup(agent => agent.UpdateVoertuig(It.IsAny<Schema.Voertuig>()));
            agentBSMock.Setup(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Schema.Onderhoudswerkzaamheden>()));
            agentBSMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(voertuigen);
            agentISMock.Setup(
                agent => agent.SendAPKKeuringsverzoek(It.IsAny<Schema.Voertuig>(), It.IsAny<AgentSchema.Garage>(),
                    It.IsAny<AgentSchema.Keuringsverzoek>())).Returns(new SendRdwKeuringsverzoekResponseMessage
                    {
                        Steekproef = true
                    });

            var target = new PcSOnderhoudServiceHandler(agentBSMock.Object, agentISMock.Object);

            //Act
            var result = target.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden, new AgentSchema.Garage { Kvk = "1324 1345", Naam = "Caespi BV.", Plaats = "Utrecht" });

            //Assert

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
        [ExpectedException(typeof(FaultException<FunctionalErrorDetail[]>))]
        public void ThrowsFunctionalErrorDetailArrayException()
        {
            //Arrange            
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail();
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>())).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegOnderhoudsopdrachtToe(new Schema.Onderhoudsopdracht
            {
                Voertuig = new Schema.Voertuig()
            });

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsFunctionalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail();
            error.Message = "error gegooid";
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>())).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegOnderhoudsopdrachtToe(new Schema.Onderhoudsopdracht
                {
                    Voertuig = new Schema.Voertuig()
                });
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
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>())).Throws(
                new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegOnderhoudsopdrachtToe(new Schema.Onderhoudsopdracht
            {
                Voertuig = new Schema.Voertuig()
            });

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>())).Throws(
                 new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegOnderhoudsopdrachtToe(new Schema.Onderhoudsopdracht
                {
                    Voertuig = new Schema.Voertuig()
                });
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("error", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalErrorDetailArrayExceptionIfOnderhoudsopdrachtIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegOnderhoudsopdrachtToe(null);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrorsIfOnderhoudsopdrachtIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegOnderhoudsopdrachtToe(null);
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("Onderhoudsopdracht cannot be null", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalErrorDetailArrayExceptionIfVoertuigIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegOnderhoudsopdrachtToe(new Schema.Onderhoudsopdracht());

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrorsIfVoertuigIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegOnderhoudsopdrachtToe(It.IsAny<Schema.Onderhoudsopdracht>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegOnderhoudsopdrachtToe(new Schema.Onderhoudsopdracht());
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("Voertuig cannot be null", ex.Message);
            }
        }
    }
}
