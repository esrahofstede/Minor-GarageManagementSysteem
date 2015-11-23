using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    public class AgentBSKlantEnVoertuigBeheer
    {
        private ServiceFactory<IBSVoertuigEnKlantbeheer> _factory;

        public AgentBSKlantEnVoertuigBeheer()
        {
        }

        public AgentBSKlantEnVoertuigBeheer(ServiceFactory<IBSVoertuigEnKlantbeheer> factory)
        {
            _factory = factory;
        }

        public void VoegOnderhoudsopdrachtMetVoertuigEnKlantToe(Voertuig voertuig)
        {
            throw new System.NotImplementedException();
        }
    }
}