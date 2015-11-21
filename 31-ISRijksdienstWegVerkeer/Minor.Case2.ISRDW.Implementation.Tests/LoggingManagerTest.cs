using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Minor.Case2.ISRDW.DAL;
using Minor.Case2.ISRDW.DAL.Entities;

namespace Minor.Case2.ISRDW.Implementation.Tests
{
    [TestClass]
    public class LoggingManagerTest
    {
        [TestMethod]
        public void LoggingManagerLogApkKeuringsverzoekRequestMessageTest()
        {
            //Arrange
            var mock = new Mock<IDataMapper<Logging, long>>(MockBehavior.Strict);
            mock.Setup(datamapper => datamapper.Insert(It.IsAny<Logging>()));
            mock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetAllKeuringsregistratieLoggings());


            LoggingManager manager = new LoggingManager(mock.Object);
            var requestmessage = DummyData.GetApkKeuringsverzoekRequestMessage();

            //Act
            manager.Log(requestmessage, new DateTime(2015, 11, 18, 11, 13, 00));

            //Assert
            var all = manager.FindAll();
            var lastLogItem = all.Last();

            Assert.AreEqual(new DateTime(2015, 11, 18, 11, 13, 00), lastLogItem.Time);
            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", lastLogItem.Keuringsregistratie.CorrelatieId);
            Assert.AreEqual("BV-01-EG", lastLogItem.Keuringsregistratie.Kenteken);
            Assert.AreEqual(new DateTime(2008, 11, 19), lastLogItem.Keuringsregistratie.Keuringsdatum);
            Assert.IsNull(lastLogItem.Keuringsregistratie.Steekproef);
            Assert.IsNull(lastLogItem.Keuringsverzoek);
        }

        [TestMethod]
        public void LoggingManagerLogApkKeuringsverzoekResponseMessageTest()
        {
            //Arrange
            var mock = new Mock<IDataMapper<Logging, long>>(MockBehavior.Strict);
            mock.Setup(datamapper => datamapper.Insert(It.IsAny<Logging>()));
            mock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetAllKeuringsverzoekLoggings());


            LoggingManager manager = new LoggingManager(mock.Object);
            var requestmessage = DummyData.GetApkKeuringsverzoekResponseMessage();

            //Act
            manager.Log(requestmessage, new DateTime(2015, 11, 18, 11, 13, 00));

            //Assert
            var all = manager.FindAll();
            var lastLogItem = all.Last();

