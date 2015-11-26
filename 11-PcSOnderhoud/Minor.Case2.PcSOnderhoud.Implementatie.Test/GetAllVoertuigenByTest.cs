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
    public class GetAllVoertuigenByTest
    {
        [TestMethod]
        public void ReturnsVoertuigenCollection()
        {
            //Arrange
            var voertuigenCollections = new Schema.VoertuigenCollection
            {
                new Schema.Voertuig(),
                new Schema.Voertuig(),
                new Schema.Voertuig()
            };
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(voertuigenCollections);

            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.GetVoertuigBy(new Schema.VoertuigenSearchCriteria());

            //Assert
            Assert.AreEqual(typeof(Schema.VoertuigenCollection), result.GetType());
        }

        [TestMethod]
        public void ReturnCorrectData()
        {
            //Arrange
            var voertuigenCollections = new Schema.VoertuigenCollection();
            voertuigenCollections.Add(new Schema.Voertuig());
            voertuigenCollections.Add(new Schema.Voertuig());
            voertuigenCollections.Add(new Schema.Voertuig());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Returns(voertuigenCollections);

            //Act
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);
            var result = target.GetVoertuigBy(new Schema.VoertuigenSearchCriteria());

            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FunctionalErrorDetail[]>))]
        public void ThrowsFunctionalErrorDetailArrayException()
        {
            //Arrange
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail();
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.GetAllLeasemaatschappijen();

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
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Throws(
                new FunctionalException(new FunctionalErrorList(new[] { error })));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                target.GetVoertuigBy(new Schema.VoertuigenSearchCriteria());
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
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Throws(
                new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.GetVoertuigBy(new Schema.VoertuigenSearchCriteria());

            //Assert
            //Exception thrown
        }

        [TestMethod]
        public void ThrowsTechnicalErrorDetailArrayExceptionCorrectErrors()
        {
            //Arrange
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetVoertuigBy(It.IsAny<Schema.VoertuigenSearchCriteria>())).Throws(
                 new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                var result = target.GetVoertuigBy(new Schema.VoertuigenSearchCriteria());
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("error", ex.Message);
            }
        }
    }
}
