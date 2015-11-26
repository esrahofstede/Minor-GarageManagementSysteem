using Minor.ServiceBus.Agent.Implementation;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using AgentISSchema = Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using AgentISMessages = Minor.Case2.ISRijksdienstWegverkeerService.V1.Messages.Agent;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentBSSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    /// <summary>
    /// Deze agent is verantwoordelijk voor de communicatie met de ISRDWService
    /// </summary>
    public class AgentISRDW : IAgentISRDW
    {
        private ServiceFactory<IISRDWService> _factory;
        /// <summary>
        /// Standaard constructor die een nieuwe ServiceFactory maakt voor de ISRDWService
        /// </summary>
        public AgentISRDW()
        {
            _factory = new ServiceFactory<IISRDWService>("ISRDWService");
        }

        /// <summary>
        /// Aan deze constructor kan een custom ServiceFactory meegegeven worden
        /// Niet CLS compliant omdat de servicefactory generic is
        /// </summary>
        /// <param name="factory"></param>
        [CLSCompliant(false)]
        public AgentISRDW(ServiceFactory<IISRDWService> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Deze methode verstuurt een APK keuringsverzoek naar de IS service
        /// </summary>
        /// <param name="voertuig">Het voertuig waarvoor de apk keuring is gedaan</param>
        /// <param name="garage">De garage die de keuring verstuurt</param>
        /// <param name="keuringsverzoek">Parameters voor het keuringsverzoek</param>
        /// <returns>Het antwoord van de IS</returns>
        public AgentISMessages.SendRdwKeuringsverzoekResponseMessage SendAPKKeuringsverzoek(Schema.Voertuig voertuig, AgentISSchema.Garage garage, AgentISSchema.Keuringsverzoek keuringsverzoek)
        {
            if (keuringsverzoek == null)
            {
                throw new TechnicalException("Keuringsverzoek mag niet null zijn");
            }
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
