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
    public class ServiceLocatorServiceWithVersionTest
    {
        [TestMethod]
        public void FindMetadataEndpointAddress_WithVersion_FoundSingle()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            decimal? version = 1.0m;
            string expected = "http://localhost:30412/BSKlantbeheer/mex";
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile,
                Version = version
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile, version)).Returns(expected);
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            mock.Verify(m => m.FindMetadataEndpointAddress(name, profile, version), Times.Once);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FindMetadataEndpointAddress_WithVersion_FoundSingle_ExcMessage()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            decimal? version = 1.0m;
            string expected = "http://localhost:30412/BSKlantbeheer/mex";
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile,
                Version = version
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile, version)).Returns(expected);
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            mock.Verify(m => m.FindMetadataEndpointAddress(name, profile, version), Times.Once);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void FindMetadataEndpointAddress_WithVersion_FoundMultiple()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            decimal? version = 1.0m;
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile,
                Version = version
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile, version))
                .Throws(new MultipleRecordsFoundException("Multiple location services found instead of one"));
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            //Exception thrown
        }

        [TestMethod]
        [ExpectedException(typeof(FunctionalException))]
        public void FindMetadataEndpointAddress_WithVersion_FoundNone()
        {
            // Arrange
            string name = "BSKlantBeheer";
            string profile = "Development";
            decimal? version = 1.0m;
            ServiceLocation serviceLocation = new ServiceLocation
            {
                Name = name,
                Profile = profile,
                Version = version
            };

            var mock = new Mock<IServiceLocationDataMapper>(MockBehavior.Strict);
            mock.Setup(m => m.FindMetadataEndpointAddress(name, profile, version))
                .Throws(new NoRecordsFoundException("No location services found"));
            var target = new ServiceLocatorService(mock.Object);

            // Act
            var result = target.FindMetadataEndpointAddress(serviceLocation);

            // Assert
            //Exception thrown
        }
    }
}
