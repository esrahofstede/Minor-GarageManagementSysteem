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
        /// Show the form to Search Auto For Werkzaamheden
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult SearchAutoForWerkzaamheden(SearchVM search)
        {
            if(ModelState.IsValid)
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

        ///// <summary>
        ///// Post the inserted data from a KlaarmeldenVM
        ///// </summary>
        ///// <param name="model">KlaarmeldenVM with kenteken</param>
        ///// <returns>View</returns>
        //[HttpPost]
        //public ActionResult Klaarmelden(OnderhoudswerkzaamhedenVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var searchCriteria = new VoertuigenSearchCriteria
        //        //{
        //        //    Kenteken = model.Kenteken,
        //        //};

        //        var werkzaamheden = new Onderhoudswerkzaamheden
        //        {
        //            Afmeldingsdatum = model.Afmeldingsdatum,
        //            Kilometerstand = model.Kilometerstand,
        //            Onderhoudswerkzaamhedenomschrijving = "Bla 123",// model.Onderhoudsomschrijving,
        //            Onderhoudsopdracht = new Onderhoudsopdracht
        //            {
        //                ID = model.OnderhoudsopdrachtID,
        //                Aanmeldingsdatum = DateTime.Now,
        //                APK = true,
        //                Kilometerstand = 123,
        //                Onderhoudsomschrijving = "Bla",
        //                Voertuig = new Voertuig
        //                {
        //                    Kenteken = "00-00-00",
        //                    Merk = "VW",
        //                    Type = "Polo",
        //                    Eigenaar = new Persoon
        //                    {
        //                        Voornaam = "Kees",
        //                        Achternaam = "Caespi",
        //                        Telefoonnummer = "0612345678",
        //                    }
        //                },


        //            },
        //        };

        //        //var voertuigen = _agent.GetVoertuigBy(searchCriteria);

        //        model.Steekproef = _agent.VoegOnderhoudswerkzaamhedenToe(werkzaamheden);

        //        //if (model.Steekproef)
        //        //{
        //        //    model.Message = $"De auto met het kenteken {model.Kenteken} is klaargemeld.";
        //        //}
        //        //else
        //        //{
        //        //    model.Message = $"De auto met het kenteken {model.Kenteken} is afgemeld.";
        //        //}

        //    }
        //    return View("Klaargemeld", model);
        //}

        /// <summary>
        /// Show the form to show Onderhoudsopdracht
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Onderhoudsopdracht()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a KlaarmeldenVM
        /// </summary>
        /// <param name="model">KlaarmeldenVM with kenteken</param>
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