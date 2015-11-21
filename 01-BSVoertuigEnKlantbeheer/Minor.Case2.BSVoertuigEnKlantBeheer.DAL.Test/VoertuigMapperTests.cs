using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class VoertuigMapperTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testcontext)
        {
            Database.SetInitializer(new DbInitializer());
            using (var context = new VoertuigEnKlantContext())
            {
                context.Database.Initialize(false);
            }
        }

        [TestMethod]
        public void GetVoertuigFromDatabaseTest()
        {
            // Arrange
            var target = new VoertuigDataMapper();

            // Act
            IEnumerable<Voertuig> result = target.FindAll();

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void AddVoertuigToDatabaseTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Arrange
                var target = new VoertuigDataMapper();

                // Act
                target.Insert(DummyData.GetDummyVoertuig());
                IEnumerable<Voertuig> result = target.FindAll();

                // Assert
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public void HasVoertuigABestuurderTest()
        {
            // Arrange
            var target = new VoertuigDataMapper();

            // Act
            IEnumerable<Voertuig> result = target.FindAll();

            // Assert
            Assert.IsNotNull(result.First().Bestuurder);
        }

        [TestMethod]
        public void HasVoertuigAnEigenaarTest()
        {
            // Arrange
            var target = new VoertuigDataMapper();

            // Act
            IEnumerable<Voertuig> result = target.FindAll();

            // Assert
            Assert.IsNotNull(result.First().Eigenaar);
        }
    }
}
