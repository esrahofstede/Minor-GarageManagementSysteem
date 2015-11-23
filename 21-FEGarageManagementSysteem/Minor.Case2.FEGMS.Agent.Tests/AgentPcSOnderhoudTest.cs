using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

namespace Minor.Case2.FEGMS.Agent.Tests
{
    [TestClass]
    public class AgentPcSOnderhoudTest
    {
        [TestMethod]
        public void TestMethod1()
        { 
            //Arrange
            AgentPcSOnderhoud agent = new AgentPcSOnderhoud();
            Onderhoudsopdracht opdracht = DummyData.GetDummyOnderhoudsopdracht();
            
            //Act
            agent.AddOnderhoudsOpdrachtWithKlantAndVoertuig(opdracht);

            //Assert
        }


    }
}
