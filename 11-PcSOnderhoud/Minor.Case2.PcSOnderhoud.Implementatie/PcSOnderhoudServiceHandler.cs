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
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;

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
        /// Een constructor waarmee een custom BS Agent geinjecteerd kan worden
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
            try
            {
                return _agentBS.GetAllLeasemaatschappijen();
            }
            catch (FunctionalException ex)
            {
                throw new FaultException<FunctionalErrorDetail[]>(ex.Errors.Details);

            }
            catch (TechnicalException ex)
            {
                throw new FaultException(ex.Message);
            }
            
        }

        /// <summary>
        /// Deze methode haalt alle voertuigen op die voldoen aan de ingevulde criteria. Als geen voertuigen gevonden zijn, 
        /// wordt een lege VoertuigenCollection verstuurd
        /// </summary>
        /// <param name="searchCriteria">De criteria waarop gezocht kan wroden naar een voertuig</param>
        /// <returns>Alle voertuigen die voldoen aan de gestelde criteria</returns>
        public Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria searchCriteria)
        {
            try
            {
                return _agentBS.GetVoertuigBy(searchCriteria);
            }
            catch (FunctionalException ex)
            {
                throw new FaultException<FunctionalErrorDetail[]>(ex.Errors.Details);

            }
            catch (TechnicalException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Schema.VoertuigenCollection HaalVoertuigenOpVoor(Schema.Persoon persoon)
        {
            Schema.VoertuigenCollection voertuigen = new Schema.VoertuigenCollection();
            var personen = from persoonAs in _agentBS.GetAllPersonen()
                            select persoonAs as Schema.Persoon;
            var filteredPersonen = personen.Where(
                p => p.Achternaam == persoon.Achternaam && 
                p.Tussenvoegsel == persoon.Tussenvoegsel &&
                p.Voornaam == persoon.Voornaam &&
                p.Telefoonnummer == persoon.Telefoonnummer).ToList();
            if (filteredPersonen.Count() == 1 )
            {
                var searchCriteria = new Schema.VoertuigenSearchCriteria
                {
                    Bestuurder = new Schema.Persoon
                    {
                        ID = filteredPersonen.First().ID
                    }
                };
                voertuigen = _agentBS.GetVoertuigBy(searchCriteria);

            }
            return voertuigen;
        }

        /// <summary>
        /// Deze methode stuurt een nieuwe onderhoudsopdracht naar de BS
        /// Het is verplicht om een onderzoeksopdracht met daarin een voertuig op te geven
        /// </summary>
        /// <param name="onderhoudsopdracht">De onderzoekopdracht die verstuurt wordt, met daarbij het ook het voertuig</param>
        public void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                if (onderhoudsopdracht == null)
                {
                    throw new FaultException("Onderhoudsopdracht cannot be null");
                }
                if (onderhoudsopdracht.Voertuig == null)
                {
                    throw new FaultException("Voertuig cannot be null");
                }
                onderhoudsopdracht.Voertuig.Status = "Aangemeld";
                _agentBS.VoegOnderhoudsopdrachtToe(onderhoudsopdracht);
            }
            catch (FunctionalException ex)
            {
                throw new FaultException<FunctionalErrorDetail[]>(ex.Errors.Details);

            }
            catch (TechnicalException ex)
            {
                throw new FaultException(ex.Message);
            }
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
            try
            {
                var onderhoudsopdrachten = _agentBS.GetOnderhoudsopdrachtenBy(searchCriteria);
                if (onderhoudsopdrachten.Count == 0)
                {
                    return null;
                }
                var onderhoudsopdracht = onderhoudsopdrachten.OrderByDescending(o => o.Aanmeldingsdatum).FirstOrDefault();
                return onderhoudsopdracht;
            }
            catch (FunctionalException ex)
            {
                throw new FaultException<FunctionalErrorDetail[]>(ex.Errors.Details);

            }
            catch (TechnicalException ex)
            {
                throw new FaultException(ex.Message);
            }
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

        /// <summary>
        /// Deze methode voegt een nieuw voertuig met een nieuwe klant toe aan de BS
        /// Voertuig, Eigenaar en Bestuurder moeten ingevuld zijn.
        /// </summary>
        /// <param name="voertuig">Het nieuwe voertuig met daarbij ook de Bestuurder en Eigenaar</param>
        public void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig)
        {
            try
            {
                if (voertuig == null)
                {
                    throw new TechnicalException("Voertuig cannot be null");
                }
                if (voertuig.Bestuurder == null)
                {
                    throw new TechnicalException("Bestuurder cannot be null");
                }
                if (voertuig.Eigenaar == null)
                {
                    throw new TechnicalException("Eigenaar cannot be null");
                }
                voertuig.Status = "Aangemeld";
                _agentBS.VoegVoertuigMetKlantToe(voertuig);
            }
            catch (FunctionalException ex)
            {
                throw new FaultException<FunctionalErrorDetail[]>(ex.Errors.Details);

            }
            catch (TechnicalException ex)
            {
                throw new FaultException(ex.Message);
            }


        }
    }
}
