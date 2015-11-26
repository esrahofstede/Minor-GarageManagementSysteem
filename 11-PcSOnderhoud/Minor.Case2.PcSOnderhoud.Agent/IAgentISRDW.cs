namespace Minor.Case2.PcSOnderhoud.Agent
{
    public interface IAgentISRDW
    {
        ISRijksdienstWegverkeerService.V1.Messages.Agent.SendRdwKeuringsverzoekResponseMessage SendAPKKeuringsverzoek(BSVoertuigenEnKlantBeheer.V1.Schema.Voertuig voertuig, ISRijksdienstWegverkeerService.V1.Schema.Agent.Garage garage, ISRijksdienstWegverkeerService.V1.Schema.Agent.Keuringsverzoek keuringsverzoek);
    }
}