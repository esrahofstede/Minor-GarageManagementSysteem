using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Minor.ServiceBus.Agent.Implementation
{
    public class ServiceFactory<TContract>
    {
        private string _serviceName;

        private Dictionary<Uri, ChannelFactory<TContract>> _cache =
            new Dictionary<Uri, ChannelFactory<TContract>>();

        public ServiceLocatorConfigSection _config;

        public IServiceLocator ServiceLocator { get; set; }

        public ServiceFactory(string name) : this(name, ConfigurationManager.GetSection("serviceBus/serviceLocators") as ServiceLocatorConfigSection){ }

        public ServiceFactory(string name, ServiceLocatorConfigSection config)
        {
            _config = config;
            _serviceName = name;

            if (_config.Active == "fileServiceLocator")
            {
                ServiceLocator = new FileServiceLocator(_config.FileServiceLocator.FilePath);
            }
            else if (_config.Active == "webServiceLocator")
            {
                ServiceLocator = new WebServiceLocator<TContract>(_config.WebServiceLocator.Address, _config.WebServiceLocator.Binding);
            }
            else
            {
                throw new UnknownServiceLocatorException("No implementation for ServiceLocator: " + _config.Active);
            }
        }

        public TContract CreateAgent()
        {
            var mexAddress = ServiceLocator.FindMetadataEndpointAddress(_serviceName, _config.Profile);
            var mexUri = new Uri(mexAddress);

            ChannelFactory<TContract> factory = BuildFromUriOrGetFromCache(mexUri);

            TContract proxy = factory.CreateChannel();
            return proxy;
        }

        private ChannelFactory<TContract> BuildFromUriOrGetFromCache(Uri adress)
        {
            ChannelFactory<TContract> factory;
            if (!_cache.ContainsKey(adress))   // if not in cache
            {
                factory = BuildFactory(adress);// get it, and
                _cache.Add(adress, factory);   // add it to the cache
            }
            else                                // else
            {
                factory = _cache[adress];      // get it from the cache
            }
            return factory;
        }

        private ChannelFactory<TContract> BuildFactory(Uri adress)
        {
            var mexClient = new MetadataExchangeClient(adress, MetadataExchangeClientMode.MetadataExchange);
            MetadataSet metadataset = mexClient.GetMetadata();
            var importer = new WsdlImporter(metadataset);
            var endpoints = importer.ImportAllEndpoints();
            ServiceEndpoint endpoint = endpoints.First();

            var factory = new ChannelFactory<TContract>(endpoint.Binding, endpoint.Address);
            return factory;
        }
    }
}
