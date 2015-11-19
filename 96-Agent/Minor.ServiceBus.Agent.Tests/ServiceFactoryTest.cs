using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.ServiceBus.Agent.Implementation;
using Moq;
using System.Configuration;

namespace Minor.ServiceBus.Agent.Tests
{
    /// <summary>
    /// Summary description for ServiceFactoryTest
    /// </summary>
    [TestClass]
    public class ServiceFactoryTest
    {
        [TestMethod]
        public void ServiceFactory_Bestaat()
        {
            //Arrange
            var serviceFactory = new ServiceFactory<IKlant>("BSBeheerKlant");

            //Assert
            Assert.IsInstanceOfType(serviceFactory, typeof(ServiceFactory<IKlant>));
        }

        [TestMethod]
        public void ServiceFactory_HaaltConfigSectionOp()
        {
            //Arrange
            var serviceFactory = new ServiceFactory<IKlant>("BSBeheerKlant");

            //Assert
            Assert.AreEqual(serviceFactory._config.Active, "fileServiceLocator");
        }

        [TestMethod]
        public void ServiceFactory_GebruiktFileServiceLocator()
        {
            //Arrange
            var serviceFactory = new ServiceFactory<IKlant>("BSBeheerKlant");

            //Assert
            Assert.IsInstanceOfType(serviceFactory.ServiceLocator, typeof(FileServiceLocator));
        }

        [TestMethod]
        public void ServiceFactory_GebruiktWebServiceLocator()
        {
            //Arrange
            var config = new ServiceLocatorConfigSection();
            config.Active = "webServiceLocator";
            config.WebServiceLocator.Address = "http://www.test.nl";
            config.WebServiceLocator.Binding = "basicHttpBinding";

            var serviceFactory = new ServiceFactory<IKlant>("BSBeheerKlant", config);

            //Assert
            Assert.IsInstanceOfType(serviceFactory.ServiceLocator, typeof(WebServiceLocator<IKlant>));
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownServiceLocatorException))]
        public void ServiceFactory_VerwachtUnknownServiceLocatorException()
        {
            //Arrange
            var config = new ServiceLocatorConfigSection();
            config.Active = "unknownServiceLocator";
            var serviceFactory = new ServiceFactory<IKlant>("BSBeheerKlant", config);

            //Assert
            Assert.IsInstanceOfType(serviceFactory.ServiceLocator, typeof(WebServiceLocator<IKlant>));
        }


        [TestMethod]
        public void ServiceFactory()
        {
            ServiceFactory<IKlant> factory = new ServiceFactory<IKlant>("ISRDWService");
            var test = factory.CreateAgent();
        }

    }
}
