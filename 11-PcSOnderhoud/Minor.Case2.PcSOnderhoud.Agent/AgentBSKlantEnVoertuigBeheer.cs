using log4net;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System;
using System.Linq;
using System.ServiceModel;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

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

        public void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                proxy.VoegVoertuigMetKlantToe(mapper.SchemaToAgentVoertuigMapper(voertuig));
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                
            }
            catch (InvalidOperationException ex)
            {
                Logger.Fatal(ex.InnerException.Message);
            }
            
        }

        public void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                proxy.VoegOnderhoudsopdrachtToe(mapper.SchemaToAgentOnderhoudsopdrachtMapper(onderhoudsopdracht));
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {

            }
            catch (InvalidOperationException ex)
            {
                Logger.Fatal(ex.InnerException.Message);
            }
            
        }

        public void UpdateVoertuig(Schema.Voertuig voertuig)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                //proxy.UpdateVoertuig(mapper.SchemaToAgentVoertuigMapper(voertuig));
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {

            }
            catch (InvalidOperationException ex)
            {
                Logger.Fatal(ex.InnerException.Message);
            }

        }

        public Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria criteria)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                AgentSchema.VoertuigenCollection voertuigen = proxy
                    .GetVoertuigBy(mapper.SchemaToAgentVoertuigSearchCriteriaMapper(criteria));
                var query = from voertuig in voertuigen
                    select mapper.AgentToSchemaVoertuigMapper(voertuig);
                Schema.VoertuigenCollection voertuigenCollection = new Schema.VoertuigenCollection();
                voertuigenCollection.AddRange(query);
                return voertuigenCollection;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {

            }
            catch (InvalidOperationException ex)
            {
                Logger.Fatal(ex.InnerException.Message);
            }
            return new Schema.VoertuigenCollection();
        }

        public Schema.KlantenCollection GetAllKlanten()
        {
            var proxy = _factory.CreateAgent();
            try
            {
                var klanten = proxy.GetAllKlanten();
                return null;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {

            }
            return null;
        }
        
    }
}