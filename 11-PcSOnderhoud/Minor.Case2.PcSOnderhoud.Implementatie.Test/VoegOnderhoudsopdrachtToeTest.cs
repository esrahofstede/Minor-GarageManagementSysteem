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
    public class VoegOnderhoudsopdrachtToeTest
    {
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
