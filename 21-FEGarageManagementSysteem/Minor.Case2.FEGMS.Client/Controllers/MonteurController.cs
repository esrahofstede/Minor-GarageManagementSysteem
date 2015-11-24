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

        // GET: Monteur
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Klaarmelden()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Klaarmelden(KlaarmeldenVM model)
        {
            if (ModelState.IsValid)
            {
                var searchCriteria = new VoertuigenSearchCriteria
                {
                    Kenteken = model.Kenteken,
                };

                var voertuigen = _agent.FindVoertuigBy(searchCriteria);

                if(voertuigen.Any())
                {
                    model.Voertuig = voertuigen.First();                   
                }
            }
            return View(model);
        }
    }
}