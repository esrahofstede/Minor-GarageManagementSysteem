using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;

namespace Minor.Case2.ISRDW.Contract
{

    [ServiceContract(Namespace ="urn:case2:isrijksdienstwegverkeer:service")]
    public interface IISRDWService
    {

        [OperationContract]
        SendRdwKeuringsverzoekResponseMessage RequestKeuringsverzoek(SendRdwKeuringsverzoekRequestMessage message);
    }

}
