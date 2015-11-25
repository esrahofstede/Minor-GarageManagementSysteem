using Minor.ServiceBus.Agent.Implementation;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using AgentISSchema = Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentISMessages = Minor.Case2.ISRijksdienstWegverkeerService.V1.Messages.Agent;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentBSSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    
    public class AgentISRDW
    {
        private ServiceFactory<IISRDWService> _factory;

        public AgentISRDW()
        {
            _factory = new ServiceFactory<IISRDWService>("ISRDWService");
        }

        [CLSCompliant(false)]
        public AgentISRDW(ServiceFactory<IISRDWService> factory)
        {
            _factory = factory;
        }
        public AgentISMessages.SendRdwKeuringsverzoekResponseMessage SendAPKKeuringsverzoek(Schema.Voertuig voertuig, AgentISSchema.Garage garage, AgentISSchema.Keuringsverzoek keuringsverzoek)
        {
            var proxy =_factory.CreateAgent();
            keuringsverzoek.Date = DateTime.Now;
            keuringsverzoek.CorrolatieId = Guid.NewGuid().ToString();
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
            var apkKeuringsverzoek = new AgentISMessages.SendRdwKeuringsverzoekRequestMessage
            {
                Voertuig = mapper.SchemaToAgentVoertuigMapper(voertuig),
                Garage =  garage,
                Keuringsverzoek = keuringsverzoek
            };
            AgentISMessages.SendRdwKeuringsverzoekResponseMessage result = proxy.RequestKeuringsverzoek(apkKeuringsverzoek);
            return result;
        }
    }
}
