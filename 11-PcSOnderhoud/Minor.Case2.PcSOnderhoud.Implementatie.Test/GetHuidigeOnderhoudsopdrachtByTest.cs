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
    public class GetHuidigeOnderhoudsopdrachtByTest
    {
        [TestMethod]
        [ExpectedException(typeof(FaultException<FunctionalErrorDetail[]>))]
        public void ThrowsFunctionalErrorDetailArrayException()
        {
            //Arrange            
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail();
            agentMock.Setup(agent => agent.GetOnderhoudsopdrachtenBy(It.IsAny<Schema.OnderhoudsopdrachtZoekCriteria>())).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.GetHuidigeOnderhoudsopdrachtBy(new Schema.OnderhoudsopdrachtZoekCriteria());

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
            agentMock.Setup(agent => agent.GetOnderhoudsopdrachtenBy(It.IsAny<Schema.OnderhoudsopdrachtZoekCriteria>())).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.GetHuidigeOnderhoudsopdrachtBy(new Schema.OnderhoudsopdrachtZoekCriteria());
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
            agentMock.Setup(agent => agent.GetOnderhoudsopdrachtenBy(It.IsAny<Schema.OnderhoudsopdrachtZoekCriteria>())).Throws(
                new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            target.GetHuidigeOnderhoudsopdrachtBy(new Schema.OnderhoudsopdrachtZoekCriteria());

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetOnderhoudsopdrachtenBy(It.IsAny<Schema.OnderhoudsopdrachtZoekCriteria>())).Throws(
                 new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.GetHuidigeOnderhoudsopdrachtBy(new Schema.OnderhoudsopdrachtZoekCriteria());
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("error", ex.Message);
            }
        }

    }
}
