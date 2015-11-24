using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.FEGMS.Agent
{
    public interface IAgentPcSOnderhoud
    {
        void AddOnderhoudsOpdrachtWithKlantAndVoertuig(Onderhoudsopdracht opdracht);
        void VoegVoertuigMetKlantToe(Voertuig voertuig);
        VoertuigenCollection GetVoertuigBy(VoertuigenSearchCriteria critera);
        void MeldVoertuigKlaar(Voertuig voertuig);
    }
}