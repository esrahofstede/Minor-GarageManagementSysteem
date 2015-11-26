using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.ServiceBus.Agent.Implementation;
using System.ServiceModel;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema;
using System.Configuration;

namespace Minor.Case2.FEGMS.Agent
{
    /// <summary>
    /// Connection to PcSOnderhoud
    /// </summary>
    public class AgentPcSOnderhoud : IAgentPcSOnderhoud
    {
        private ServiceFactory<IPcSOnderhoudService> _factory;

        public AgentPcSOnderhoud()
        {
            _factory = new ServiceFactory<IPcSOnderhoudService>("PcSOnderhoud");
        }

        /// <summary>
        /// Creates an instance of the AgentPcSOnderhoud and can be injected with an ServiceFactory<IPcSOnderhoudService>
        /// </summary>
        /// <param name="factory">Injectable ServiceFactory<IPcSOnderhoudService></param>
        public AgentPcSOnderhoud(ServiceFactory<IPcSOnderhoudService> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Submit an onderhoudsopdracht to the PcSOnderhoud via a proxy
        /// </summary>
        /// <param name="opdracht">The Onderhoudsopdracht</param>
        public void AddOnderhoudsopdrachtWithKlantAndVoertuig(Onderhoudsopdracht opdracht)
        {
            try
            {
                var proxy = _factory.CreateAgent();
                proxy.VoegOnderhoudsopdrachtToe(opdracht);
            }
            catch (FaultException ex)
            {

            }

        }

        /// <summary>
        /// Submit a voertuig and klant to the PcSOnderhoud via a proxy
        /// </summary>
        /// <param name="voertuig">Voertuig and Klant</param>
        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            try
            {
                var proxy = _factory.CreateAgent();
                proxy.VoegVoertuigMetKlantToe(voertuig);
            }
            catch (FaultException ex)
            {

            }
        }

        /// <summary>
        /// Get all voertuigen by criteria from the PcSOnderhoud via a proxy
        /// </summary>
        /// <param name="critera">The VoertuigenSearchCriteria</param>
        /// <returns>All voertuigen that match the criteria</returns>
        public VoertuigenCollection GetVoertuigBy(VoertuigenSearchCriteria critera)
        {
            try
            {
                var proxy = _factory.CreateAgent();
                return proxy.GetVoertuigBy(critera);
            }
            catch (FaultException ex)
            {

            }
            return null;
        }

        public KlantenCollection GetAllLeasemaatschappijen()
        {
            try
            {
                var proxy = _factory.CreateAgent();
                return proxy.GetAllLeasemaatschappijen();
            }
            catch (FaultException ex)
            {

            }
            return null;
        }

        public Onderhoudsopdracht GetOnderhoudsopdrachtBy(OnderhoudsopdrachtZoekCriteria criteria)
        {
            try
            {
                var proxy = _factory.CreateAgent();
                return proxy.GetHuidigeOnderhoudsopdrachtBy(criteria);
            }
            catch (FaultException ex)
            {

            }
            return null;
        }

        public bool? VoegOnderhoudswerkzaamhedenToe(Onderhoudswerkzaamheden werkzaamheden)
        {
            try
            {
                var section = ConfigurationManager.GetSection("Keuringsinstantie/Instantie") as KeuringsinstantieConfigSection;
                var garage = new Garage
                {
                    Naam = section.Naam,
                    Plaats = section.Plaats,
                    Kvk = section.KVK,
                    Type = section.TypeInstantie,
                };

                var proxy = _factory.CreateAgent();
                return proxy.VoegOnderhoudswerkzaamhedenToe(werkzaamheden, garage);
            }
            catch (FaultException ex)
            {

            }
            return false;
        }

        public VoertuigenCollection HaalVoertuigenOpVoor(Persoon persoon)
        {
            try
            {
                var proxy = _factory.CreateAgent();
                return proxy.HaalVoertuigenOpVoor(persoon);
            }
            catch (FaultException ex)
            {

            }
            return null;
        }

    }
}