            Assert.AreEqual(new DateTime(2015, 11, 18, 11, 13, 00), lastLogItem.Time);
            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", lastLogItem.Keuringsverzoek.CorrelatieId);
            Assert.AreEqual("BV-01-EG", lastLogItem.Keuringsverzoek.Kenteken);
            Assert.AreEqual(12345, lastLogItem.Keuringsverzoek.Kilometerstand);
            Assert.AreEqual("A. Eigenaar", lastLogItem.Keuringsverzoek.NaamEigenaar);
            Assert.AreEqual("personenauto", lastLogItem.Keuringsverzoek.VoertuigType);
            Assert.AreEqual(new DateTime(2008, 11, 19), lastLogItem.Keuringsverzoek.Keuringsdatum);
            Assert.AreEqual("Garage Voorbeeld B.V.", lastLogItem.Keuringsverzoek.KeuringsinstantieNaam);
            Assert.AreEqual("Wijk bij Voorbeeld", lastLogItem.Keuringsverzoek.KeuringsinstantiePlaats);
            Assert.AreEqual("garage", lastLogItem.Keuringsverzoek.KeuringsinstantieType);
            Assert.AreEqual("3013 5370", lastLogItem.Keuringsverzoek.KVK);
            Assert.IsNull(lastLogItem.Keuringsregistratie);
        }

        [TestMethod]
        public void LoggingManagerLogApkKeuringsverzoekRequestMessageNullTest()
        {
            //Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            var mock = new Mock<IDataMapper<Logging, long>>(MockBehavior.Strict);
            mock.Setup(datamapper => datamapper.Insert(It.IsAny<Logging>()));
            mock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetAllKeuringsregistratieLoggings());


            LoggingManager manager = new LoggingManager(mock.Object);
            apkKeuringsverzoekRequestMessage requestmessage = null;

            try
            {
            //Act
            manager.Log(requestmessage, new DateTime(2015, 11, 18, 11, 13, 00));
            }
            catch (ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            //Assert
            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("The request message that needs to be mapped, cannot be null\r\nParameter name: requestMessage", exceptionMessage);
        }

        [TestMethod]
        public void LoggingManagerLogApkKeuringsverzoekResponseMessageNullTest()
        {
            //Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            var mock = new Mock<IDataMapper<Logging, long>>(MockBehavior.Strict);
            mock.Setup(datamapper => datamapper.Insert(It.IsAny<Logging>()));
            mock.Setup(datamapper => datamapper.FindAll()).Returns(DummyData.GetAllKeuringsregistratieLoggings());


            LoggingManager manager = new LoggingManager(mock.Object);
            apkKeuringsverzoekResponseMessage responsemessage = null;

            try
            {
                //Act
                manager.Log(responsemessage, new DateTime(2015, 11, 18, 11, 13, 00));
            }
            catch (ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            //Assert
            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("The response message that needs to be mapped, cannot be null\r\nParameter name: responseMessage", exceptionMessage);
        }

        [TestMethod]
        public void MapperMapToLoggingRequestMessageTest()
        {
            // Arrange
            var requestMessage = DummyData.GetApkKeuringsverzoekRequestMessage();

            // Act
            var resultLog = Mapper.MapToLogging(requestMessage, new DateTime(2015, 11, 23, 13, 14, 53));
            
            // Assert
            Assert.AreEqual(new DateTime(2015, 11, 23, 13, 14, 53), resultLog.Time);
            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", resultLog.Keuringsverzoek.CorrelatieId);
            Assert.AreEqual("BV-01-EG", resultLog.Keuringsverzoek.Kenteken);
            Assert.AreEqual(12345, resultLog.Keuringsverzoek.Kilometerstand);
            Assert.AreEqual("A. Eigenaar", resultLog.Keuringsverzoek.NaamEigenaar);
            Assert.AreEqual("personenauto", resultLog.Keuringsverzoek.VoertuigType);
            Assert.AreEqual(new DateTime(2008, 11, 19), resultLog.Keuringsverzoek.Keuringsdatum);
            Assert.AreEqual("Garage Voorbeeld B.V.", resultLog.Keuringsverzoek.KeuringsinstantieNaam);
            Assert.AreEqual("Wijk bij Voorbeeld", resultLog.Keuringsverzoek.KeuringsinstantiePlaats);
            Assert.AreEqual("garage", resultLog.Keuringsverzoek.KeuringsinstantieType);
            Assert.AreEqual("3013 5370", resultLog.Keuringsverzoek.KVK);
            Assert.IsNull(resultLog.Keuringsregistratie);
        }


        [TestMethod]
        public void MapperMapToLoggingResponseMessageTest()
        {
            // Arrange
            var responseMessage = DummyData.GetApkKeuringsverzoekResponseMessage();

            // Act
            var resultLog = Mapper.MapToLogging(responseMessage, new DateTime(2015, 11, 11, 13, 14, 16));

            // Assert
            Assert.AreEqual(new DateTime(2015, 11, 11, 13, 14, 16), resultLog.Time);
            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", resultLog.Keuringsregistratie.CorrelatieId);
            Assert.AreEqual("BV-01-EG", resultLog.Keuringsregistratie.Kenteken);
            Assert.AreEqual(new DateTime(2008, 11, 19), resultLog.Keuringsregistratie.Keuringsdatum);
            Assert.IsNull(resultLog.Keuringsregistratie.Steekproef);
            Assert.IsNull(resultLog.Keuringsverzoek);
        }


        [TestMethod]
        public void MapperMapToLoggingRequestMessageNullTest()
        {
            // Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            apkKeuringsverzoekRequestMessage message = null;

            try
            {
                // Act
                Mapper.MapToLogging(message, new DateTime(2015, 11, 18));
            } catch (ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            // Assert
            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("The message that needs to be mapped, cannot be null\r\nParameter name: message", exceptionMessage);
        }

        [TestMethod]
        public void MapperMapToLoggingResponseMessageNullTest()
        {
            // Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            apkKeuringsverzoekResponseMessage message = null;

            try
            {
                // Act
                Mapper.MapToLogging(message, new DateTime(2015, 11, 18));
            }
            catch (ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            // Assert
            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("The message that needs to be mapped, cannot be null\r\nParameter name: message", exceptionMessage);
        }
    }
}
