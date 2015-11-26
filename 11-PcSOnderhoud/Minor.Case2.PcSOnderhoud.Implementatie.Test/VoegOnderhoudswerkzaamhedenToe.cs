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

    }
}
