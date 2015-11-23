using log4net;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System;
using System.ServiceModel;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    public class AgentBSKlantEnVoertuigBeheer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AgentBSKlantEnVoertuigBeheer));
        private ServiceFactory<IBSVoertuigEnKlantbeheer> _factory;

        public AgentBSKlantEnVoertuigBeheer()
        {
            _factory = new ServiceFactory<IBSVoertuigEnKlantbeheer>("BSVoertuigEnKlantBeheer");
        }

        [CLSCompliant(false)]
        public AgentBSKlantEnVoertuigBeheer(ServiceFactory<IBSVoertuigEnKlantbeheer> factory)
        {
            _factory = factory;
        }

        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            IBSVoertuigEnKlantbeheer proxy;
            try
            {
                proxy = _factory.CreateAgent();
                proxy.VoegVoertuigMetKlantToe(voertuig);
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                
            }
            catch (InvalidOperationException ex)
            {
                logger.Fatal(ex.InnerException.Message);
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