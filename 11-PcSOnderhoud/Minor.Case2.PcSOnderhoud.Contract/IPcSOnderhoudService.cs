using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.PcSOnderhoud.Contract
{
    [ServiceContract(Namespace = "urn:minor:case2:pcsonderhoud:v1")]
    public interface IPcSOnderhoudService
    {
        [OperationContract]
        KlantenCollection GetAllKlanten();

        [OperationContract]
        VoertuigCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria);

        [OperationContract]
        void VoegOnderhoudsopdrachtToeMetVoertuigEnKlant(Onderhoudsopdracht onderhoudsopdracht);
    }
    
}
