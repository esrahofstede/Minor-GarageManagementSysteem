using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minor.Case2.ISRDW.Implementation.Tests
{
    [TestClass]
    public class RDWAdapterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string urlRDW = @"http://rdwserver:18423/rdw/APKKeuringsverzoek";
            RDWAdapter adapter = new RDWAdapter();

            string message = adapter.CreateMessage();


            //Act


            var result = adapter.SubmitAPKVerzoek(urlRDW, message);


            //Assert
        }
    }
}
