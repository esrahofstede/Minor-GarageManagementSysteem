using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Minor.ServiceBus.PfSLocatorService.Contract.DTO;
using Minor.ServiceBus.PfSLocatorService.DAL;
using Minor.ServiceBus.PfSLocatorService.DAL.Exceptions;
using System.ServiceModel;

namespace Minor.ServiceBus.PfSLocatorService.Implementation.Test
{
    [TestClass]
    public class ServiceLocatorServiceWithoutVersionTest
    {
        [TestMethod]
        public void FindMetadataEndpointAddress_WithoutVersion_FoundSingle()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            string expected = "http://localhost:30412/BSKlantbeheer/mex";
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile)).Returns(expected);
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            mock.Verify(m => m.FindMetadataEndpointAddress(name, profile), Times.Once);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void FindMetadataEndpointAddress_WithoutVersion_FoundMultiple()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile))
                .Throws(new MultipleRecordsFoundException("Multiple location services found instead of one"));
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            //Exception thrown
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void FindMetadataEndpointAddress_WithoutVersion_FoundNone()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile))
                .Throws(new NoRecordsFoundException("No location services found"));
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            //Exception thrown
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void FindMetadataEndpointAddress_WithoutVersion_FoundVersion()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile))
                .Throws(new VersionedRecordFoundException(
                    "The location service found has a version, so specify the version"));
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            //Exception thrown
        }
    }
}
