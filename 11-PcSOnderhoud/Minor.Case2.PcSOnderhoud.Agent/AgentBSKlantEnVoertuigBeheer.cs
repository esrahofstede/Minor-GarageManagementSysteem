using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System;
using System.ServiceModel;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    public class AgentBSKlantEnVoertuigBeheer
    {
        private ServiceFactory<IBSVoertuigEnKlantbeheer> _factory;

        public AgentBSKlantEnVoertuigBeheer() {}

        [CLSCompliant(false)]
        public AgentBSKlantEnVoertuigBeheer(ServiceFactory<IBSVoertuigEnKlantbeheer> factory)
        {
            _factory = factory;
        }

        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            var proxy = _factory.CreateAgent();
            try
            {
                proxy.VoegVoertuigMetKlantToe(voertuig);
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                
            }
            
        }

        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            var proxy = _factory.CreateAgent();
            proxy.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
        }

        public KlantenCollection GetAllKlanten()
        {
            KlantenCollection klanten;
            var proxy = _factory.CreateAgent();
            try
            {
                klanten = proxy.GetAllKlanten();
                return klanten;
                
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {

            }
            return null;
        }
    }
}