using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.ServiceModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using Minor.Case2.PcSOnderhoud.Contract;
using Minor.Case2.PcSOnderhoud.Agent;

namespace Minor.Case2.PcSOnderhoud.Implementation
{
    /// <summary>
    /// De implementatie van het Contract
    /// In deze classes staan alle methodes die aangeroepen kunnen worden.
    /// </summary>
    public class PcSOnderhoudServiceHandler : IPcSOnderhoudService
    {
        private readonly IAgentBSVoertuigEnKlantBeheer _agentBS;

        /// <summary>
        /// Standaard constructor die log4net configureert en een instantie van de BSAgent aanmaakt
        /// </summary>
        public PcSOnderhoudServiceHandler()
        {
            log4net.Config.XmlConfigurator.Configure();
            _agentBS = new AgentBSVoertuigEnKlantBeheer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentBS"></param>
        public PcSOnderhoudServiceHandler(IAgentBSVoertuigEnKlantBeheer agentBS)
        {
            _agentBS = agentBS;
        }

        /// <summary>
        /// Deze methode haalt alle leasemaatschappijen op die in de BS te vinden zijn
        /// Als geen leasemaatschappijen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <returns>Alle leasemaatschappijen die in de BS te vinden zijn, lege lijst als geen leasemaatschappijen gevonden zijn</returns>
        public Schema.KlantenCollection GetAllLeasemaatschappijen()
        {
            return _agentBS.GetAllLeasemaatschappijen(); ;
        }

        public Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria searchCriteria)
        {
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            return agent.GetVoertuigBy(searchCriteria);
        }

        public Schema.VoertuigenCollection HaalVoertuigenOpVoor(Schema.Persoon persoon)
        {
            throw new NotImplementedException();
        }

        public void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            if (onderhoudsopdracht == null)
            {
                return;
            }
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            onderhoudsopdracht.Voertuig.Status = "Aangemeld";
            agent.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
        }

        public bool MeldVoertuigKlaar(Schema.Voertuig voertuig, Garage garage)
        {
            if (voertuig == null || garage == null)
            {
                return false;
            }
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

        public bool? VoegOnderhoudswerkzaamhedenToe(Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden, Garage garage)
        {
            bool? steekproef = null;
            if (onderhoudswerkzaamheden == null)
            {
                FunctionalErrorDetail error = new FunctionalErrorDetail {Message = "Onderhoudswerkzaamheden mogen niet null zijn"};
                throw new FaultException<FunctionalErrorDetail[]>(new FunctionalErrorDetail[] {error});
            }
            AgentBSVoertuigEnKlantBeheer agentBS = new AgentBSVoertuigEnKlantBeheer();
            AgentISRDW agentIS = new AgentISRDW();

            Schema.OnderhoudsopdrachtZoekCriteria zoekCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                ID = onderhoudswerkzaamheden.Onderhoudsopdracht.ID
            };
            var onderhoudsopdrachten = agentBS.GetOnderhoudsopdrachtenBy(zoekCriteria);
            var onderhoudsopdracht = onderhoudsopdrachten.First();
            if (onderhoudsopdracht.APK)
            {
                Keuringsverzoek keuringsverzoek = new Keuringsverzoek
                {
                    Kilometerstand = (int)onderhoudswerkzaamheden.Kilometerstand,
                    Date = onderhoudswerkzaamheden.Afmeldingsdatum,
                    CorrolatieId = Guid.NewGuid().ToString()
                };

                onderhoudsopdracht.Voertuig.Status = "Klaar";

                steekproef = agentIS.SendAPKKeuringsverzoek(onderhoudsopdracht.Voertuig, garage, keuringsverzoek).Steekproef;
            }

            if (steekproef == null)
            {
                onderhoudsopdracht.Voertuig.Status = "Afgemeld";
            }
            
            agentBS.UpdateVoertuig(onderhoudsopdracht.Voertuig);
            agentBS.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);
            return steekproef;
        }

        public void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig)
        {
            if (voertuig == null)
            {
                return;
            }
            AgentBSVoertuigEnKlantBeheer agent = new AgentBSVoertuigEnKlantBeheer();
            voertuig.Status = "Aangemeld";
            agent.VoegVoertuigMetKlantToe(voertuig);
            
        }
    }
}
