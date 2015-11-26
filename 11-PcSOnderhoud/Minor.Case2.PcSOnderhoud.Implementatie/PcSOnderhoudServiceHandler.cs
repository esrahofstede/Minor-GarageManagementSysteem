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
        private readonly IAgentISRDW _agentIS;

        /// <summary>
        /// Standaard constructor die log4net configureert en een instantie van de BSAgent en ISAgent aanmaakt
        /// </summary>
        public PcSOnderhoudServiceHandler()
        {
            log4net.Config.XmlConfigurator.Configure();
            _agentBS = new AgentBSVoertuigEnKlantBeheer();
            _agentIS = new AgentISRDW();
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
        /// Een constructor waar een custom BS agent en een custom IS Agent worden geinjecteerd.
        /// </summary>
        /// <param name="agentBS">De custom BS agent, moet IAgentBSVoertuigEnKlantBeheer implementeren</param>
        /// <param name="agentsIS">De custom IS agent, moet IAgentISRDW implementeren</param>
        public PcSOnderhoudServiceHandler(IAgentBSVoertuigEnKlantBeheer agentBS, IAgentISRDW agentsIS)
        {
            _agentBS = agentBS;
            _agentIS = agentsIS;
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
        /// Functionele fouten worden doorgestuurd naar de aanroeper
        /// Technische fouten worden doorgestuurd naar de aanroeper
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

        /// <summary>
        /// Deze methode haalt alle voertuigen op voor een een persoon
        /// Als de persoon nog niet bestaat, dan wordt deze toegevoegd in de BS
        /// Als de persoon wel bestaat, worden al zijn voertuigen terug gestuurt.
        /// De persoon wordt gefilterd op basis van voor- en achternaam en telefoonnummer.
        /// De voertuigen worden opgehaald op basis van het ID van de klant.
        /// </summary>
        /// <param name="persoon">De persoon waarvan de voertuigen opgehaald moeten worden</param>
        /// <returns>
        /// Null als er meerdere personen zjin gevonden, 
        /// Lege lijst als er geen voertuigen zijn gevonden, 
        /// of het een nieuwe klant is en een lijst met voertuigen als er voertuigen in de BS staan voor die klant
        /// </returns>
        public Schema.VoertuigenCollection HaalVoertuigenOpVoor(Schema.Persoon persoon)
        {
            if (persoon == null)
            {
                throw new FaultException("Persoon mag niet null zijn");
            }
            try
            {
                Schema.VoertuigenCollection voertuigen = new Schema.VoertuigenCollection();
                var personen = from persoonAs in _agentBS.GetAllPersonen()
                               select persoonAs as Schema.Persoon;
                var filteredPersonen = personen.Where(
                    p => p.Achternaam == persoon.Achternaam &&
                    p.Tussenvoegsel == persoon.Tussenvoegsel &&
                    p.Voornaam == persoon.Voornaam &&
                    p.Telefoonnummer == persoon.Telefoonnummer).ToList();

                if (filteredPersonen.Count() == 1)
                {
                    var searchCriteria = new Schema.VoertuigenSearchCriteria
                    {
                        Bestuurder = new Schema.Persoon
                        {
                            ID = filteredPersonen.First().ID,
                            Achternaam = filteredPersonen.First().Achternaam,
                            Voornaam = filteredPersonen.First().Voornaam,
                            Telefoonnummer = filteredPersonen.First().Telefoonnummer,
                        }
                    };
                    voertuigen = _agentBS.GetVoertuigBy(searchCriteria);
                }
                else if (filteredPersonen.Count > 1)
                {
                    return null;
                }
                return voertuigen;
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
        /// Deze methode stuurt een nieuwe onderhoudsopdracht naar de BS
        /// Het is verplicht om een onderzoeksopdracht met daarin een voertuig op te geven
        /// Functionele fouten worden doorgestuurd naar de aanroeper
        /// Technische fouten worden doorgestuurd naar de aanroeper
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

        /// <summary>
        /// Deze methode zoek de huidigeonderhoudsopdracht op uit de BS
        /// Uit de lijst met onderzoeksopdrachten wordt de niewste opdracht teruggegeven
        /// Het is verplicht om zoekcriteria op te geven.
        /// Functionele fouten worden doorgestuurd naar de aanroeper
        /// Technische fouten worden doorgestuurd naar de aanroeper
        /// </summary>
        /// <param name="searchCriteria">De criteria waarop gezocht moet worden in de BS</param>
        /// <returns></returns>
        public Schema.Onderhoudsopdracht GetHuidigeOnderhoudsopdrachtBy(Schema.OnderhoudsopdrachtZoekCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new FaultException("SearchCriteria mag niet nul zijn");
            }
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

        /// <summary>
        /// Deze methode verstuur nieuwe onderhoudswerkzaamheden naar de BS
        /// Als er een APK is uitgevoerd, wordt de APK verstuurt naar de IS
        /// De IS geeft terug of er wel of geen steekproef wordt uitgevoerd
        /// </summary>
        /// <param name="onderhoudswerkzaamheden">De onderhoudswerkzaamheden die verstuurt worden naar de BS</param>
        /// <param name="garage">De garage die de onderhoudswerkzaameheden verstuurt, nodig voor de apk check</param>
        /// <returns>Geeft null terug als geen APK is uitgevoerd, False als er geen steekproef is en True als er wel een steekproef is</returns>
        public bool? VoegOnderhoudswerkzaamhedenToe(Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden, Garage garage)
        {
            bool? steekproef = null;
            if (onderhoudswerkzaamheden == null)
            {
                FunctionalErrorDetail error = new FunctionalErrorDetail {Message = "Onderhoudswerkzaamheden mogen niet null zijn"};
                throw new FaultException<FunctionalErrorDetail[]>(new[] {error});
            }
            if (onderhoudswerkzaamheden.Onderhoudsopdracht == null)
            {
                FunctionalErrorDetail error = new FunctionalErrorDetail { Message = "Onderhoudsopdracht mag niet null zijn" };
                throw new FaultException<FunctionalErrorDetail[]>(new[] { error });
            }
            try
            {
                Schema.OnderhoudsopdrachtZoekCriteria zoekCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
                {
                    ID = onderhoudswerkzaamheden.Onderhoudsopdracht.ID
                };
                var onderhoudsopdrachten = _agentBS.GetOnderhoudsopdrachtenBy(zoekCriteria);
                if (onderhoudsopdrachten.Count == 0)
                {
                    FunctionalErrorDetail error = new FunctionalErrorDetail { Message = "Geen onderhoudsopdrachten gevonden" };
                    throw new FaultException<FunctionalErrorDetail[]>(new[] { error });
                }
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
                    var voertuigSearchCriteria = new Schema.VoertuigenSearchCriteria
                    {
                        ID = onderhoudsopdracht.Voertuig.ID
                    };
                    var voertuigen = _agentBS.GetVoertuigBy(voertuigSearchCriteria);
                    onderhoudsopdracht.Voertuig = voertuigen.First();
                    steekproef = _agentIS.SendAPKKeuringsverzoek(onderhoudsopdracht.Voertuig, garage, keuringsverzoek).Steekproef;
                }

                if (steekproef == null)
                {
                    onderhoudsopdracht.Voertuig.Status = "Afgemeld";
                }
            
                _agentBS.UpdateVoertuig(onderhoudsopdracht.Voertuig);
                _agentBS.VoegOnderhoudswerkzaamhedenToe(onderhoudswerkzaamheden);
                return steekproef;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
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
                var personen = from persoonAs in _agentBS.GetAllPersonen()
                               select persoonAs as Schema.Persoon;
                var filteredPersonen = personen.Where(
                    p => p.Achternaam == voertuig.Bestuurder.Achternaam &&
                    p.Tussenvoegsel == voertuig.Bestuurder.Tussenvoegsel &&
                    p.Voornaam == voertuig.Bestuurder.Voornaam &&
                    p.Telefoonnummer == voertuig.Bestuurder.Telefoonnummer).ToList();
                if (filteredPersonen.Count == 1)
                {
                    voertuig.Bestuurder.ID = filteredPersonen.First().ID;
                }

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
