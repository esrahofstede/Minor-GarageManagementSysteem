using log4net;
using Minor.Case2.Exceptions.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System;
using System.Linq;
using System.ServiceModel;
using Minor.Case2.PcSOnderhoud.Agent.Exceptions;
using Minor.Case2.PcSOnderhoud.Agent.Validators;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    /// <summary>
    /// Deze agent is verantwoordelijk voor de communicatie met de BSVoertuigEnKlantBeheer
    /// </summary>
    public class AgentBSVoertuigEnKlantBeheer : IAgentBSVoertuigEnKlantBeheer
    {
        private static ILog _logger = LogManager.GetLogger(typeof(AgentBSVoertuigEnKlantBeheer));
        private readonly ServiceFactory<IBSVoertuigEnKlantbeheer> _factory;

        /// <summary>
        /// Default constructor
        /// Sets new ServiceFactory for the BSVoertuigEnKlantBeheerService
        /// </summary>
        public AgentBSVoertuigEnKlantBeheer()
        {
            _factory = new ServiceFactory<IBSVoertuigEnKlantbeheer>("BSVoertuigEnKlantBeheer");
        }

        /// <summary>
        /// Constructor with custom factory and logger
        /// Not CLSCompliant because of the Generic ServiceFactory
        /// </summary>
        /// <param name="factory">Custom factory, must best a ServiceFactory<IBSVoertuigEnKlantbeheer></param>
        /// <param name="logger">Custom logger, must implement ILog from log4net</param>
        [CLSCompliant(false)]
        public AgentBSVoertuigEnKlantBeheer(ServiceFactory<IBSVoertuigEnKlantbeheer> factory, ILog logger)
        {
            _factory = factory;
            _logger = logger;
        }

        /// <summary>
        /// Constructor with custom factory
        /// Not CLSCompliant because of the Generic ServiceFactory
        /// </summary>
        /// <param name="factory">Custom factory, must best a ServiceFactory<IBSVoertuigEnKlantbeheer></param>
        [CLSCompliant(false)]
        public AgentBSVoertuigEnKlantBeheer(ServiceFactory<IBSVoertuigEnKlantbeheer> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Verstuurt een nieuw voertuig met een klant naar de BS
        /// Als de klant al bestaat wordt niet een nieuwe klant aangemaakt, maar een ref gelegd.
        /// De klant en bestuurder moeten allebei ingevuld zijn
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="voertuig">Een voertuig met daarin een referentie naar een bestuurder en een eigenaar</param>
        public void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig)
        {
            VoertuigValidator.Validate(voertuig);

            try
            {
                var mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                proxy.VoegVoertuigMetKlantToe(mapper.SchemaToAgentVoertuigMapper(voertuig));
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }
            

        }
        
        /// <summary>
        /// Verstuurt een nieuwe onderhoudsopdracht naar de BS
        /// Het voertuig moet ingevuld zijn
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="onderhoudsopdracht">Een onderhoudsopdracht met daarin een referentie naar een voertuig</param>
        public void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                var mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                proxy.VoegOnderhoudsopdrachtToe(mapper.SchemaToAgentOnderhoudsopdrachtMapper(onderhoudsopdracht));
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Deze methode voegt onderhoudswerkzeemheden toe aan een onderhoudsopdracht
        /// De werkzaamheden worden verstuurt naar de BS
        /// Het opgeven van een onderhoudsopdracht ID is verplicht
        /// TechnicalExceptions worden gelogd
        /// Functionele fouten worden doorgegooid
        /// </summary>
        /// <param name="onderhoudswerkzaamheden">De onderhoudswerkzaamheden die naar de BS verstuurd worden</param>
        public void VoegOnderhoudswerkzaamhedenToe(Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden)
        {
            try
            {
                var mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                proxy.VoegOnderhoudswerkzaamhedenToe(mapper.SchemaToAgentOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden));

            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Deze methode stuurt een voertuig naar de BS, zodat deze daar geupdate wordt.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="voertuig"></param>
        public void UpdateVoertuig(Schema.Voertuig voertuig)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                proxy.UpdateVoertuig(mapper.SchemaToAgentVoertuigMapper(voertuig));
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }

        }

        /// <summary>
        /// Deze methode haalt alle voertuigen op die voldoen aan de ingevulde criteria uit de BS
        /// Als geen voertuigen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="criteria">De criteria waaraan de voertuigen moeten voldoen die opgehaald worden</param>
        /// <returns>Alle voertuigen die voldoen aan de criteria, lege lijst als geen voertuigen gevonden zijn</returns>
        public Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria criteria)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                AgentSchema.VoertuigenCollection voertuigen = proxy
                    .GetVoertuigBy(mapper.SchemaToAgentVoertuigSearchCriteriaMapper(criteria));
                if (voertuigen.Count == 0)
                {
                    return new Schema.VoertuigenCollection();
                }
                var query = from voertuig in voertuigen
                    select mapper.AgentToSchemaVoertuigMapper(voertuig);
                Schema.VoertuigenCollection voertuigenCollection = new Schema.VoertuigenCollection();
                voertuigenCollection.AddRange(query);
                return voertuigenCollection;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Deze methode haalt alle onderhoudsopdrachten op die voeldoen aan de ingevulde criteria uit de BS
        /// Als geen onderhoudsopdrachten gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="criteria">De criteria waaraan de onderhoudsopdrachten moeten voldoen die opgehaald worden</param>
        /// <returns>Alle onderhoudsopdrachten die voldoen aan de criteria, lege lijst als geen onderhoudsopdrachten gevonden zijn</returns>
        public Schema.OnderhoudsopdrachtenCollection GetOnderhoudsopdrachtenBy(Schema.OnderhoudsopdrachtZoekCriteria criteria)
        {
            try
            {
                BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();
                var proxy = _factory.CreateAgent();
                var onderhoudsopdrachten = proxy
                    .GetOnderhoudsopdrachtenBy(mapper.SchemaToAgentOnderhoudsopdrachtSearchCriteriaMapper(criteria));
                if (onderhoudsopdrachten.Count == 0)
                {
                    return new Schema.OnderhoudsopdrachtenCollection();
                }
                var query = from onderhoudsopdracht in onderhoudsopdrachten
                            select mapper.AgentToSchemaOnderhoudsopdrachtMapper(onderhoudsopdracht);
                var onderhoudsopdrachtenCollection = new Schema.OnderhoudsopdrachtenCollection();
                onderhoudsopdrachtenCollection.AddRange(query);
                return onderhoudsopdrachtenCollection;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }
        }
        
        /// <summary>
        /// Deze methode haalt alle leasemaatschappijen op die in de BS te vinden zijn
        /// Als geen leasemaatschappijen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <returns>Alle leasemaatschappijen die in de BS te vinden zijn, lege lijst als geen leasemaatschappijen gevonden zijn</returns>
        public Schema.KlantenCollection GetAllLeasemaatschappijen()
        {
            var proxy = _factory.CreateAgent();
            try
            {
                var mapper = new BSKlantEnVoertuigMapper();
                var leasemaatschappijen = proxy.GetAllLeasemaatschappijen();
                if (leasemaatschappijen.Count == 0)
                {
                    return new Schema.KlantenCollection();
                }
                var query = from leasemaatschappij in leasemaatschappijen
                    select mapper.AgentToSchemaKlantMapper(leasemaatschappij);
                var leasemaatschappijenCollection = new Schema.KlantenCollection();
                leasemaatschappijenCollection.AddRange(query);
                return leasemaatschappijenCollection;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Deze methode haalt alle personen op die in de BS te vinden zijn
        /// Als geen personen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <returns>Alle personen die in de BS te vinden zijn, lege lijst als geen personen gevonden zijn</returns>
        public Schema.KlantenCollection GetAllPersonen()
        {
            var proxy = _factory.CreateAgent();
            try
            {
                var mapper = new BSKlantEnVoertuigMapper();
                var personen = proxy.GetAllPersonen();
                if (personen.Count == 0)
                {
                    return new Schema.KlantenCollection();
                }
                var query = from persoon in personen
                            select mapper.AgentToSchemaKlantMapper(persoon);
                var personenCollection = new Schema.KlantenCollection();
                personenCollection.AddRange(query);
                return personenCollection;
            }
            catch (FaultException<FunctionalErrorDetail[]> ex)
            {
                throw new FunctionalException(new FunctionalErrorList(ex.Detail));
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                throw new TechnicalException(ex.Message, ex.InnerException);
            }
        }
    }
}