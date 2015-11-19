using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.ServiceBus.PfSLocatorService.DAL.Exceptions;

namespace Minor.ServiceBus.PfSLocatorService.DAL.Test
{
    [TestClass]
    public class ServiceLocationXMLDataMapperTest
    {
        [TestMethod]
        public void FileServiceLocator_Bestaat()
        {
            //Arrange
            var serviceLocator = new ServiceLocationXMLDataMapper("../../locationData.xml");

            //Assert
            Assert.IsInstanceOfType(serviceLocator, typeof(ServiceLocationXMLDataMapper));
        }

        [TestMethod]
        public void GetMexAdressMetNaamEnProfiel()
        {
            //Arrange
            var fileServiceLocator = new ServiceLocationXMLDataMapper("../../locationData.xml");

            //Act
            var adress = fileServiceLocator.FindMetadataEndpointAddress("BSCursusadministatie", "Production");

            //Assert
            Assert.AreEqual("http://infosupport.intranet/CAS", adress);
        }

        [TestMethod]
        public void GetMexAdressMetNaamEnProfielEnVersion()
        {
            //Arrange
            var fileServiceLocator = new ServiceLocationXMLDataMapper("../../locationData.xml");

            //Act
            var adress = fileServiceLocator.FindMetadataEndpointAddress("PcSPlanningmaken", "Acceptation", 1.0m);

            //Assert
            Assert.AreEqual("http://infosupport.test/CAS", adress);
        }

        [TestMethod]
        [ExpectedException(typeof(FilePathNotDefinedException))]
        public void PathfileIsEmptyString_En_Verwacht_FilePathNotDefinedException()
        {
            //Arrange
            var fileServiceLocator = new ServiceLocationXMLDataMapper("");

            //Act
            var adress = fileServiceLocator.FindMetadataEndpointAddress("BSCurusadministatie", "Production");

            //Assert
            Assert.AreEqual("http://infosupport.intranet/CAS/mex", adress);
        }
    }
}
