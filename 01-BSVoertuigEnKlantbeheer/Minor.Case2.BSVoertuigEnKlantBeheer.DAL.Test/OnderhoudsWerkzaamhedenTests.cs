using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    [TestClass]
    public class OnderhoudsWerkzaamhedenTests
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
        public void GetOnderhoudsWerkzaamhedenFromDatabaseTest()
        {
            // Arrange
            var target = new OnderhoudsWerkzaamhedenDataMapper();

            // Act
            IEnumerable<OnderhoudsWerkzaamheden> result = target.FindAll();

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void AddOnderhoudsWerkzaamhedenToDatabaseTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Arrange
                var target = new OnderhoudsWerkzaamhedenDataMapper();

                // Act
                target.Insert(DummyData.GetDummyOnderhoudsWerkzaamheden());
                IEnumerable<OnderhoudsWerkzaamheden> result = target.FindAll();

                // Assert
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public void HasOnderhoudsWerkzaamhedenAnOnderhoudsOpdracht()
        {
            // Arrange
            var target = new OnderhoudsWerkzaamhedenDataMapper();

            // Act
            IEnumerable<OnderhoudsWerkzaamheden> result = target.FindAll();

            // Assert
            Assert.IsNotNull(result.First().OnderhoudsOpdracht);
        }
    }
}