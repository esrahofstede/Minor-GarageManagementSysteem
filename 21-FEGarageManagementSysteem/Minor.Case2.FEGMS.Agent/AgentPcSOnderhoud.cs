using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System.ServiceModel;

namespace Minor.Case2.FEGMS.Agent
{
    /// <summary>
    /// Connection to PcSOnderhoud
    /// </summary>
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
            
            //proxy.
            
            //proxy..VoegOnderhoudsopdrachtToeMetVoertuigEnKlant(opdracht);
        }

        /// <summary>
        /// Submit Voertuig and Klant to the PcSOnderhoud
        /// </summary>
        /// <param name="voertuig">Voertuig and Klant</param>
        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            try {
                var proxy = _factory.CreateAgent();
                proxy.VoegVoertuigMetKlantToe(voertuig);
            }
            catch(FaultException ex)
            {

            }
        }
    }
}
