using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.FEGMS.Agent;
using Moq;
using Minor.Case2.FEGMS.Client.Controllers;
using System.Web.Mvc;
using Minor.Case2.FEGMS.Client.ViewModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.Web.Script.Serialization;
using System.Linq;

namespace Minor.Case2.FEGMS.Client.Tests.Controllers
{
    [TestClass]
    public class MoneurControllerTest
    {
        [TestMethod]
        public void SearchAutoForWerkzaamhedenTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            MonteurController controller = new MonteurController(mock.Object);

            // Act
            ViewResult result = controller.SearchAutoForWerkzaamheden() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void SearchAutoForWerkzaamhedenPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>())).Returns(DummyData.GetDummyOnderhoudsopdracht());
            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            SearchVM search = new SearchVM { Kenteken = "00-00-00" };

            // Act
            RedirectToRouteResult result = controller.SearchAutoForWerkzaamheden(search) as RedirectToRouteResult;

            // Assert
            var onderhoudswerkzaamhedenCookie = controller.HttpContext.Response.Cookies.Get("Onderhoudswerkzaamheden");
            var onderhoudswerkzaamheden = new JavaScriptSerializer().Deserialize<OnderhoudswerkzaamhedenVM>(onderhoudswerkzaamhedenCookie.Value);

            mock.Verify(agent => agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>()));

            Assert.IsNotNull(result);
            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.AreEqual(12000, onderhoudswerkzaamheden.Kilometerstand);
            Assert.AreEqual("APK Keuren", onderhoudswerkzaamheden.Onderhoudsomschrijving);
            Assert.AreEqual(1, onderhoudswerkzaamheden.OnderhoudsopdrachtID);
        }


        [TestMethod]
        public void SearchAutoForWerkzaamhedenWithoutOnderhoudsopdrachtPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>())).Returns(default(Onderhoudsopdracht));
            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            SearchVM search = new SearchVM { Kenteken = "00-00-00" };

            // Act
            ViewResult result = controller.SearchAutoForWerkzaamheden(search) as ViewResult;

            // Assert
            var onderhoudswerkzaamhedenCookie = controller.HttpContext.Response.Cookies.Get("Onderhoudswerkzaamheden");

            mock.Verify(agent => agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>()));

            Assert.IsNull(onderhoudswerkzaamhedenCookie.Value);
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual("Er is geen onderhoudsopdracht gevonden voor de auto met het kenteken 00-00-00", controller.ModelState["Kenteken"].Errors.First().ErrorMessage);
            Assert.IsInstanceOfType(result.Model, typeof(SearchVM));
            var searchModel = result.Model as SearchVM;
            Assert.AreEqual("00-00-00", searchModel.Kenteken);
        }

        [TestMethod]
        public void OnderhoudswerkzaamhedenInvoerenTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("Onderhoudswerkzaamheden", controller);

            // Act
            ViewResult result = controller.OnderhoudswerkzaamhedenInvoeren() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(OnderhoudswerkzaamhedenVM));
            var onderhoudswerkzaamheden = result.Model as OnderhoudswerkzaamhedenVM;
            Assert.AreEqual(123456, onderhoudswerkzaamheden.Kilometerstand);
            Assert.AreEqual("Uitlaat vervangen", onderhoudswerkzaamheden.Onderhoudsomschrijving);
            Assert.AreEqual(1, onderhoudswerkzaamheden.OnderhoudsopdrachtID);

            var onderhoudswerkzaamhedenCookie = controller.HttpContext.Response.Cookies.Get("Onderhoudswerkzaamheden");
            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString(), onderhoudswerkzaamhedenCookie.Expires.ToString());
        }

        [TestMethod]
        public void OnderhoudswerkzaamhedenInvoerenWithoutCookieTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);

            // Act
            RedirectToRouteResult result = controller.OnderhoudswerkzaamhedenInvoeren() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("SearchAutoForWerkzaamheden", result.RouteValues.First().Value);
        }

        [TestMethod]
        public void OnderhoudswerkzaamhedenInvoerenWithoutSteekproefPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>())).Returns(false);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            OnderhoudswerkzaamhedenVM onderhoudswerkzaamheden = DummyData.GetOnderhoudswerkzaamheden();

            // Act
            RedirectToRouteResult result = controller.OnderhoudswerkzaamhedenInvoeren(onderhoudswerkzaamheden) as RedirectToRouteResult;

            // Assert
            var cookie = controller.HttpContext.Response.Cookies.Get("APK");

            mock.Verify(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>()));
            Assert.IsNotNull(result);
            Assert.AreEqual("Status", result.RouteValues.First().Value);
            Assert.AreEqual("!steekproef", cookie.Value);
        }

        [TestMethod]
        public void OnderhoudswerkzaamhedenInvoerenWithouAPKPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            bool? noAPK = null;
            mock.Setup(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>())).Returns(noAPK);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            OnderhoudswerkzaamhedenVM onderhoudswerkzaamheden = DummyData.GetOnderhoudswerkzaamheden();

            // Act
            RedirectToRouteResult result = controller.OnderhoudswerkzaamhedenInvoeren(onderhoudswerkzaamheden) as RedirectToRouteResult;

            // Assert
            var cookie = controller.HttpContext.Response.Cookies.Get("APK");

            mock.Verify(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>()));
            Assert.IsNotNull(result);
            Assert.AreEqual("Status", result.RouteValues.First().Value);
            Assert.AreEqual("geen", cookie.Value);
        }

        [TestMethod]
        public void OnderhoudswerkzaamhedenInvoerenWithSteekproefPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>())).Returns(true);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            OnderhoudswerkzaamhedenVM onderhoudswerkzaamheden = DummyData.GetOnderhoudswerkzaamheden();

            // Act
            RedirectToRouteResult result = controller.OnderhoudswerkzaamhedenInvoeren(onderhoudswerkzaamheden) as RedirectToRouteResult;

            // Assert
            var cookie = controller.HttpContext.Response.Cookies.Get("APK");
            mock.Verify(agent => agent.VoegOnderhoudswerkzaamhedenToe(It.IsAny<Onderhoudswerkzaamheden>()));

            Assert.IsNotNull(result);
            Assert.AreEqual("Status", result.RouteValues.First().Value);
            Assert.AreEqual("steekproef", cookie.Value);
        }

        [TestMethod]
        public void StatusWithSteekproefTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("APK", controller);

            // Act
            ViewResult result = controller.Status() as ViewResult;

            // Assert
            var steekproefCookie = controller.HttpContext.Response.Cookies.Get("APK");
            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString(), steekproefCookie.Expires.ToString());

            Assert.AreEqual(true, result.Model);
            Assert.AreEqual("steekproef", steekproefCookie.Value);
        }

        [TestMethod]
        public void StatusWithoutSteekproefTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("!APK", controller);

            // Act
            ViewResult result = controller.Status() as ViewResult;

            // Assert
            var steekproefCookie = controller.HttpContext.Response.Cookies.Get("APK");
            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString(), steekproefCookie.Expires.ToString());

            Assert.AreEqual(false, result.Model);
            Assert.AreEqual("!steekproef", steekproefCookie.Value);
        }

        [TestMethod]
        public void StatusWithoutAPKTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            Helper.SetCookie("NoAPK", controller);

            // Act
            ViewResult result = controller.Status() as ViewResult;

            // Assert
            var steekproefCookie = controller.HttpContext.Response.Cookies.Get("APK");
            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString(), steekproefCookie.Expires.ToString());

            Assert.IsNull(result.Model);
            Assert.AreEqual("geen", steekproefCookie.Value);
        }

        [TestMethod]
        public void StatusWithoutCookieTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);

            MonteurController controller = new MonteurController(mock.Object);
            controller.ControllerContext = Helper.CreateContext(controller);
            
            // Act
            RedirectToRouteResult result = controller.Status() as RedirectToRouteResult;

            // Assert
            var steekproefCookie = controller.HttpContext.Response.Cookies.Get("APK");
            Assert.IsNull(steekproefCookie.Value);

            Assert.AreEqual("Index", result.RouteValues.First().Value);
            Assert.AreEqual("Onderhoud", result.RouteValues.Last().Value);
        }

        [TestMethod]
        public void OnderhoudsopdrachtTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            MonteurController controller = new MonteurController(mock.Object);

            // Act
            ViewResult result = controller.Onderhoudsopdracht() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void OnderhoudsopdrachtPostTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>())).Returns(DummyData.GetDummyOnderhoudsopdracht());
            MonteurController controller = new MonteurController(mock.Object);

            OnderhoudsopdrachtVM model = new OnderhoudsopdrachtVM
            {
                Kenteken = "00-00-00",
            };

            // Act
            ViewResult result = controller.Onderhoudsopdracht(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(OnderhoudsopdrachtVM));
            var onderhoudsopdrachtVM = result.Model as OnderhoudsopdrachtVM;
            Assert.AreEqual(1, onderhoudsopdrachtVM.Onderhoudsopdracht.ID);
            Assert.IsTrue(onderhoudsopdrachtVM.Onderhoudsopdracht.APK);
            Assert.AreEqual(12000, onderhoudsopdrachtVM.Onderhoudsopdracht.Kilometerstand);
            Assert.AreEqual(new DateTime(2015, 11, 11), onderhoudsopdrachtVM.Onderhoudsopdracht.Aanmeldingsdatum);
            Assert.AreEqual("APK Keuren", onderhoudsopdrachtVM.Onderhoudsopdracht.Onderhoudsomschrijving);
        }


        [TestMethod]
        public void OnderhoudsopdrachtPostWithoutOnderhoudsopdrachtTest()
        {
            // Arrange
            var mock = new Mock<IAgentPcSOnderhoud>(MockBehavior.Strict);
            mock.Setup(agent => agent.GetOnderhoudsopdrachtBy(It.IsAny<OnderhoudsopdrachtZoekCriteria>())).Returns(default(Onderhoudsopdracht));
            MonteurController controller = new MonteurController(mock.Object);

            OnderhoudsopdrachtVM model = new OnderhoudsopdrachtVM
            {
                Kenteken = "00-00-00",
            };

            // Act
            ViewResult result = controller.Onderhoudsopdracht(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(OnderhoudsopdrachtVM));
            var onderhoudsopdrachtVM = result.Model as OnderhoudsopdrachtVM;
            Assert.IsNull(onderhoudsopdrachtVM.Onderhoudsopdracht);
            Assert.AreEqual("Voor de auto met het kenteken 00-00-00 kon geen onderhoudsopdracht gevonden worden", onderhoudsopdrachtVM.Message);

        }
    }
}
