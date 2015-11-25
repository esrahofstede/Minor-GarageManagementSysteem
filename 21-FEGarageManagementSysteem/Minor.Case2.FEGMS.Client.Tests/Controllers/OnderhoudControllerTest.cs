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
        public void InsertKlantgegevensPostWitLeaseTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(true);

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            // Act
            RedirectToRouteResult result = controller.InsertKlantgegevens(klantgegevens) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("Klantgegevens");

            var klantgegevensFromCookie = new JavaScriptSerializer().Deserialize<InsertKlantgegevensVM>(cookie.Value);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("InsertLeasemaatschappijGegevens", result.RouteValues.First().Value);
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
        public void InsertKlantgegevensPostWitoutLeaseTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(false);

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            // Act
            RedirectToRouteResult result = controller.InsertKlantgegevens(klantgegevens) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("Klantgegevens");

            var klantgegevensFromCookie = new JavaScriptSerializer().Deserialize<InsertKlantgegevensVM>(cookie.Value);

            // Assert
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
            //Set klantcookie
            var klantgegevensCookie = new HttpCookie("Klantgegevens", new JavaScriptSerializer().Serialize(DummyData.GetKlantGegevens(true)));
            controller.HttpContext.Request.Cookies.Add(klantgegevensCookie);

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

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

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
        public void InsertVoertuiggegevens()
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
            Assert.IsNull(result.Model);
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

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);
           
            //Set klantcookie
            var klantgegevensCookie = new HttpCookie("Klantgegevens", serializer.Serialize(DummyData.GetKlantGegevens(true)));
            controller.HttpContext.Request.Cookies.Add(klantgegevensCookie);
            //Set leasemaatschappijcookie
            var leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializer.Serialize(DummyData.GetLeasemaatschappijGegevens(false)));
            controller.HttpContext.Request.Cookies.Add(leasemaatschappijCookie);

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
        public void InsertVoertuiggegevensWithoutLeasePostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.VoegVoertuigMetKlantToe(It.IsAny<Voertuig>()));
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            //Set klantcookie
            var klantgegevensCookie = new HttpCookie("Klantgegevens", serializer.Serialize(DummyData.GetKlantGegevens(false)));
            controller.HttpContext.Request.Cookies.Add(klantgegevensCookie);

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

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            //Set klantcookie
            var klantgegevensCookie = new HttpCookie("Klantgegevens", serializer.Serialize(DummyData.GetKlantGegevens(false)));
            controller.HttpContext.Request.Cookies.Add(klantgegevensCookie);
            //Set leasemaatschappijcookie
            var leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializer.Serialize(DummyData.GetLeasemaatschappijGegevens(false)));
            controller.HttpContext.Request.Cookies.Add(leasemaatschappijCookie);
            //Set voertuigcookie
            var voertuigCookie = new HttpCookie("Voertuiggegevens", serializer.Serialize(DummyData.GetVoertuiggegevens()));
            controller.HttpContext.Request.Cookies.Add(voertuigCookie);

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
            Onderhoudsopdracht onderhoudsopdracht = DummyData.GetDummyOnderhoudsopdracht();

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            //Set Onderhoudsopdracht
            var onderhoudsopdrachtCookie = new HttpCookie("Onderhoudsopdracht", serializer.Serialize(onderhoudsopdracht));
            controller.HttpContext.Request.Cookies.Add(onderhoudsopdrachtCookie);     

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

            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            // Act
            RedirectToRouteResult result = controller.InsertedOnderhoudsopdracht() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues.First().Value);
        }

    }
}
