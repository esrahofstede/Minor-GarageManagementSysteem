using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace Minor.Case2.FEGMS.Client.Tests.Controllers
{
    public static class Helper
    {
        /// <summary>
        /// Helper method to create a controllercontext
        /// </summary>
        /// <param name="controller">The controller self</param>
        /// <returns>ControllerContext</returns>
        public static ControllerContext CreateContext(Controller controller)
        {
            var request = new HttpRequest("", "http://example.com/", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            return new ControllerContext(httpContext, new RouteData(), controller);
        }

        /// <summary>
        /// Helper method to set a new cookie
        /// </summary>
        /// <param name="name">Name of the cookie</param>
        /// <param name="controller">Controller to set the cookie</param>
        public static void SetCookie(string name, Controller controller)
        {
            var serializer = new JavaScriptSerializer();

            switch (name)
            {
                case "Onderhoudsopdracht":
                    var onderhoudsopdrachtCookie = new HttpCookie("Onderhoudsopdracht", serializer.Serialize(DummyData.GetDummyOnderhoudsopdracht()));
                    controller.HttpContext.Request.Cookies.Add(onderhoudsopdrachtCookie);
                    break;
                case "Klantgegevens":
                    var klantgegevensCookie = new HttpCookie("Klantgegevens", serializer.Serialize(DummyData.GetKlantGegevens(false)));
                    controller.HttpContext.Request.Cookies.Add(klantgegevensCookie);
                    break;
                case "LeasemaatschappijGegevens":
                    var leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializer.Serialize(DummyData.GetLeasemaatschappijGegevens(false)));
                    controller.HttpContext.Request.Cookies.Add(leasemaatschappijCookie);
                    break;
                case "Voertuiggegevens":
                    var voertuigCookie = new HttpCookie("Voertuiggegevens", serializer.Serialize(DummyData.GetVoertuiggegevens()));
                    controller.HttpContext.Request.Cookies.Add(voertuigCookie);
                    break;
                case "Onderhoudswerkzaamheden":
                    var onderhoudswerkzaamhedenCookie = new HttpCookie("Onderhoudswerkzaamheden", serializer.Serialize(DummyData.GetOnderhoudswerkzaamheden()));
                    controller.HttpContext.Request.Cookies.Add(onderhoudswerkzaamhedenCookie);
                    break;
                case "APK":
                    var steekproefCookie = new HttpCookie("APK", "steekproef");
                    controller.HttpContext.Request.Cookies.Add(steekproefCookie);
                    break;
                case "!APK":
                    var notSteekproefCookie = new HttpCookie("APK", "!steekproef");
                    controller.HttpContext.Request.Cookies.Add(notSteekproefCookie);
                    break;
                case "NoAPK":
                    var NoAPKCookie = new HttpCookie("APK", "geen");
                    controller.HttpContext.Request.Cookies.Add(NoAPKCookie);
                    break;
                case "VoertuiggegevensExisting":
                    var existingVoertuigenCookie = new HttpCookie("VoertuiggegevensExisting", serializer.Serialize(DummyData.GetVoertuigenCollection()));
                    controller.HttpContext.Request.Cookies.Add(existingVoertuigenCookie);
                    break;
            }
        }
    }
}
