using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Minor.Case2.FEGMS.Agent.Tests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void KeuringsinstantieConfigurationTest()
        {
            //Act
            var section = ConfigurationManager.GetSection("Keuringsinstantie/Instantie") as KeuringsinstantieConfigSection;

            //Assert
            Assert.AreEqual("garage", section.TypeInstantie);
            Assert.AreEqual("1414 2135", section.KVK);
            Assert.AreEqual("Caespi", section.Naam);
            Assert.AreEqual("Utrecht", section.Plaats);
        }
    }
}
