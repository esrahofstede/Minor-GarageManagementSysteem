﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.FEGMS.Agent;
using Moq;
using Minor.Case2.FEGMS.Client.Controllers;
using System.Web.Mvc;
using Minor.Case2.FEGMS.Client.ViewModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.FEGMS.Client.Tests.Controllers
{
    [TestClass]
    public class MoneurControllerTest
    {
        [TestMethod]
        public void KlaarmeldenTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            MonteurController controller = new MonteurController(mock.Object);

            // Act
            ViewResult result = controller.Klaarmelden() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void KlaarmeldenPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>())).Returns(DummyData.GetVoertuigenCollection());
            mock.Setup(agent => agent.MeldVoertuigKlaar(It.IsAny<Voertuig>()));
            MonteurController controller = new MonteurController(mock.Object);
            KlaarmeldenVM klaarmelden = DummyData.GetKlaarmelden();

            // Act
            ViewResult result = controller.Klaarmelden(klaarmelden) as ViewResult;

            // Assert
            mock.Verify(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>()));
            mock.Verify(agent => agent.MeldVoertuigKlaar(It.IsAny<Voertuig>()));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(KlaarmeldenVM));

            var klaarmeldenVW = result.Model as KlaarmeldenVM;
            Assert.IsNotNull(klaarmelden.Voertuig);
            Assert.AreEqual("DS-344-S", klaarmelden.Voertuig.Kenteken);
        }
    }
}
