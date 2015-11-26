using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.Exceptions.V1.Schema;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.PcSOnderhoud.Agent;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using Moq;

namespace Minor.Case2.PcSOnderhoud.Implementation.Tests
{
    [TestClass]
    public class VoegVoertuigMetKlantToeTest
    {
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsFunctionalErrorDetailArrayException()
        {
            //Arrange            
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail();
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>())).Throws(new TechnicalException(""));
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(new FaultException(""));

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegVoertuigMetKlantToe(new Schema.Voertuig
            {
                Bestuurder = new Schema.Persoon(),
                Eigenaar = new Schema.Persoon()
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
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>())).Throws(new TechnicalException("error gegooid"));
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(new FaultException("error gegooid"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegVoertuigMetKlantToe(new Schema.Voertuig
                {
                    Bestuurder = new Schema.Persoon(),
                    Eigenaar = new Schema.Persoon()
                });
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual(error.Message, ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalErrorDetailArrayException()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>())).Throws(
                new TechnicalException("error"));
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(new FaultException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegVoertuigMetKlantToe(new Schema.Voertuig
            {
                Bestuurder = new Schema.Persoon(),
                Eigenaar = new Schema.Persoon()
            });

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>())).Throws(
                 new TechnicalException("error"));
            agentMock.Setup(agent => agent.GetAllPersonen()).Throws(new FaultException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegVoertuigMetKlantToe(new Schema.Voertuig
                {
                    Bestuurder = new Schema.Persoon(),
                    Eigenaar = new Schema.Persoon()
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
        public void ThrowsTechnicalErrorDetailArrayExceptionIfVoertuigIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegVoertuigMetKlantToe(null);

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrorsIfVoertuigIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegVoertuigMetKlantToe(null);
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("Voertuig cannot be null", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalErrorDetailArrayExceptionIfBestuurderIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegVoertuigMetKlantToe(new Schema.Voertuig
            {
                Eigenaar = new Schema.Persoon()
            });

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrorsIfBestuurderIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegVoertuigMetKlantToe(new Schema.Voertuig
                {
                    Eigenaar = new Schema.Persoon()
                });

            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("Bestuurder cannot be null", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ThrowsTechnicalErrorDetailArrayExceptionIfEigenaarIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.VoegVoertuigMetKlantToe(new Schema.Voertuig
            {
                Bestuurder = new Schema.Persoon()
            });

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrorsIfEigenaarIsNull()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Schema.Voertuig>()));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.VoegVoertuigMetKlantToe(new Schema.Voertuig
                {
                    Bestuurder = new Schema.Persoon()
                });

            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("Eigenaar cannot be null", ex.Message);
            }
        }

    }
}
