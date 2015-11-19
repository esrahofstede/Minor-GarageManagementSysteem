using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.ServiceBus.Agent.Implementation;
using minor.servicebus.pfslocatorservice.schema;

namespace Minor.ServiceBus.Agent.Tests
{
    [TestClass]
    public class FileServiceLocatorTest
    {
        [TestMethod]
        public void FileServiceLocator_Bestaat()
        {
            //Arrange
            var serviceLocator = new FileServiceLocator("../../locationData.xml");

            //Assert
            Assert.IsInstanceOfType(serviceLocator, typeof(FileServiceLocator));
        }

        [TestMethod]
        public void GetMexAdressMetNaamEnProfiel()
        {
            //Arrange
            var fileServiceLocator = new FileServiceLocator("../../locationData.xml");

            //Act
            var adress = fileServiceLocator.FindMetadataEndpointAddress("BSCurusadministatie", "Production");

            //Assert
            Assert.AreEqual("http://infosupport.intranet/CAS/mex", adress);
        }

        [TestMethod]
        public void GetMexAdressMetNaamEnProfielEnVersion()
        {
            //Arrange
            var fileServiceLocator = new FileServiceLocator("../../locationData.xml");

            //Act
            var adress = fileServiceLocator.FindMetadataEndpointAddress("PcSPlanningmaken", "Acceptation", 1.4m);

            //Assert
            Assert.AreEqual("http://infosupport.test/CAS/metadata", adress);
        }

        [TestMethod]
        [ExpectedException(typeof(FilePathNotDefinedException))]
        public void PathfileIsEmptyString_En_Verwacht_FilePathNotDefinedException()
        {
            //Arrange
            var fileServiceLocator = new FileServiceLocator("");

            //Act
            var adress = fileServiceLocator.FindMetadataEndpointAddress("BSCurusadministatie", "Production");

            //Assert
            Assert.AreEqual("http://infosupport.intranet/CAS/mex", adress);
        }
    }
}
