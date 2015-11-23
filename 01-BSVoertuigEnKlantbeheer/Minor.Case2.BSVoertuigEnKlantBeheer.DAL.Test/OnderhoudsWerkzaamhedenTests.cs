using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    [TestClass]
    public class OnderhoudswerkzaamhedenTests
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
        public void GetOnderhoudsWerkzaamhedenFromDatabaseTest()
        {
            // Arrange
            var target = new OnderhoudsWerkzaamhedenDataMapper();

            // Act
            IEnumerable<Onderhoudswerkzaamheden> result = target.FindAll();

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
                IEnumerable<Onderhoudswerkzaamheden> result = target.FindAll();

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
            IEnumerable<Onderhoudswerkzaamheden> result = target.FindAll();

            // Assert
            Assert.IsNotNull(result.First().Onderhoudsopdracht);
        }
    }
}