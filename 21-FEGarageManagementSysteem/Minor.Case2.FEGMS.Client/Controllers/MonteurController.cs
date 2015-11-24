using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Agent;
using Minor.Case2.FEGMS.Client.ViewModel;
using System;
using System.Collections.Generic;
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
                    _agent.MeldVoertuigKlaar(model.Voertuig);           
                }
            }
            return View(model);
        }
    }
}