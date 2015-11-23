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
            OnderhoudController controller = new OnderhoudController(mock.Object);

            // Act
            ViewResult result = controller.InsertLeasemaatschappijGegevens() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void InsertLeasemaatschappijGegevensPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens();

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
        }

        [TestMethod]
        public void InsertVoertuiggegevens()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            OnderhoudController controller = new OnderhoudController(mock.Object);

            // Act
            ViewResult result = controller.InsertVoertuiggegevens() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void InsertVoertuiggegevensPostTest()
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

            // Act
            ViewResult result = controller.InsertOnderhoudsopdracht() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void InsertOnderhoudsopdrachtPostTest()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.AddOnderhoudsOpdrachtWithKlantAndVoertuig(It.IsAny<Onderhoudsopdracht>()));
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
            var leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializer.Serialize(DummyData.GetLeasemaatschappijGegevens()));
            controller.HttpContext.Request.Cookies.Add(leasemaatschappijCookie);
            //Set voertuigcookie
            var voertuigCookie = new HttpCookie("Voertuiggegevens", serializer.Serialize(DummyData.GetVoertuiggegevens()));
            controller.HttpContext.Request.Cookies.Add(voertuigCookie);

            // Act
            RedirectToRouteResult result = controller.InsertOnderhoudsopdracht(onderhoudsopdracht) as RedirectToRouteResult;
            var cookie = controller.HttpContext.Response.Cookies.Get("Voertuiggegevens");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues.First().Value);
        }
    }
}
