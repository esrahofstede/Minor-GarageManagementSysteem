using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.ServiceModel;
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

        public KlantenCollection GetAllKlanten()
        {
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            return null;
        }

        public VoertuigCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria)
        {
            throw new NotImplementedException();
        }

        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            throw new NotImplementedException();
        }

        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            BSVoertuigenEnKlantBeheer.V1.Schema.Agent.Klant klant = new BSVoertuigenEnKlantBeheer.V1.Schema.Agent.Persoon
            {
                Voornaam = voertuig.Bestuurder.Voornaam,
                Achternaam = voertuig.Bestuurder.Achternaam,
                Adres = voertuig.Bestuurder.Adres,
                Emailadres = voertuig.Bestuurder.Emailadres,
                Postcode = voertuig.Bestuurder.Postcode,
                Tussenvoegsel = voertuig.Bestuurder.Tussenvoegsel,
                Woonplaats = voertuig.Bestuurder.Woonplaats,
                Telefoonnummer = voertuig.Bestuurder.Telefoonnummer
            };

            BSVoertuigenEnKlantBeheer.V1.Schema.Agent.Persoon bestuurder = new BSVoertuigenEnKlantBeheer.V1.Schema.Agent.Persoon
            {
                Voornaam = voertuig.Bestuurder.Voornaam,
                Achternaam = voertuig.Bestuurder.Achternaam,
                Adres = voertuig.Bestuurder.Adres,
                Emailadres = voertuig.Bestuurder.Emailadres,
                Postcode = voertuig.Bestuurder.Postcode,
                Tussenvoegsel = voertuig.Bestuurder.Tussenvoegsel,
                Woonplaats = voertuig.Bestuurder.Woonplaats,
                Telefoonnummer = voertuig.Bestuurder.Telefoonnummer
            };

            Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent.Voertuig voertuigToSend = new BSVoertuigenEnKlantBeheer.V1.Schema.Agent.Voertuig
            {
                Kenteken = voertuig.Kenteken,
                Merk = voertuig.Merk,
                Type = voertuig.Type,
                Bestuurder = bestuurder,
                Eigenaar = klant
            };
            AgentBSKlantEnVoertuigBeheer agent = new AgentBSKlantEnVoertuigBeheer();
            agent.VoegVoertuigMetKlantToe(voertuigToSend);
            
        }
    }
}
