using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Contract
{
    [ServiceContract(Namespace = "urn:minor-case2-bsvoertuigenklantbeheer:v1")]
    public interface IBSVoertuigEnKlantbeheer
    {
        [OperationContract]
        KlantenCollection GetAllKlanten();

        [OperationContract]
        KlantenCollection GetAllLeasemaatschappijen();

        [OperationContract]
        OnderhoudsopdrachtenCollection GetOnderhoudsopdrachtenBy(VoertuigenSearchCriteria zoekCriteria);

        [OperationContract]
        VoertuigenCollection GetVoertuigBy(OnderhoudsopdrachtZoekCriteria zoekCriteria);

        [OperationContract]
        void VoegVoertuigMetKlantToe(Voertuig voertuig);

        [OperationContract]
        void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht);

        [OperationContract]
        void UpdateVoertuig(Voertuig voertuig);

    }
}
