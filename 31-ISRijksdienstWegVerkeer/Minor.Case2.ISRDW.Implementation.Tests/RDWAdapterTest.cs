using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Minor.Case2.ISRDW.Implementation.RDWIntegration;

namespace Minor.Case2.ISRDW.Implementation.Tests
{
    [TestClass]
    public class RDWAdapterTest
    {

        /// REAL TEST TO RDW API      

        //[TestMethod]
        //public void RDWAdapterRealTest()
        //{
        //    Arrange
        //   var message = new apkKeuringsverzoekRequestMessage(); //DummyData.GetMessage();

        //    RDWAdapter adapter = new RDWAdapter();

        //    Act
        //   var resultSubmition = adapter.SubmitAPKVerzoek(message);

        //    Assert
        //    Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", resultSubmition.keuringsregistratie.correlatieId);
        //    Assert.AreEqual("BV-01-EG", resultSubmition.keuringsregistratie.kenteken);
        //    Assert.AreEqual(new DateTime(2008, 11, 19), resultSubmition.keuringsregistratie.keuringsdatum);
        //    Assert.IsNull(resultSubmition.keuringsregistratie.steekproef);
        //}

        [TestMethod]
        public void RDWAdapterMockWithSteekproefTest()
        {
            // Arrange
            var message = DummyData.GetMessage();

            var response = "<?xml version=\"1.0\" encoding=\"utf-8\"?><apkKeuringsverzoekResponseMessage xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><keuringsregistratie correlatieId=\"0038c17b-aa10-4f93-8569-d184fdfc265b\" xmlns=\"http://www.rdw.nl\" xmlns:apk=\"http://www.rdw.nl/apk\"><kenteken>BV-01-EG</kenteken><apk:keuringsdatum>2008-11-19</apk:keuringsdatum><apk:steekproef xsi:nil=\"true\"/></keuringsregistratie></apkKeuringsverzoekResponseMessage>";

            var mock = new Mock<IRDWService>(MockBehavior.Strict);
            mock.Setup(rdwService => rdwService.SubmitAPKVerzoek(It.IsAny<string>())).Returns(response);

            RDWAdapter adapter = new RDWAdapter(mock.Object);

            // Act
            var resultSubmition = adapter.SubmitAPKVerzoek(message);

            // Assert 
            mock.Verify(rdwAdapter => rdwAdapter.SubmitAPKVerzoek(It.IsAny<string>()));

            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", resultSubmition.keuringsregistratie.correlatieId);
            Assert.AreEqual("BV-01-EG", resultSubmition.keuringsregistratie.kenteken);
            Assert.AreEqual(new DateTime(2008, 11, 19), resultSubmition.keuringsregistratie.keuringsdatum);
            Assert.IsNull(resultSubmition.keuringsregistratie.steekproef);
        }

