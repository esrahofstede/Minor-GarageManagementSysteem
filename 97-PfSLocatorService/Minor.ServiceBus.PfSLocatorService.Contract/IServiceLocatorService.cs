using Minor.ServiceBus.PfSLocatorService.Contract.DTO;
using System.ServiceModel;

namespace Minor.ServiceBus.PfSLocatorService.Contract
{
    [ServiceContract(Namespace = "urn:minor:servicebus:pfslocatorservice")]
    public interface IServiceLocatorService
    {
        [OperationContract]
        [FaultContract(typeof(FunctionalErrorList))]
        string FindMetadataEndpointAddress(ServiceLocation serviceLocation);
    }
}
