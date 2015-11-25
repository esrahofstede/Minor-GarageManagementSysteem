using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.ServiceModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
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
        
        public Schema.KlantenCollection GetAllLeasemaatschappijen()
        {
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            return agent.GetAllLeasemaatschappijen(); ;
        }

        public Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria zoekCriteria)
        {
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            return agent.GetVoertuigBy(zoekCriteria);
        }

        public Schema.VoertuigenCollection HaalVoertuigenOpVoor(Schema.Persoon persoon)
        {
            throw new NotImplementedException();
        }

        public void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            onderhoudsopdracht.Voertuig.Status = "Aangemeld";
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
        }

        public bool MeldVoertuigKlaar(Schema.Voertuig voertuig, Garage garage)
        {
            AgentBSVoertuigEnKlantBeheer agentBS = new AgentBSVoertuigEnKlantBeheer();
            AgentISRDW agentIS = new AgentISRDW();
            
            Keuringsverzoek keuringsverzoek = new Keuringsverzoek
            {
                Kilometerstand = 100000,
                Date = DateTime.Now, 
                CorrolatieId = Guid.NewGuid().ToString()
            };
            voertuig.Status = "Klaar";
            
            bool steekproef = agentIS.SendAPKKeuringsverzoek(voertuig, garage, keuringsverzoek).Steekproef;
            if (!steekproef)
            {
                voertuig.Status = "Afgemeld";
            }
            agentBS.UpdateVoertuig(voertuig);
            return steekproef;
        }

        public Schema.Onderhoudsopdracht GetHuidigeOnderhoudsopdrachtBy(Schema.OnderhoudsopdrachtZoekCriteria searchCriteria)
        {
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            var onderhoudsopdrachten = agent.GetOnderhoudsopdrachtenBy(searchCriteria);
            if (onderhoudsopdrachten.Count == 0)
            {
                return null;
            }
            Schema.Onderhoudsopdracht onderhoudsopdracht = onderhoudsopdrachten.OrderByDescending(o => o.Aanmeldingsdatum).FirstOrDefault();
            return onderhoudsopdracht;
        }

        public bool VoegOnderhoudswerkzaamhedenToe(Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden, Garage garage)
        {
            AgentBSVoertuigEnKlantBeheer agentBS = new AgentBSVoertuigEnKlantBeheer();
            AgentISRDW agentIS = new AgentISRDW();

            Schema.OnderhoudsopdrachtZoekCriteria zoekCriteria = new Schema.OnderhoudsopdrachtZoekCriteria();
            zoekCriteria.ID = onderhoudswerkzaamheden.Onderhoudsopdracht.ID;
            var onderhoudsopdrachten = agentBS.GetOnderhoudsopdrachtenBy(zoekCriteria);
            var onderhoudsopdracht = onderhoudsopdrachten.First();

            Keuringsverzoek keuringsverzoek = new Keuringsverzoek
            {
                Kilometerstand = (int) onderhoudswerkzaamheden.Kilometerstand,
                Date = onderhoudswerkzaamheden.Afmeldingsdatum,
                CorrolatieId = Guid.NewGuid().ToString()
            };
            onderhoudsopdracht.Voertuig.Status = "Klaar";

            bool steekproef = agentIS.SendAPKKeuringsverzoek(onderhoudsopdracht.Voertuig, garage, keuringsverzoek).Steekproef;
            if (!steekproef)
            {
                onderhoudsopdracht.Voertuig.Status = "Afgemeld";
            }
            agentBS.UpdateVoertuig(onderhoudsopdracht.Voertuig);
            agentBS.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);
            return steekproef;
        }

        public void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig)
        {
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            voertuig.Status = "Aangemeld";
            agent.VoegVoertuigMetKlantToe(voertuig);
            
        }
    }
}
