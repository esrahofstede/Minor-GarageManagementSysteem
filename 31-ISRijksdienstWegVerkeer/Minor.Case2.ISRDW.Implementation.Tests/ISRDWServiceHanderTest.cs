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
            var requestMessage = DummyData.GetRequestMessage();
            ISRDWServiceHandler handler = new ISRDWServiceHandler();
            //Act
            var rdwRequestMessage = Mapper.MapToRDWRequestMessage(requestMessage);
            //Assert
            Assert.AreEqual(requestMessage.Garage.Kvk, rdwRequestMessage.keuringsverzoek.keuringsinstantie.kvk);
            Assert.AreEqual(requestMessage.Voertuig.kenteken, rdwRequestMessage.keuringsverzoek.voertuig.kenteken);
            Assert.AreEqual(requestMessage.Keuringsverzoek.CorrolatieId, rdwRequestMessage.keuringsverzoek.correlatieId);
        }

        [TestMethod]
        public void MapAPKResponseMessageToResponseMessageTest()
        {
            //Arange
            var reponseMessage = DummyData.GetReponseMessage();
            ISRDWServiceHandler handler = new ISRDWServiceHandler();
            //Act
            var rdwResponseMessage = Mapper.MapToResponseMessage(reponseMessage);
            //Assert
            Assert.AreEqual(reponseMessage.keuringsregistratie.kenteken, rdwResponseMessage.Kenteken);
            Assert.AreEqual(reponseMessage.keuringsregistratie.steekproefSpecified, rdwResponseMessage.Steekproef);
        }
    }
}
