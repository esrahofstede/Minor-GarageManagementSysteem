using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    [TestClass]
    public class LeasemaatschappijMapperTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testcontext)
        {
            Database.SetInitializer(new VoertuigDbInitializer());
            using (var context = new VoertuigContext())
            {
                context.Database.Initialize(false);
            }
            Database.SetInitializer(new KlantDbInitializer());
            using (var context = new KlantContext())
            {
                context.Database.Initialize(false);
            }
        }

        [TestMethod]
        public void GetLeasemaatschappijFromDatabaseTest()
        {
            // Arrange
            var target = new LeasemaatschappijDataMapper();

            // Act
            IEnumerable<Leasemaatschappij> result = target.FindAll();

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void AddLeasemaatschappijToDatabaseTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Arrange
                var target = new LeasemaatschappijDataMapper();

                // Act
                target.Insert(DummyData.GetDummyLeasemaatschappij());
                IEnumerable<Leasemaatschappij> result = target.FindAll();

                // Assert
                Assert.AreEqual(2, result.Count());
            }
        }
    }
}
