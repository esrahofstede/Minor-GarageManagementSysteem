using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;
using Minor.Case2.All.V1.Schema;

namespace Minor.Case2.ISRDW.Contract
{

    [ServiceContract(Namespace = "urn:case2:isrijksdienstwegverkeer:v1" )]
    public interface IISRDWService
    {
        [OperationContract]
        [FaultContract(typeof(FunctionalErrorDetail[]))]
        SendRdwKeuringsverzoekResponseMessage RequestKeuringsverzoek(SendRdwKeuringsverzoekRequestMessage message);
    }

}