        [TestMethod]
        public void RDWAdapterMockWithoutSteekproefTest()
        {
            // Arrange
            var message = DummyData.GetMessage();

            var response = "<?xml version=\"1.0\" encoding=\"utf-8\"?><apkKeuringsverzoekResponseMessage xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><keuringsregistratie correlatieId=\"0038c17b-aa10-4f93-8569-d184fdfc265b\" xmlns=\"http://www.rdw.nl\" xmlns:apk=\"http://www.rdw.nl/apk\"><kenteken>BV-01-EG</kenteken><apk:keuringsdatum>2008-11-19</apk:keuringsdatum><apk:steekproef>2008-11-19</apk:steekproef></keuringsregistratie></apkKeuringsverzoekResponseMessage>";

            var mock = new Mock<IRDWService>(MockBehavior.Strict);
            mock.Setup(rdwService => rdwService.SubmitAPKVerzoek(It.IsAny<string>())).Returns(response);

            RDWAdapter adapter = new RDWAdapter(mock.Object);

            // Act
            var resultSubmition = adapter.SubmitAPKVerzoek(message);

            // Assert 
            mock.Verify(rdwAdapter => rdwAdapter.SubmitAPKVerzoek(It.IsAny<string>()));

            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", resultSubmition.keuringsregistratie.correlatieId);
            Assert.AreEqual("BV-01-EG", resultSubmition.keuringsregistratie.kenteken);
            Assert.AreEqual(new DateTime(2008, 11, 19), resultSubmition.keuringsregistratie.keuringsdatum);
            Assert.IsNotNull(resultSubmition.keuringsregistratie.steekproef);
            Assert.AreEqual(new DateTime(2008, 11, 19), resultSubmition.keuringsregistratie.steekproef);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RDWAdapterNullTest()
        {
            // Arrange
            apkKeuringsverzoekRequestMessage message = null;

            RDWAdapter adapter = new RDWAdapter();

            // Act
            var resultSubmition = adapter.SubmitAPKVerzoek(message);

            // Assert ArgumentNullException
        }

        [TestMethod]
        public void UtilSerializeToXMLTest()
        {
            // Arrange
            var message = DummyData.GetMessage();

            // Act
            var result = Utility.SerializeToXML(message);

            // Assert
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<apkKeuringsverzoekRequestMessage xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <keuringsverzoek correlatieId=\"0038c17b-aa10-4f93-8569-d184fdfc265b\" xmlns=\"http://www.rdw.nl\">\r\n    <voertuig>\r\n      <kenteken>BV-01-EG</kenteken>\r\n      <kilometerstand>12345</kilometerstand>\r\n      <naam>A. Eigenaar</naam>\r\n    </voertuig>\r\n    <keuringsdatum xmlns=\"http://www.rdw.nl/apk\">2008-11-19</keuringsdatum>\r\n    <keuringsinstantie type=\"garage\" kvk=\"3013 5370\" xmlns=\"http://www.rdw.nl/apk\">\r\n      <naam>Garage Voorbeeld B.V.</naam>\r\n      <plaats>Wijk bij Voorbeeld</plaats>\r\n    </keuringsinstantie>\r\n  </keuringsverzoek>\r\n</apkKeuringsverzoekRequestMessage>";
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilSerializeToXMLNullTest()
        {
            // Arrange
            apkKeuringsverzoekRequestMessage message = null;

            // Act
            var result = Utility.SerializeToXML(message);

            // Assert ArgumentNullException
        }

        [TestMethod]
        public void UtilDeserializeToXMLTest()
        {
            // Arrange
            var message = "<?xml version=\"1.0\" encoding=\"utf-8\"?><apkKeuringsverzoekResponseMessage xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><keuringsregistratie correlatieId=\"0038c17b-aa10-4f93-8569-d184fdfc265b\" xmlns=\"http://www.rdw.nl\" xmlns:apk=\"http://www.rdw.nl/apk\"><kenteken>BV-01-EG</kenteken><apk:keuringsdatum>2008-11-19</apk:keuringsdatum><apk:steekproef xsi:nil=\"true\"/></keuringsregistratie></apkKeuringsverzoekResponseMessage>";

            // Act
            var result = Utility.DeserializeFromXML<apkKeuringsverzoekResponseMessage>(message);

            // Assert
            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", result.keuringsregistratie.correlatieId);
            Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", result.keuringsregistratie.correlatieId);
            Assert.AreEqual("BV-01-EG", result.keuringsregistratie.kenteken);
            Assert.AreEqual(new DateTime(2008, 11, 19), result.keuringsregistratie.keuringsdatum);
            Assert.IsNull(result.keuringsregistratie.steekproef);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilDeserializeToXMLNullTest()
        {
            // Arrange
            string message = string.Empty;

            // Act
            var result = Utility.DeserializeFromXML<apkKeuringsverzoekResponseMessage>(message);

            // Assert ArgumentNullException
        }
        

    }
}
