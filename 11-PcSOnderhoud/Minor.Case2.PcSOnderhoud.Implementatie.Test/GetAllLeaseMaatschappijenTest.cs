using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.PcSOnderhoud.Agent;
using Moq;

namespace Minor.Case2.PcSOnderhoud.Implementation.Tests
{
    [TestClass]
    public class GetAllLeaseMaatschappijenTest
    {
        [TestMethod]
        public void ReturnKlantenCollection()
        {
            //Arrange
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(leasemaatschappijen);

            //Act
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);

            //Assert
            Assert.AreEqual(typeof(Schema.KlantenCollection), target.GetAllLeasemaatschappijen().GetType());
        }

        [TestMethod]
        public void ReturnCorrectData()
        {
            //Arrange
            var leasemaatschappijen = new Schema.KlantenCollection();
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            leasemaatschappijen.Add(new Schema.Leasemaatschappij());
            var agentMock = new Mock<IAgentBSVoertuigEnKlantBeheer>(MockBehavior.Strict);
            agentMock.Setup(agent => agent.GetAllLeasemaatschappijen()).Returns(leasemaatschappijen);

            //Act
            var target = new PcSOnderhoudServiceHandler(agentMock.Object);
            var result = target.GetAllLeasemaatschappijen();

            //Assert
            Assert.AreEqual(3, target.GetAllLeasemaatschappijen().Count);
        }
    }
}
