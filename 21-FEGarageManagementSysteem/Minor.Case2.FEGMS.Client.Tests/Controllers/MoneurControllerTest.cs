using System;
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
        //[TestMethod]
        //public void KlaarmeldenTest()
        //{
        //    // Arrange
        //    var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
        //    MonteurController controller = new MonteurController(mock.Object);

        //    // Act
        //    ViewResult result = controller.Klaarmelden() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsNull(result.Model);
        //}

        //[TestMethod]
        //public void KlaarmeldenPostWithoutSteekproefTest()
        //{
        //    // Arrange
        //    var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
        //    mock.Setup(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>())).Returns(DummyData.GetVoertuigenCollection());
        //    mock.Setup(agent => agent.MeldVoertuigKlaar(It.IsAny<Voertuig>())).Returns(false);
        //    MonteurController controller = new MonteurController(mock.Object);
        //    KlaarmeldenVM klaarmelden = DummyData.GetKlaarmelden();

        //    // Act
        //    ViewResult result = controller.Klaarmelden(klaarmelden) as ViewResult;

        //    // Assert
        //    mock.Verify(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>()));
        //    mock.Verify(agent => agent.MeldVoertuigKlaar(It.IsAny<Voertuig>()));

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result.Model, typeof(KlaarmeldenVM));

        //    var klaarmeldenVW = result.Model as KlaarmeldenVM;
        //    Assert.IsNotNull(klaarmelden.Voertuig);
        //    Assert.AreEqual("De auto met het kenteken DS-344-S is afgemeld.", klaarmelden.Message);
        //    Assert.AreEqual("DS-344-S", klaarmelden.Voertuig.Kenteken);
        //    Assert.IsFalse(klaarmelden.Steekproef);
        //}

        //[TestMethod]
        //public void KlaarmeldenPostWithSteekproefTest()
        //{
        //    // Arrange
        //    var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
        //    mock.Setup(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>())).Returns(DummyData.GetVoertuigenCollection());
        //    mock.Setup(agent => agent.MeldVoertuigKlaar(It.IsAny<Voertuig>())).Returns(true);
        //    MonteurController controller = new MonteurController(mock.Object);
        //    KlaarmeldenVM klaarmelden = DummyData.GetKlaarmelden();

        //    // Act
        //    ViewResult result = controller.Klaarmelden(klaarmelden) as ViewResult;

        //    // Assert
        //    mock.Verify(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>()));
        //    mock.Verify(agent => agent.MeldVoertuigKlaar(It.IsAny<Voertuig>()));

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result.Model, typeof(KlaarmeldenVM));

        //    var klaarmeldenVW = result.Model as KlaarmeldenVM;
        //    Assert.IsNotNull(klaarmelden.Voertuig);
        //    Assert.AreEqual("De auto met het kenteken DS-344-S is klaargemeld.", klaarmelden.Message);
        //    Assert.AreEqual("DS-344-S", klaarmelden.Voertuig.Kenteken);
        //    Assert.IsTrue(klaarmelden.Steekproef);
        //}

        //[TestMethod]
        //public void KlaarmeldenPostVoertuigNotFoundTest()
        //{
        //    // Arrange
        //    var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
        //    mock.Setup(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>())).Returns(new VoertuigenCollection());
        //    MonteurController controller = new MonteurController(mock.Object);
        //    KlaarmeldenVM klaarmelden = DummyData.GetKlaarmelden();

        //    // Act
        //    ViewResult result = controller.Klaarmelden(klaarmelden) as ViewResult;

        //    // Assert
        //    mock.Verify(agent => agent.GetVoertuigBy(It.IsAny<VoertuigenSearchCriteria>()));

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result.Model, typeof(KlaarmeldenVM));

        //    var klaarmeldenVW = result.Model as KlaarmeldenVM;
        //    Assert.IsNull(klaarmelden.Voertuig);
        //    Assert.AreEqual("De auto met het kenteken DS-344-S kon niet worden gevonden in het systeem.", klaarmelden.Message);
        //}
    }
}
