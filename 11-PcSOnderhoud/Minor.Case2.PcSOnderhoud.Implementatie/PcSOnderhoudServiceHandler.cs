using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.ServiceModel;
using Minor.Case2.PcSOnderhoud.Contract;
using Minor.Case2.PcSOnderhoud.Agent;

namespace Minor.Case2.PcSOnderhoud.Implementation
{
    public class PcSOnderhoudServiceHandler : IPcSOnderhoudService
    {
        public PcSOnderhoudServiceHandler()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public KlantenCollection GetAllKlanten()
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            return null;
        }

        public VoertuigenCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            return agent.GetVoertuigBy(zoekCriteria);
        }

        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
        }

        public void MeldVoertuigKlaar(Voertuig voertuig)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            agent.UpdateVoertuig(voertuig);
        }
        
        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            agent.VoegVoertuigMetKlantToe(voertuig);
            
        }
    }
}
