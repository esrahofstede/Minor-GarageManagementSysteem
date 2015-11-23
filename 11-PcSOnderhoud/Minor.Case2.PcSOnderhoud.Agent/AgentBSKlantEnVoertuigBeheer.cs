using log4net;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System;
using System.ServiceModel;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    public class AgentBSKlantEnVoertuigBeheer
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AgentBSKlantEnVoertuigBeheer));
        private readonly ServiceFactory<IBSVoertuigEnKlantbeheer> _factory;

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
            try
            {
                var proxy = _factory.CreateAgent();
                proxy.VoegVoertuigMetKlantToe(voertuig);
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                
            }
            catch (InvalidOperationException ex)
            {
                Logger.Fatal(ex.InnerException.Message);
            }
            
        }

        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            var proxy = _factory.CreateAgent();
            proxy.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
        }

        public KlantenCollection GetAllKlanten()
        {
            var proxy = _factory.CreateAgent();
            try
            {
                var klanten = proxy.GetAllKlanten();
                return klanten;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {

            }
            return null;
        }
    }
}