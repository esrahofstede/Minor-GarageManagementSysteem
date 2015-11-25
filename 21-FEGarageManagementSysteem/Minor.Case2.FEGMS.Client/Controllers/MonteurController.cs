using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Agent;
using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

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

        public ActionResult OnderhoudswerkzaamhedenInvoeren()
        {
            return View();
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
        public ActionResult SearchAutoForWerkzaamheden(string kenteken)
        {
            if(string.IsNullOrWhiteSpace(kenteken))
            {

            }
            
            if(ModelState.IsValid)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult OnderhoudswerkzaamhedenInvoeren(OnderhoudsopdrachtVM model)
        {
            if(ModelState.IsValid)
            {
                var searchCriteria = new OnderhoudsopdrachtZoekCriteria
                {
                    VoertuigenSearchCriteria = new VoertuigenSearchCriteria
                    {
                        Kenteken = model.Kenteken,
                    },
                };

                model.Onderhoudsopdracht = _agent.GetOnderhoudsopdrachtBy(searchCriteria);
                if (model.Onderhoudsopdracht != null)
                {
                    model.OnderhoudsopdrachtID = model.Onderhoudsopdracht.ID;
                }
            }
            return View(model);
        }

        /// <summary>
        /// Post the inserted data from a KlaarmeldenVM
        /// </summary>
        /// <param name="model">KlaarmeldenVM with kenteken</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Klaarmelden(OnderhoudsopdrachtVM model)
        {
            if (ModelState.IsValid)
            {
                //var searchCriteria = new VoertuigenSearchCriteria
                //{
                //    Kenteken = model.Kenteken,
                //};

                var werkzaamheden = new Onderhoudswerkzaamheden
                {
                    Afmeldingsdatum = model.Afmeldingsdatum,
                    Kilometerstand = model.Kilometerstand,
                    Onderhoudswerkzaamhedenomschrijving = "Bla 123",// model.Onderhoudsomschrijving,
                    Onderhoudsopdracht = new Onderhoudsopdracht
                    {
                        ID = model.OnderhoudsopdrachtID,
                        Aanmeldingsdatum = DateTime.Now,
                        APK = true,
                        Kilometerstand = 123,
                        Onderhoudsomschrijving = "Bla",
                        Voertuig = new Voertuig
                        {
                            Kenteken = "00-00-00",
                            Merk = "VW",
                            Type = "Polo",
                            Eigenaar = new Persoon
                            {
                                Voornaam = "Kees",
                                Achternaam = "Caespi",
                                Telefoonnummer = "0612345678",
                            }
                        },
                        
                        
                    },
                };

                //var voertuigen = _agent.GetVoertuigBy(searchCriteria);

                model.Steekproef = _agent.VoegOnderhoudswerkzaamhedenToe(werkzaamheden);

                if (model.Steekproef)
                {
                    model.Message = $"De auto met het kenteken {model.Kenteken} is klaargemeld.";
                }
                else
                {
                    model.Message = $"De auto met het kenteken {model.Kenteken} is afgemeld.";
                }

            }
            return View("Klaargemeld", model);
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