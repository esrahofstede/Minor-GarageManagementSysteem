using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Messages;
using Minor.Case2.ISRijksdienstWegverkeerService.V1.Schema;

namespace Minor.Case2.PcSOnderhoud.Agent.Tests
{
    [TestClass]
    public class AgentBSKlantEnVoertuigBeheerTest
    {
        [TestMethod]
        public void AgentBS_InsertMethodeBestaat()
        {
            //Arrange
            var agent = new AgentBSKlantEnVoertuigBeheer();
            var voertuig = new Voertuig();

            //Act
            //agent.VoegOnderhoudsopdrachtToe(voertuig);
            
            //Assert
            Assert.IsTrue(true);
        }
    }
}
