using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minor.Case2.ISRDW.Implementation.Tests
{
    [TestClass]
    public class ISRDWServiceHanderTest
    {
        [TestMethod]
        public void MapRequestMessageToAPKRequestMessageTest()
        {
            //Arange
            var requestMessage = DummyData.GetSendRdwKeuringsverzoekRequestMessage();
            ISRDWServiceHandler handler = new ISRDWServiceHandler();
            //Act
            var rdwRequestMessage = Mapper.MapToRDWRequestMessage(requestMessage);
            //Assert
            Assert.AreEqual(requestMessage.Garage.Kvk, rdwRequestMessage.keuringsverzoek.keuringsinstantie.kvk);
            Assert.AreEqual(requestMessage.Voertuig.Kenteken, rdwRequestMessage.keuringsverzoek.voertuig.kenteken);
            Assert.AreEqual(requestMessage.Keuringsverzoek.CorrolatieId, rdwRequestMessage.keuringsverzoek.correlatieId);
        }

        [TestMethod]
        public void MapAPKResponseMessageToResponseMessageTest()
        {
            //Arange
            var reponseMessage = DummyData.GetApkKeuringsverzoekResponseMessage();
            ISRDWServiceHandler handler = new ISRDWServiceHandler();
            //Act
            var rdwResponseMessage = Mapper.MapToResponseMessage(reponseMessage);
            //Assert
            Assert.AreEqual("BV-01-EG", rdwResponseMessage.Kenteken);
            Assert.IsFalse(rdwResponseMessage.Steekproef);
        }
    }
}
