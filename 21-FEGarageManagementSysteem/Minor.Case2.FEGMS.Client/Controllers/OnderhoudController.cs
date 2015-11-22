using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        public ActionResult InsertLeasemaatschappijGegevens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertLeasemaatschappijGegevens(InsertLeasemaatschappijGegevensVM model)
        {
            return View();
        }

        public ActionResult InsertVoertuiggegevens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertVoertuiggegevens(InsertVoertuiggegevensVM model)
        {
            return View();
        }


    }
}
