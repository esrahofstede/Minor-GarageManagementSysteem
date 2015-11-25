using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Agent;
using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Minor.Case2.FEGMS.Client.Controllers
{
    public class MonteurController : Controller
    {
        IAgentPcSOnderhoud _agent;

        /// <summary>
        /// Contstructor to instantiate the agent
        /// </summary>
        public MonteurController()
        {
            _agent = new AgentPcSOnderhoud();
        }

        /// <summary>
        /// Creates an instance of the MonteurController and can be injected with an IAgentPcSOnderhoud
        /// </summary>
        /// <param name="agent">Injectable IAgentPcSOnderhoud</param>
        public MonteurController(IAgentPcSOnderhoud agent)
        {
            _agent = agent;
        }

        /// <summary>
        /// Show the form to Search Auto For Werkzaamheden
        /// </summary>
        /// <returns>View</returns>
        public ActionResult SearchAutoForWerkzaamheden()
        {
            return View();
        }

        /// <summary>
        /// Post Search Auto For Werkzaamheden
        /// Calls the agent to get onderhoudsopdracht
        /// </summary>
        /// <param name="search">Viewmodel to search on kenteken</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult SearchAutoForWerkzaamheden(SearchVM search)
        {
            if (ModelState.IsValid)
            {
                var searchCriteria = new OnderhoudsopdrachtZoekCriteria
                {
                    VoertuigenSearchCriteria = new VoertuigenSearchCriteria
                    {
                        Kenteken = search.Kenteken,
                    },
                };

                var onderhoudsopdracht = _agent.GetOnderhoudsopdrachtBy(searchCriteria);
                if (onderhoudsopdracht == null)
                {
                    ModelState.AddModelError("Kenteken", $"Er is geen onderhoudsopdracht gevonden voor de auto met het kenteken {search.Kenteken}");
                    return View(search);
                }

                OnderhoudswerkzaamhedenVM onderhoudswerkzaamheden = new OnderhoudswerkzaamhedenVM
                {
                    Kilometerstand = onderhoudsopdracht.Kilometerstand,
                    Onderhoudsomschrijving = onderhoudsopdracht.Onderhoudsomschrijving,
                    OnderhoudsopdrachtID = onderhoudsopdracht.ID,
                };

                var serializedOnderhoudswerkzaamheden = new JavaScriptSerializer().Serialize(onderhoudswerkzaamheden);
                HttpCookie onderhoudswerkzaamhedenCookie = new HttpCookie("Onderhoudswerkzaamheden", serializedOnderhoudswerkzaamheden);
                Response.Cookies.Add(onderhoudswerkzaamhedenCookie);

                return RedirectToAction("OnderhoudswerkzaamhedenInvoeren");
            }
            return View(search);
        }

        /// <summary>
        /// Shows a form to insert the Ondehoudswerkzaamheden
        /// Field already filled with data and read that data from the cookie SearchAutoForWerkzaamheden
        /// </summary>
        /// <returns>View or redirect to SearchAutoForWerkzaamheden when cookie is not set</returns>
        public ActionResult OnderhoudswerkzaamhedenInvoeren()
        {
            HttpCookie onderhoudswerkzaamhedenCookie = Request.Cookies.Get("Onderhoudswerkzaamheden");

            if(onderhoudswerkzaamhedenCookie == null)
            {
                return RedirectToAction("SearchAutoForWerkzaamheden");
            }

            var onderhoudswerkzaamheden = new JavaScriptSerializer().Deserialize<OnderhoudswerkzaamhedenVM>(onderhoudswerkzaamhedenCookie.Value);
            onderhoudswerkzaamhedenCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(onderhoudswerkzaamhedenCookie);

            return View(onderhoudswerkzaamheden);
        }

        /// <summary>
        /// Post the form for Ondehoudswerkzaamheden
        /// Calls the agent to insert the Ondehoudswerkzaamheden
        /// Set a cookie with the status of the RDW
        /// "steekproef" : "!steekproef" : "geen" are the different options 
        /// </summary>
        /// <param name="model">OnderhoudswerkzaamhedenVM</param>
        /// <returns>Redirect to Status</returns>
        [HttpPost]
        public ActionResult OnderhoudswerkzaamhedenInvoeren(OnderhoudswerkzaamhedenVM model)
        {
            if(ModelState.IsValid)
            {

                var werkzaamheden = new Onderhoudswerkzaamheden
                {
                    Afmeldingsdatum = DateTime.Now,
                    Kilometerstand = model.Kilometerstand,
                    Onderhoudswerkzaamhedenomschrijving = model.Onderhoudsomschrijving,
                    Onderhoudsopdracht = new Onderhoudsopdracht
                    {
                        ID = model.OnderhoudsopdrachtID,
                    },
                };

                bool? steekproef = _agent.VoegOnderhoudswerkzaamhedenToe(werkzaamheden);

                HttpCookie apkCookie = new HttpCookie("APK", steekproef.HasValue ? steekproef.Value ? "steekproef" : "!steekproef" : "geen");
                Response.Cookies.Add(apkCookie);

                return RedirectToAction("Status");
            }
            return View(model);
        }

        /// <summary>
        /// Shows the status of the RDW
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Status()
        {
            HttpCookie apkCookie = Request.Cookies.Get("APK");

            if (apkCookie != null)
            {
                apkCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(apkCookie);
                bool? steekproef = false;

                if (apkCookie.Value == "steekproef")
                {
                    steekproef = true;
                }
                else if (apkCookie.Value == "!steekproef")
                {
                    steekproef = false;
                }
                else if (apkCookie.Value == "geen")
                {
                    steekproef = null;
                }

                return View(steekproef);
            }

            return RedirectToAction("Index", "Onderhoud");

        }

        /// <summary>
        /// Show the form to show Onderhoudsopdracht
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Onderhoudsopdracht()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a Onderhoudsopdracht
        /// </summary>
        /// <param name="model">OnderhoudsopdrachtVM with kenteken</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Onderhoudsopdracht(OnderhoudsopdrachtVM model)
        {
            if (ModelState.IsValid)
            {
                var searchCriteria = new OnderhoudsopdrachtZoekCriteria
                {
                    VoertuigenSearchCriteria = new VoertuigenSearchCriteria
                    {
                        Kenteken = model.Kenteken,
                    },
                };

                model.Onderhoudsopdracht = _agent.GetOnderhoudsopdrachtBy(searchCriteria);

                if (model.Onderhoudsopdracht == null)
                {
                    model.Message = $"Voor de auto met het kenteken {model.Kenteken} kon geen onderhoudsopdracht gevonden worden";
                }
            }
            return View(model);
        }
    }
}