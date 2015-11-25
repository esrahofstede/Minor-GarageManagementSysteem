namespace Minor.Case2.PcSOnderhoud.Agent
{
    public interface IAgentBSVoertuigEnKlantBeheer
    {
        /// <summary>
        /// Verstuurt een nieuw voertuig met een klant naar de BS
        /// Als de klant al bestaat wordt niet een nieuwe klant aangemaakt, maar een ref gelegd.
        /// De klant en bestuurder moeten allebei ingevuld zijn
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="voertuig">Een voertuig met daarin een referentie naar een bestuurder en een eigenaar</param>
        void VoegVoertuigMetKlantToe(BSVoertuigenEnKlantBeheer.V1.Schema.Voertuig voertuig);

        /// <summary>
        /// Verstuurt een nieuwe onderhoudsopdracht naar de BS
        /// Het voertuig moet ingevuld zijn
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="onderhoudsopdracht">Een onderhoudsopdracht met daarin een referentie naar een voertuig</param>
        void VoegOnderhoudsopdrachtToe(BSVoertuigenEnKlantBeheer.V1.Schema.Onderhoudsopdracht onderhoudsopdracht);

        /// <summary>
        /// Deze methode voegt onderhoudswerkzeemheden toe aan een onderhoudsopdracht
        /// De werkzaamheden worden verstuurt naar de BS
        /// Het opgeven van een onderhoudsopdracht ID is verplicht
        /// TechnicalExceptions worden gelogd
        /// Functionele fouten worden doorgegooid
        /// </summary>
        /// <param name="onderhoudswerkzaamheden">De onderhoudswerkzaamheden die naar de BS verstuurd worden</param>
        void VoegOnderhoudswerkzaamhedenToe(BSVoertuigenEnKlantBeheer.V1.Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden);

        /// <summary>
        /// Deze methode stuurt een voertuig naar de BS, zodat deze daar geupdate wordt.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="voertuig"></param>
        void UpdateVoertuig(BSVoertuigenEnKlantBeheer.V1.Schema.Voertuig voertuig);

        /// <summary>
        /// Deze methode haalt alle voertuigen op die voldoen aan de ingevulde criteria uit de BS
        /// Als geen voertuigen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="criteria">De criteria waaraan de voertuigen moeten voldoen die opgehaald worden</param>
        /// <returns>Alle voertuigen die voldoen aan de criteria, lege lijst als geen voertuigen gevonden zijn</returns>
        BSVoertuigenEnKlantBeheer.V1.Schema.VoertuigenCollection GetVoertuigBy(BSVoertuigenEnKlantBeheer.V1.Schema.VoertuigenSearchCriteria criteria);

        /// <summary>
        /// Deze methode haalt alle onderhoudsopdrachten op die voeldoen aan de ingevulde criteria uit de BS
        /// Als geen onderhoudsopdrachten gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <param name="criteria">De criteria waaraan de onderhoudsopdrachten moeten voldoen die opgehaald worden</param>
        /// <returns>Alle onderhoudsopdrachten die voldoen aan de criteria, lege lijst als geen onderhoudsopdrachten gevonden zijn</returns>
        BSVoertuigenEnKlantBeheer.V1.Schema.OnderhoudsopdrachtenCollection GetOnderhoudsopdrachtenBy(BSVoertuigenEnKlantBeheer.V1.Schema.OnderhoudsopdrachtZoekCriteria criteria);

        /// <summary>
        /// Deze methode haalt alle leasemaatschappijen op die in de BS te vinden zijn
        /// Als geen leasemaatschappijen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <returns>Alle leasemaatschappijen die in de BS te vinden zijn, lege lijst als geen leasemaatschappijen gevonden zijn</returns>
        BSVoertuigenEnKlantBeheer.V1.Schema.KlantenCollection GetAllLeasemaatschappijen();

        /// <summary>
        /// Deze methode haalt alle personen op die in de BS te vinden zijn
        /// Als geen personen gevonden worden dan wordt een lege collection teruggeven.
        /// TechnicalExceptions worden gelogd en doorgegooid
        /// FunctionalExceptions worden doorgegooid
        /// </summary>
        /// <returns>Alle personen die in de BS te vinden zijn, lege lijst als geen personen gevonden zijn</returns>
        BSVoertuigenEnKlantBeheer.V1.Schema.KlantenCollection GetAllPersonen();
    }
}