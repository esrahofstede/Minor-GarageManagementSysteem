using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AgentSchema = Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema.Agent;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.PcSOnderhoud.Contract
{
    [ServiceContract(Namespace = "urn:minor:case2:pcsonderhoud:v1")]
    public interface IPcSOnderhoudService
    {
        [OperationContract]
        Schema.KlantenCollection GetAllKlanten();

        Schema.KlantenCollection GetAllLeaseMaatschappijen();

        [OperationContract]
        Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria zoekCriteria);

        [OperationContract]
        void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig);

        [OperationContract]
        void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht);

        [OperationContract]
        bool MeldVoertuigKlaar(Schema.Voertuig voertuig, AgentSchema.Garage garage);

        [OperationContract]
        Schema.Onderhoudsopdracht GetHuidigGetHuidigeOnderhoudsopdrachtBy(Schema.VoertuigenSearchCriteria searchCriteria);

        [OperationContract]
        void VoegOnderhoudswerkzaamhedenToe(Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden);
    }

}
