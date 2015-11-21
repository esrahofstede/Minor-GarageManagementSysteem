using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.ISRDW.DAL.Entities;
using System.Transactions;
using System.Linq;
using System.Data.Entity;

namespace Minor.Case2.ISRDW.DAL.Tests
{
    [TestClass]
    public class LoggingDataMapperTest
    {
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            Database.SetInitializer(new RDWDatabaseInitializer());
            using (var context = new RDWContext())
            {
                context.Database.Initialize(false);
            }
        }

        [TestMethod]
        public void FindAllLogsTest()
        {
            //Arrange
            var mapper = new LoggingDataMapper();

            //Act
            var result = mapper.FindAll();

            //Assert
            var count = mapper.FindAll().Count();
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void InsertLogTest()
        {
            using (var transaction = new TransactionScope())
            {
                //Arrange
                var mapper = new LoggingDataMapper();
                var logging = new Logging
                {
                    Keuringsverzoek = DummyData.GetKeuringsverzoek(),
                    Time = new DateTime(2015, 11, 17, 11, 32, 44),
                };

                //Act
                mapper.Insert(logging);

                //Assert
                var all = mapper.FindAll();
                var count = all.Count();
                var lastInserted = all.Last();

                Assert.AreEqual(3, count);
                Assert.AreEqual("0038c17b-aa10-4f93-8569-d184fdfc265b", lastInserted.Keuringsverzoek.CorrelatieId);
                Assert.AreEqual("BV-01-EG", lastInserted.Keuringsverzoek.Kenteken);
                Assert.AreEqual(new DateTime(2008, 11, 19), lastInserted.Keuringsverzoek.Keuringsdatum);
                Assert.AreEqual("Garage Voorbeeld B.V", lastInserted.Keuringsverzoek.KeuringsinstantieNaam);
                Assert.AreEqual("Wijk bij Voorbeeld", lastInserted.Keuringsverzoek.KeuringsinstantiePlaats);
                Assert.AreEqual("garage", lastInserted.Keuringsverzoek.KeuringsinstantieType);
                Assert.AreEqual("3013 5370", lastInserted.Keuringsverzoek.KVK);
                Assert.AreEqual("A. Eigenaar", lastInserted.Keuringsverzoek.NaamEigenaar);
                Assert.AreEqual(new DateTime(2015, 11, 17, 11, 32, 44), lastInserted.Time);
            }
        }

        [TestMethod]
        public void InsertLogKeuringsverzoekAndKeuringsregistratieNullTest()
        {
            using (var transaction = new TransactionScope())
            {
                //Arrange
                bool exceptionWasThrown = false;
                string exceptionMessage = string.Empty;
                var mapper = new LoggingDataMapper();
                var logging = new Logging
                {
                    Time = new DateTime(2015, 11, 17, 11, 32, 44),
                };

                try {
                    //Act
                    mapper.Insert(logging);
                }
                catch(ArgumentException ex)
                {
                    exceptionWasThrown = true;
                    exceptionMessage = ex.Message;
                }

                //Assert
                Assert.IsTrue(exceptionWasThrown);
                Assert.AreEqual("Keuringsregistratie and Keuringsverzoek cannot both be null", exceptionMessage);
            }
        }

        [TestMethod]
        public void InsertLogWithoutDateTimeTest()
        {
            using (var transaction = new TransactionScope())
            {
                //Arrange
                bool exceptionWasThrown = false;
                string exceptionMessage = string.Empty;
                var mapper = new LoggingDataMapper();
                var logging = new Logging
                {
                    Keuringsverzoek = DummyData.GetKeuringsverzoek()
                };

                try
                {
                    //Act
                    mapper.Insert(logging);
                }
                catch (ArgumentException ex)
                {
                    exceptionWasThrown = true;
                    exceptionMessage = ex.Message;
                }

                //Assert
                Assert.IsTrue(exceptionWasThrown);
                Assert.AreEqual("Time has to be specified when inserting a log", exceptionMessage);
            }
        }

        [TestMethod]
        public void InsertLogNullTest()
        {
            using (var transaction = new TransactionScope())
            {
                //Arrange
                bool exceptionWasThrown = false;
                string exceptionMessage = string.Empty;
                var mapper = new LoggingDataMapper();
                Logging logging = null;

                try {
                    //Act
                    mapper.Insert(logging);
                }
                catch(ArgumentNullException ex)
                {
                    exceptionWasThrown = true;
                    exceptionMessage = ex.Message;
                }

                //Assert ArgumentNullException
                Assert.IsTrue(exceptionWasThrown);
                Assert.AreEqual("Logging item cannot be null\r\nParameter name: item", exceptionMessage);
            }
        }
    }
}
