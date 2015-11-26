﻿using System;
using System.Diagnostics;
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
    public class GetAllLeaseMaatschappijenTest
    {
        [TestMethod]
        public void ReturnKlantenCollection()
        {
            //Arrange
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(leasemaatschappijen);

            //Act
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Assert
            Assert.AreEqual(typeof(Schema.KlantenCollection), target.GetAllLeasemaatschappijen().GetType());
        }

        [TestMethod]
        public void ReturnCorrectData()
        {
            //Arrange
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(leasemaatschappijen);

            //Act
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);
            var result = target.GetAllLeasemaatschappijen();

            //Assert
            Assert.AreEqual(3, target.GetAllLeasemaatschappijen().Count);
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
                new FunctionalException(new FunctionalErrorList(new []{error})));
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
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            var error = new FunctionalErrorDetail();
            error.Message = "error gegooid";
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Throws(
                new FunctionalException(new FunctionalErrorList(new []{error})));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                var result = target.GetAllLeasemaatschappijen();
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
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Throws(
                new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            var result = target.GetAllLeasemaatschappijen();

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
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Throws(
                 new TechnicalException("error"));
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Act
            try
            {
                var result = target.GetAllLeasemaatschappijen();
            }
            catch (FaultException ex)
            {
                //Assert   
                Assert.AreEqual("error", ex.Message);
            }
        }
    }
}
