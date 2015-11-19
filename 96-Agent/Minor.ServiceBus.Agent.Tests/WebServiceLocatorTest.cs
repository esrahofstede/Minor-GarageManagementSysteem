using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.ServiceBus.Agent.Implementation;

namespace Minor.ServiceBus.Agent.Tests
{
    [TestClass]
    public class WebServiceLocatorTest
    {
        [TestMethod]
        public void WebServiceLocator_Bestaat()
        {
            //Arrange
            var webServiceLocator = new WebServiceLocator<IKlant>("http://www.test.nl", "basicHttpBinding");

            //Assert
            Assert.IsInstanceOfType(webServiceLocator, typeof(WebServiceLocator<IKlant>));
        }
    }
}
