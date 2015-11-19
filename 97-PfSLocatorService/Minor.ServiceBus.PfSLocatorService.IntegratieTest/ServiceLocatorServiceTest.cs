using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using Minor.ServiceBus.PfSLocatorService.Contract;
using Moq;
using Minor.ServiceBus.PfSLocatorService.Contract.DTO;
using Minor.ServiceBus.PfSLocatorService.Implementation;
using System.ServiceModel.Channels;

namespace Minor.ServiceBus.PfSLocatorService.IntegratieTest
{
    [TestClass]
    public class ServiceLocatorServiceTest
    {

        static private ServiceHost _host;

        [ClassInitialize()]
        public static void Initialize(TestContext context)
        {
            _host = new ServiceHost(typeof(ServiceLocatorService));

            var contract = typeof(IServiceLocatorService);
            var binding = new NetNamedPipeBinding();
            var address = "net.pipe://localhost/ServiceLocatorService";
            _host.AddServiceEndpoint(contract, binding, address);
            _host.Open();
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            _host.Close();
        }



        [TestMethod]
        public void FindMetadataEndpointAddress_WithoutVersion_Integration()
        {
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = "PcSPlanningmaken",
                Profile = "Acceptation",
                Version = 1.0M
            };
            EndpointAddress address = new EndpointAddress("net.pipe://localhost/ServiceLocatorService");
            Binding binding = new NetNamedPipeBinding();

            var factory = new ChannelFactory<IServiceLocatorService>(binding, address);

            IServiceLocatorService proxy = factory.CreateChannel();

            string uri = proxy.FindMetadataEndpointAddress(serviceLocation);

            Assert.AreEqual("http://infosupport.test/CAS", uri);
        }
    }
}
