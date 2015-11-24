using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.PcSOnderhoud.Contract
{
    [ServiceContract(Namespace = "urn:minor:case2:pcsonderhoud:v1")]
    public interface IPcSOnderhoudService
    {
        [OperationContract]
        Schema.KlantenCollection GetAllKlanten();

        [OperationContract]
        Schema.VoertuigenCollection GetVoertuigBy(Schema.VoertuigenSearchCriteria zoekCriteria);

        [OperationContract]
        void VoegVoertuigMetKlantToe(Schema.Voertuig voertuig);

        [OperationContract]
        void VoegOnderhoudsopdrachtToe(Schema.Onderhoudsopdracht onderhoudsopdracht);

        [OperationContract]
        bool MeldVoertuigKlaar(Schema.Voertuig voertuig);
    }
    
}
