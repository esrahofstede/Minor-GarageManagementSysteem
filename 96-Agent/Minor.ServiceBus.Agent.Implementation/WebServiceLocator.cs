using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using minor.servicebus.pfslocatorservice.schema;
using System.ServiceModel.Description;

namespace Minor.ServiceBus.Agent.Implementation
{
    public class WebServiceLocator<TContract> : IServiceLocator
    {
        private Uri _adress;
        private Binding _binding;

        public WebServiceLocator(string adress, string binding)
        {
            _adress = new Uri(adress);
            if(binding == "basicHttpBinding")
            {
                _binding = new BasicHttpBinding();
            } else
            {
                throw new Exception("No implementation found for binding: " + binding);
            }    
        }

        public string FindMetadataEndpointAddress(string name, string profile)
        {
            return GetMetaDataEndpointAddress(name, profile);
        }

        public string FindMetadataEndpointAddress(string name, string profile, decimal version)
        {
            return GetMetaDataEndpointAddress(name, profile, version);
        }

        public string GetMetaDataEndpointAddress(string name, string profile, decimal? version = null)
        {
            var endpointAdress = new EndpointAddress(_adress);
            var factory = new ChannelFactory<IServiceLocatorService>(_binding, endpointAdress);
            IServiceLocatorService proxy = factory.CreateChannel();

            var serviceLoction = new ServiceLocation();
            serviceLoction.Name = name;
            serviceLoction.Profile = profile;
            serviceLoction.Version = version;

            return proxy.FindMetadataEndpointAddress(serviceLoction);
        }
    }
}
