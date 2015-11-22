using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;

namespace Minor.Case2.FEGMS.Agent
{
    public class AgentPcSOnderhoud
    {
        private ServiceFactory<IPcSOnderhoudService> _factory;

        public AgentPcSOnderhoud()
        {
            _factory = new ServiceFactory<IPcSOnderhoudService>("PcSOnderhoud");
        }
        public void AddOnderhoudsOpdrachtWithKlantAndVoertuig(Onderhoudsopdracht opdracht)
        {
            var proxy = _factory.CreateAgent();
            proxy.VoegOnderhoudsopdrachtToeMetVoertuigEnKlant(opdracht);
        }
    }
}
