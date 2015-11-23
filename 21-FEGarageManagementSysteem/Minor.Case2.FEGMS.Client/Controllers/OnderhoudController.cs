using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Client.Helper;
using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Minor.Case2.FEGMS.Client.Controllers
{
    public class OnderhoudController : Controller
    {
        // GET: Onderhoud
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertKlantgegevens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertKlantgegevens(InsertKlantgegevensVM model)
        {
            if(ModelState.IsValid)
            {

                var serializedKlantgegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie KlantgegevensCookie = new HttpCookie("Klantgegevens", serializedKlantgegevens);
                Response.SetCookie(KlantgegevensCookie);

                if (model.Lease)
                {
                    return RedirectToAction("InsertLeasemaatschappijGegevens");
                }
                else
                {
                    return RedirectToAction("InsertVoertuiggegevens");
                }
            }
            return View(model);
        }

        public ActionResult InsertLeasemaatschappijGegevens()
        {
            HttpCookie KlantgegevensCookie = Request.Cookies.Get("Klantgegevens");
            var klantgegevens = new JavaScriptSerializer().Deserialize<InsertKlantgegevensVM>(KlantgegevensCookie.Value);
            
            return View();
        }

        [HttpPost]
        public ActionResult InsertLeasemaatschappijGegevens(InsertLeasemaatschappijGegevensVM model)
        {
            if(ModelState.IsValid)
            {
                var serializedLeasmaatschappijGegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializedLeasmaatschappijGegevens);
                Response.SetCookie(leasemaatschappijCookie);

                return RedirectToAction("InsertVoertuiggegevens");
            }
           
            return View(model);
        }

        public ActionResult InsertVoertuiggegevens()
        {
            HttpCookie leasemaatschappijCookie = Request.Cookies.Get("LeasemaatschappijGegevens");
            var leasemaatschappijgegevens = new JavaScriptSerializer().Deserialize<InsertLeasemaatschappijGegevensVM>(leasemaatschappijCookie.Value);

            return View();
        }

        [HttpPost]
        public ActionResult InsertVoertuiggegevens(InsertVoertuiggegevensVM model)
        {
            if (ModelState.IsValid)
            {
                var serializedVoertuiggegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie voertuiggegevensCookie = new HttpCookie("Voertuiggegevens", serializedVoertuiggegevens);
                Response.SetCookie(voertuiggegevensCookie);

                return RedirectToAction("InsertOnderhoudsopdracht");
            }

            return View(model);
        }

        public ActionResult InsertOnderhoudsopdracht()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertOnderhoudsopdracht(InsertOnderhoudsopdrachtVM model)
        {
            if (ModelState.IsValid)
            {
                HttpCookie leasemaatschappijCookie = Request.Cookies.Get("LeasemaatschappijGegevens");
                var leasemaatschappijgegevens = new JavaScriptSerializer().Deserialize<InsertLeasemaatschappijGegevensVM>(leasemaatschappijCookie.Value);

                HttpCookie klantgegevensCookie = Request.Cookies.Get("Klantgegevens");
                var klantgegevens = new JavaScriptSerializer().Deserialize<InsertKlantgegevensVM>(klantgegevensCookie.Value);

                HttpCookie voertuiggegevensCookie = Request.Cookies.Get("Voertuiggegevens");
                var voertuiggegevens = new JavaScriptSerializer().Deserialize<InsertVoertuiggegevensVM>(voertuiggegevensCookie.Value);
                var onderhoudsopdracht = Mapper.MapToOnderhoudsopdracht(model, leasemaatschappijgegevens, klantgegevens, voertuiggegevens);
            }

            return View(model);
        }

        
    }
}
