using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System.Collections.Generic;

namespace Minor.Case2.FEGMS.Agent
{
    public interface IAgentPcSOnderhoud
    {
        void AddOnderhoudsopdrachtWithKlantAndVoertuig(Onderhoudsopdracht opdracht);
        void VoegVoertuigMetKlantToe(Voertuig voertuig);
        VoertuigenCollection GetVoertuigBy(VoertuigenSearchCriteria critera);
        bool MeldVoertuigKlaar(Voertuig voertuig);
        KlantenCollection GetAllLeasemaatschappijen();
        Onderhoudsopdracht GetOnderhoudsopdrachtBy(OnderhoudsopdrachtZoekCriteria criteria);
        bool? VoegOnderhoudswerkzaamhedenToe(Onderhoudswerkzaamheden werkzaamheden);
        VoertuigenCollection HaalVoertuigenOpVoor(Persoon persoon);
    }
}