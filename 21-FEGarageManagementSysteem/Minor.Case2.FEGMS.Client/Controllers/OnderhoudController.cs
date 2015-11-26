using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Agent;
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
        IAgentPcSOnderhoud _agent;

        /// <summary>
        /// Contstructor to instantiate the agent
        /// </summary>
        public OnderhoudController()
        {
            _agent = new AgentPcSOnderhoud();
        }

        /// <summary>
        /// Creates an instance of the OnderhoudController and can be injected with an IAgentPcSOnderhoud
        /// </summary>
        /// <param name="agent">Injectable IAgentPcSOnderhoud</param>
        public OnderhoudController(IAgentPcSOnderhoud agent)
        {
            _agent = agent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show the form to insert Klantgegevens
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertKlantgegevens()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a Klant and insert it in a cookie
        /// </summary>
        /// <param name="model">Inserted klantgegevens viewmodel</param>
        /// <returns>RedirectToAction depends on lease or not. If lease to InsertLeasemaatschappijGegevens else to InsertVoertuiggegevens</returns>
        [HttpPost]
        public ActionResult InsertKlantgegevens(InsertKlantgegevensVM model)
        {
            if(ModelState.IsValid)
            {

                var serializedKlantgegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie KlantgegevensCookie = new HttpCookie("Klantgegevens", serializedKlantgegevens);
                Response.Cookies.Add(KlantgegevensCookie);

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

        /// <summary>
        /// Show the form to insert Leasemaatschappijgegevens
        /// </summary>
        /// <returns>view</returns>
        public ActionResult InsertLeasemaatschappijGegevens()
        {
            if (!AreCookieSet("Klantgegevens"))
            {
                return RedirectToAction("InsertKlantgegevens");
            }
            InsertLeasemaatschappijGegevensVM model = new InsertLeasemaatschappijGegevensVM();

            var leasemaatschappijen = _agent.GetAllLeasemaatschappijen().Cast<Leasemaatschappij>();
            model.Leasemaatschappijen = leasemaatschappijen.Select((lease => new SelectListItem { Value = lease.ID.ToString(), Text = lease.Naam }));
            model.Exist = true;
            return View(model);
        }

        /// <summary>
        /// Post the inserted data from a leasemaatschappij and insert it in a cookie
        /// </summary>
        /// <param name="model">Inserted leasemaatschappij viewmodel</param>
        /// <returns>RedirectToAction to InsertVoertuiggegevens</returns>
        [HttpPost]
        public ActionResult InsertLeasemaatschappijGegevens(InsertLeasemaatschappijGegevensVM model)
        {
            if(!model.Exist)
            {
                if (string.IsNullOrWhiteSpace(model.Naam))
                {
                    ModelState.AddModelError("Naam", "Naam is een verplicht veld");
                }

                if (string.IsNullOrWhiteSpace(model.Telefoonnummer))
                {
                    ModelState.AddModelError("Telefoonnummer", "Telefoonnummer is een verplicht veld");
                }
            }
            else
            {
                if(model.SelectedLeasemaatschappijID == 0)
                {
                    ModelState.AddModelError("Leasemaatschappijen", "Indien het een bestaande leasemaatschappij is, moet er één geselecteerd zijn.");
                }
            }


            if(ModelState.IsValid)
            {
                var serializedLeasmaatschappijGegevens = new JavaScriptSerializer().Serialize(model);

                HttpCookie leasemaatschappijCookie = new HttpCookie("LeasemaatschappijGegevens", serializedLeasmaatschappijGegevens);
                Response.Cookies.Add(leasemaatschappijCookie);

                return RedirectToAction("InsertVoertuiggegevens");
            }

            var leasemaatschappijen = _agent.GetAllLeasemaatschappijen().Cast<Leasemaatschappij>();
            model.Leasemaatschappijen = leasemaatschappijen.Select((lease => new SelectListItem { Value = lease.ID.ToString(), Text = lease.Naam }));
            return View(model);
        }

        /// <summary>
        /// Show the form to insert voertuiggegevens
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertVoertuiggegevens()
        {
            if (!AreCookieSet("Klantgegevens"))
            {
                return RedirectToAction("InsertKlantgegevens");
            }
            return View();
        }

        /// <summary>
        /// Post the inserted data from a voertuig and send it to all of the data to the agent
        /// Deserialize the previous cookies
        /// </summary>
        /// <param name="model">Inserted voertuig viewmodel</param>
        /// <returns>RedirectToAction to InsertOnderhoudsopdracht</returns>
        [HttpPost]
        public ActionResult InsertVoertuiggegevens(InsertVoertuiggegevensVM model)
        {
            if (ModelState.IsValid)
            {
                var serializer = new JavaScriptSerializer();
                var serializedVoertuiggegevens = serializer.Serialize(model);
       
                HttpCookie leasemaatschappijCookie = Request.Cookies.Get("LeasemaatschappijGegevens");

                InsertLeasemaatschappijGegevensVM leasemaatschappijgegevens = null;
                if (leasemaatschappijCookie != null)
                {
                    leasemaatschappijgegevens = serializer.Deserialize<InsertLeasemaatschappijGegevensVM>(leasemaatschappijCookie.Value);
                }

                HttpCookie klantgegevensCookie = Request.Cookies.Get("Klantgegevens");
                var klantgegevens = serializer.Deserialize<InsertKlantgegevensVM>(klantgegevensCookie.Value);


                HttpCookie voertuiggegevensCookie = new HttpCookie("Voertuiggegevens", serializedVoertuiggegevens);
                Response.Cookies.Add(voertuiggegevensCookie);

                var voertuig = Mapper.MapToVoertuig(leasemaatschappijgegevens, klantgegevens, model);

                _agent.VoegVoertuigMetKlantToe(voertuig);

                return RedirectToAction("InsertOnderhoudsopdracht");
            }

            return View(model);
        }

        /// <summary>
        /// Show the form to insert onderhoudsopdracht
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertOnderhoudsopdracht()
        {
            if (!AreCookieSet("Klantgegevens", "Voertuiggegevens"))
            {
                return RedirectToAction("InsertKlantgegevens");
            }
            return View();
        }

        /// <summary>
        /// Post the inserted data from a onderhoudsopdracht and send it to all of the data to the agent
        /// Deserialize the previous cookies
        /// </summary>
        /// <param name="model">Inserted onderhoudsopdracht viewmodel</param>
        /// <returns>RedirectToAction to Index</returns>
        [HttpPost]
        public ActionResult InsertOnderhoudsopdracht(InsertOnderhoudsopdrachtVM model)
        {
            if (ModelState.IsValid)
            {
                var serializer = new JavaScriptSerializer();

                HttpCookie leasemaatschappijCookie = Request.Cookies.Get("LeasemaatschappijGegevens");
                model.AanmeldingsDatum = DateTime.Now;

                InsertLeasemaatschappijGegevensVM leasemaatschappijgegevens = null;
                if (leasemaatschappijCookie != null)
                {
                    leasemaatschappijgegevens = serializer.Deserialize<InsertLeasemaatschappijGegevensVM>(leasemaatschappijCookie.Value);
                    leasemaatschappijCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(leasemaatschappijCookie);
                }

                HttpCookie klantgegevensCookie = Request.Cookies.Get("Klantgegevens");
                var klantgegevens = serializer.Deserialize<InsertKlantgegevensVM>(klantgegevensCookie.Value);
                klantgegevensCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(klantgegevensCookie);

                HttpCookie voertuiggegevensCookie = Request.Cookies.Get("Voertuiggegevens");
                var voertuiggegevens = serializer.Deserialize<InsertVoertuiggegevensVM>(voertuiggegevensCookie.Value);
                voertuiggegevensCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(voertuiggegevensCookie);

                var onderhoudsopdracht = Mapper.MapToOnderhoudsopdracht(model, leasemaatschappijgegevens, klantgegevens, voertuiggegevens);
                HttpCookie onderhoudsCookie = new HttpCookie("Onderhoudsopdracht", serializer.Serialize(onderhoudsopdracht));
                Response.Cookies.Add(onderhoudsCookie);

                _agent.AddOnderhoudsopdrachtWithKlantAndVoertuig(onderhoudsopdracht);

                return RedirectToAction("InsertedOnderhoudsopdracht");
            }

            return View(model);
        }

        /// <summary>
        /// Displays the onderhoudsopdrachtCookie on the screen
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InsertedOnderhoudsopdracht()
        {
            HttpCookie onderhoudsopdrachtCookie = Request.Cookies.Get("Onderhoudsopdracht");

            if (onderhoudsopdrachtCookie == null)
            {
                return RedirectToAction("Index");
            }

            var onderhoudsopdracht = new JavaScriptSerializer().Deserialize<Onderhoudsopdracht>(onderhoudsopdrachtCookie.Value);
            onderhoudsopdrachtCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(onderhoudsopdrachtCookie);

            return View(onderhoudsopdracht);
        }

        /// <summary>
        /// Methode that look if all cookies are set
        /// </summary>
        /// <param name="names">params string with cookie names</param>
        /// <returns>true is all cookies are set otherwise false</returns>
        private bool AreCookieSet(params string[] names)
        {
            if (!names.Any())
            {
                return false;
            }

            bool cookiesSet = true;
            foreach (var name in names)
            {
                HttpCookie cookie = Request.Cookies.Get(name);
                if (cookie == null)
                {
                    cookiesSet = false;
                    break;
                }
            }
            return cookiesSet;
        }
    }
}
