﻿using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Agent;
using Minor.Case2.FEGMS.Client.ViewModel;
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

        /// <summary>
        /// Show the form to Klaarmelden
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Klaarmelden()
        {
            return View();
        }

        /// <summary>
        /// Post the inserted data from a KlaarmeldenVM
        /// </summary>
        /// <param name="model">KlaarmeldenVM with kenteken</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Klaarmelden(KlaarmeldenVM model)
        {
            if (ModelState.IsValid)
            {
                var searchCriteria = new VoertuigenSearchCriteria
                {
                    Kenteken = model.Kenteken,
                };

                var voertuigen = _agent.GetVoertuigBy(searchCriteria);

                if(voertuigen.Any())
                {
                    model.Voertuig = voertuigen.First();
                    model.Steekproef = _agent.MeldVoertuigKlaar(model.Voertuig);
                    if(model.Steekproef)
                    {
                        model.Message = $"De auto met het kenteken {model.Kenteken} is klaargemeld.";
                    }
                    else
                    {
                        model.Message = $"De auto met het kenteken {model.Kenteken} is afgemeld.";
                    }
                }
                else
                {
                    model.Message = $"De auto met het kenteken {model.Kenteken} kon niet worden gevonden in het systeem.";
                }
            }
            return View(model);
        }
    }
}