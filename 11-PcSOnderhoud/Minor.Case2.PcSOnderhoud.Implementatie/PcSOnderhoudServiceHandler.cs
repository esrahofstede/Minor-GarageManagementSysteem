using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.ServiceModel;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
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

        public Schema.KlantenCollection GetAllKlanten()
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            return null;
        }

        public Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria zoekCriteria)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            return agent.GetVoertuigBy(zoekCriteria);
        }

        public void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            onderhoudsopdracht.Voertuig.Status = "Aangemeld";
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
        }

        public bool MeldVoertuigKlaar(Schema.Voertuig voertuig)
        {
            AgentBSKlantEnVoertuigBeheer agentBS = new AgentBSKlantEnVoertuigBeheer();
            AgentISRDW agentIS = new AgentISRDW();
            Garage garage = new Garage
            {
                Kvk = "123245645",
                Naam = "Caespi",
                Type = "Garage",
                Plaats = "Deventer"
            };
            Keuringsverzoek keuringsverzoek = new Keuringsverzoek
            {
                Kilometerstand = 100000,
                Date = DateTime.Now, 
                CorrolatieId = Guid.NewGuid().ToString()
            };
            voertuig.Status = "Klaar";
            agentBS.UpdateVoertuig(voertuig);
            return agentIS.SendAPKKeuringsverzoek(voertuig, garage, keuringsverzoek).Steekproef;
        }
        
        public void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig)
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            voertuig.Status = "Aangemeld";
            agent.VoegVoertuigMetKlantToe(voertuig);
            
        }
    }
}
