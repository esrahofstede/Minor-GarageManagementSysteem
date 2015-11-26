using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.FEGMS.Client;
using Minor.Case2.FEGMS.Client.Controllers;
using Minor.Case2.FEGMS.Client.ViewModel;
using Moq;
using Minor.Case2.FEGMS.Agent;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Web.Routing;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.FEGMS.Client.Tests.Controllers
{
    [TestClass]
    public class OnderhoudControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void InsertKlantgegevens()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);

            // Act
            ViewResult result = controller.InsertKlantgegevens() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void InsertKlantgegevensPostExistingVoertuigTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.HaalVoertuigenOpVoor(It.IsAny<Persoon>())).Returns(DummyData.GetVoertuigenCollection());
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(true);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertKlantgegevens(klantgegevens) as RedirectToRouteResult;

            // Assert
            var serializer = new JavaScriptSerializer();

            var cookie = controller.HttpContext.Response.Cookies.Get("Klantgegevens");
            var klantgegevensFromCookie = serializer.Deserialize<InsertKlantgegevensVM>(cookie.Value);

            var voertuigenExistCookie = controller.HttpContext.Response.Cookies.Get("VoertuiggegevensExisting");
            var voertuigencollection = serializer.Deserialize<Voertuig[]>(voertuigenExistCookie.Value);

            Assert.AreEqual(1, voertuigencollection.Length);
            Assert.AreEqual("DS-344-S", voertuigencollection.First().Kenteken);

            mock.Verify(agent => agent.HaalVoertuigenOpVoor(It.IsAny<Persoon>()));

            Assert.IsNotNull(result);
            Assert.AreEqual("InsertVoertuiggegevens", result.RouteValues.First().Value);
            Assert.AreEqual(klantgegevens.Voornaam, klantgegevensFromCookie.Voornaam);
            Assert.AreEqual(klantgegevens.Tussenvoegsel, klantgegevensFromCookie.Tussenvoegsel);
            Assert.AreEqual(klantgegevens.Achternaam, klantgegevensFromCookie.Achternaam);
            Assert.AreEqual(klantgegevens.Adres, klantgegevensFromCookie.Adres);
            Assert.AreEqual(klantgegevens.Postcode, klantgegevensFromCookie.Postcode);
            Assert.AreEqual(klantgegevens.Woonplaats, klantgegevensFromCookie.Woonplaats);
            Assert.AreEqual(klantgegevens.Emailadres, klantgegevensFromCookie.Emailadres);
            Assert.AreEqual(klantgegevens.Telefoonnummer, klantgegevensFromCookie.Telefoonnummer);
            Assert.AreEqual(klantgegevens.Lease, klantgegevensFromCookie.Lease);
        }

        [TestMethod]
        public void InsertKlantgegevensPostWitoutExistingVoertuigTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.HaalVoertuigenOpVoor(It.IsAny<Persoon>())).Returns(new VoertuigenCollection());
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(false);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertKlantgegevens(klantgegevens) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("Klantgegevens");

            var klantgegevensFromCookie = new JavaScriptSerializer().Deserialize<InsertKlantgegevensVM>(cookie.Value);

            // Assert
            mock.Verify(agent => agent.HaalVoertuigenOpVoor(It.IsAny<Persoon>()));

            Assert.IsNotNull(result);
            Assert.AreEqual("InsertVoertuiggegevens", result.RouteValues.First().Value);
            Assert.AreEqual(klantgegevens.Voornaam, klantgegevensFromCookie.Voornaam);
            Assert.AreEqual(klantgegevens.Tussenvoegsel, klantgegevensFromCookie.Tussenvoegsel);
            Assert.AreEqual(klantgegevens.Achternaam, klantgegevensFromCookie.Achternaam);
            Assert.AreEqual(klantgegevens.Adres, klantgegevensFromCookie.Adres);
            Assert.AreEqual(klantgegevens.Postcode, klantgegevensFromCookie.Postcode);
            Assert.AreEqual(klantgegevens.Woonplaats, klantgegevensFromCookie.Woonplaats);
            Assert.AreEqual(klantgegevens.Emailadres, klantgegevensFromCookie.Emailadres);
            Assert.AreEqual(klantgegevens.Telefoonnummer, klantgegevensFromCookie.Telefoonnummer);
            Assert.AreEqual(klantgegevens.Lease, klantgegevensFromCookie.Lease);
        }

        [TestMethod]
        public void InsertLeasemaatschappijGegevens()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(DummyData.GetAllLeasemaatschappijen());

            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("Klantgegevens", controller);


            // Act
            ViewResult result = controller.InsertLeasemaatschappijGegevens() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(InsertLeasemaatschappijGegevensVM));
            var lease = result.Model as InsertLeasemaatschappijGegevensVM;
            Assert.IsTrue(lease.Exist);
            Assert.AreEqual(lease.Leasemaatschappijen.Count(), DummyData.GetAllLeasemaatschappijen().Count());

            mock.Verify(agent => agent.GetAllLeasemaatschappijen());
        }

        [TestMethod]
        public void InsertLeasemaatschappijGegevensWithoutKlantgegevensTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);

            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertLeasemaatschappijGegevens() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertKlantgegevens", result.RouteValues.First().Value);
        }


        [TestMethod]
        public void InsertLeasemaatschappijGegevensNotExistingWithoutNaamAndTelefoonnummerTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(DummyData.GetAllLeasemaatschappijen());
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
            leasemaatschappij.Naam = null;
            leasemaatschappij.Telefoonnummer = null;

            // Act
            ViewResult result = controller.InsertLeasemaatschappijGegevens(leasemaatschappij) as ViewResult;

            // Assert
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState["Naam"].Errors.Count);
            Assert.AreEqual("Naam is een verplicht veld", controller.ModelState["Naam"].Errors.First().ErrorMessage);
            Assert.AreEqual(1, controller.ModelState["Telefoonnummer"].Errors.Count);
            Assert.AreEqual("Telefoonnummer is een verplicht veld", controller.ModelState["Telefoonnummer"].Errors.First().ErrorMessage);
            mock.Verify(agent => agent.GetAllLeasemaatschappijen());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(InsertLeasemaatschappijGegevensVM));
            var lease = result.Model as InsertLeasemaatschappijGegevensVM;
            Assert.AreEqual(lease.Leasemaatschappijen.Count(), DummyData.GetAllLeasemaatschappijen().Count());
        }


        [TestMethod]
        public void InsertLeasemaatschappijGegevensExistingSelectedLeasemaatschappijIDTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(DummyData.GetAllLeasemaatschappijen());
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(true);
            leasemaatschappij.SelectedLeasemaatschappijID = 0;

            // Act
            ViewResult result = controller.InsertLeasemaatschappijGegevens(leasemaatschappij) as ViewResult;

            // Assert
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual(1, controller.ModelState["Leasemaatschappijen"].Errors.Count);
            Assert.AreEqual("Indien het een bestaande leasemaatschappij is, moet er één geselecteerd zijn.", controller.ModelState["Leasemaatschappijen"].Errors.First().ErrorMessage);
            mock.Verify(agent => agent.GetAllLeasemaatschappijen());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(InsertLeasemaatschappijGegevensVM));
            var lease = result.Model as InsertLeasemaatschappijGegevensVM;
            Assert.AreEqual(lease.Leasemaatschappijen.Count(), DummyData.GetAllLeasemaatschappijen().Count());
        }


        [TestMethod]
        public void InsertNewLeasemaatschappijGegevensPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertLeasemaatschappijGegevens(leasemaatschappij) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("LeasemaatschappijGegevens");

            var leasemaatschappijFromCookie = new JavaScriptSerializer().Deserialize<InsertLeasemaatschappijGegevensVM>(cookie.Value);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertVoertuiggegevens", result.RouteValues.First().Value);
            Assert.AreEqual(leasemaatschappij.Naam, leasemaatschappijFromCookie.Naam);
            Assert.AreEqual(leasemaatschappij.Telefoonnummer, leasemaatschappijFromCookie.Telefoonnummer);
            Assert.AreEqual(leasemaatschappij.Exist, leasemaatschappijFromCookie.Exist);
            Assert.AreEqual(leasemaatschappij.SelectedLeasemaatschappijID, leasemaatschappijFromCookie.SelectedLeasemaatschappijID);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithoutExistingVoertuigenTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("Klantgegevens", controller);

            // Act
            ViewResult result = controller.InsertVoertuiggegevens() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(InsertVoertuiggegevensVM));
            var voertuigen = result.Model as InsertVoertuiggegevensVM;
            Assert.IsNull(voertuigen.Voertuigen);
            Assert.IsFalse(voertuigen.Exist);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithExistingVoertuigenTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("Klantgegevens", controller);
            Helper.SetCookie("VoertuiggegevensExisting", controller);

            // Act
            ViewResult result = controller.InsertVoertuiggegevens() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(InsertVoertuiggegevensVM));

            var voertuigen = result.Model as InsertVoertuiggegevensVM;
            Assert.AreEqual(1, voertuigen.Voertuigen.Count());
            Assert.IsTrue(voertuigen.Exist);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithoutKlantgegevensTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertVoertuiggegevens() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertKlantgegevens", result.RouteValues.First().Value);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithLeasePostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            controller.ControllerContext = Helper.CreateContext(controller);

            Helper.SetCookie("Klantgegevens", controller);
            Helper.SetCookie("LeasemaatschappijGegevens", controller);

            // Act
            RedirectToRouteResult result = controller.InsertVoertuiggegevens(voertuiggegevens) as RedirectToRouteResult;

            // Assert   
            var cookie = controller.HttpContext.Response.Cookies.Get("Voertuiggegevens");
            var voertuigFromCookie = serializer.Deserialize<InsertVoertuiggegevensVM>(cookie.Value);

            mock.Verify(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertOnderhoudsopdracht", result.RouteValues.First().Value);
            Assert.AreEqual(voertuiggegevens.Kenteken, voertuigFromCookie.Kenteken);
            Assert.AreEqual(voertuiggegevens.Merk, voertuigFromCookie.Merk);
            Assert.AreEqual(voertuiggegevens.Type, voertuigFromCookie.Type);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithoutLeasePostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("Klantgegevens", controller);

            // Act
            RedirectToRouteResult result = controller.InsertVoertuiggegevens(voertuiggegevens) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("Voertuiggegevens");

            var voertuigFromCookie = serializer.Deserialize<InsertVoertuiggegevensVM>(cookie.Value);

            // Assert
            mock.Verify(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertOnderhoudsopdracht", result.RouteValues.First().Value);
            Assert.AreEqual(voertuiggegevens.Kenteken, voertuigFromCookie.Kenteken);
            Assert.AreEqual(voertuiggegevens.Merk, voertuigFromCookie.Merk);
            Assert.AreEqual(voertuiggegevens.Type, voertuigFromCookie.Type);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithoutExistingVoertuigenWithoutKentekenMerkAndTypePostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            voertuiggegevens.Kenteken = "";
            voertuiggegevens.Merk = "";
            voertuiggegevens.Type = "";

            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("VoertuiggegevensExisting", controller);

            // Act
            ViewResult result = controller.InsertVoertuiggegevens(voertuiggegevens) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(InsertVoertuiggegevensVM));
            var insertVoertuigen = result.Model as InsertVoertuiggegevensVM;

            Assert.AreEqual(1, insertVoertuigen.Voertuigen.Count());
            Assert.AreEqual(1, controller.ModelState["Kenteken"].Errors.Count);
            Assert.AreEqual("Kenteken is een verplicht veld voor de voertuiggegevens", controller.ModelState["Kenteken"].Errors.First().ErrorMessage);
            Assert.AreEqual(1, controller.ModelState["Merk"].Errors.Count);
            Assert.AreEqual("Merk is een verplicht veld voor de voertuiggegevens", controller.ModelState["Merk"].Errors.First().ErrorMessage);
            Assert.AreEqual(1, controller.ModelState["Type"].Errors.Count);
            Assert.AreEqual("Type is een verplicht veld voor de voertuiggegevens", controller.ModelState["Type"].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithExistingVoertuigenWithoutSelectingPostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            voertuiggegevens.SelectedKenteken = "";
            voertuiggegevens.Exist = true;

            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("VoertuiggegevensExisting", controller);

            // Act
            ViewResult result = controller.InsertVoertuiggegevens(voertuiggegevens) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(InsertVoertuiggegevensVM));
            var insertVoertuigen = result.Model as InsertVoertuiggegevensVM;

            Assert.AreEqual(1, insertVoertuigen.Voertuigen.Count());
            Assert.AreEqual(1, controller.ModelState["Voertuigen"].Errors.Count);
            Assert.AreEqual("Indien het een bestaand voertuig is, moet er één geselecteerd zijn.", controller.ModelState["Voertuigen"].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void InsertVoertuiggegevensWithExistingVoertuigTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            voertuiggegevens.SelectedKenteken = "00-00-00";
            controller.ControllerContext = Helper.CreateContext(controller);

            Helper.SetCookie("Klantgegevens", controller);
            Helper.SetCookie("LeasemaatschappijGegevens", controller);

            // Act
            RedirectToRouteResult result = controller.InsertVoertuiggegevens(voertuiggegevens) as RedirectToRouteResult;

            // Assert   
            var cookie = controller.HttpContext.Response.Cookies.Get("Voertuiggegevens");
            var voertuigFromCookie = serializer.Deserialize<InsertVoertuiggegevensVM>(cookie.Value);

            Assert.IsNotNull(result);
            Assert.AreEqual("InsertOnderhoudsopdracht", result.RouteValues.First().Value);
            Assert.AreEqual("00-00-00", voertuigFromCookie.Kenteken);
        }

        [TestMethod]
        public void InsertOnderhoudsopdracht()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);

            Helper.SetCookie("Klantgegevens", controller);
            Helper.SetCookie("LeasemaatschappijGegevens", controller);
            Helper.SetCookie("Voertuiggegevens", controller);

            // Act
            ViewResult result = controller.InsertOnderhoudsopdracht() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void InsertOnderhoudsopdrachtWithoutKlantgegevensAndVoertuiggegevensTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertOnderhoudsopdracht() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertKlantgegevens", result.RouteValues.First().Value);
        }

        [TestMethod]
        public void InsertOnderhoudsopdrachtPostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.AddOnderhoudsopdrachtWithKlantAndVoertuig(It.IsAny<Onderhoudsopdracht>()));
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertOnderhoudsopdrachtVM onderhoudsopdracht = DummyData.GetOnderhoudsopdracht();
            controller.ControllerContext = Helper.CreateContext(controller);

            Helper.SetCookie("Klantgegevens", controller);
            Helper.SetCookie("LeasemaatschappijGegevens", controller);
            Helper.SetCookie("Voertuiggegevens", controller);

            // Act
            RedirectToRouteResult result = controller.InsertOnderhoudsopdracht(onderhoudsopdracht) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("Voertuiggegevens");

            // Assert
            mock.Verify(agent => agent.AddOnderhoudsopdrachtWithKlantAndVoertuig(It.IsAny<Onderhoudsopdracht>()));
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertedOnderhoudsopdracht", result.RouteValues.First().Value);
        }

        [TestMethod]
        public void InsertedOnderhoudsopdrachtTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("Onderhoudsopdracht", controller);

            // Act
            ViewResult result = controller.InsertedOnderhoudsopdracht() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(Onderhoudsopdracht));
            var opdracht = result.Model as Onderhoudsopdracht;
            Assert.AreEqual("NL-123-G", opdracht.Voertuig.Kenteken);
            Assert.AreEqual("0612345678", opdracht.Voertuig.Bestuurder.Telefoonnummer);
            Assert.AreEqual("0621448522", opdracht.Voertuig.Eigenaar.Telefoonnummer);
        }

        [TestMethod]
        public void InsertedOnderhoudsopdrachtNullCookieTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.InsertedOnderhoudsopdracht() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues.First().Value);
        }

    }
}
