using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Collections.Generic;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System.Linq;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using System.Transactions;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    [TestClass]
    public class PersoonMapperTests
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
        public void GetPersoonFromDatabaseTest()
        {
            // Arrange
            var target = new PersoonDataMapper();

            // Act
            IEnumerable<Persoon> result = target.FindAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void AddPersoonToDatabaseTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Arrange
                var target = new PersoonDataMapper();

                // Act
                target.Insert(DummyData.GetDummyPersoon());
                IEnumerable<Persoon> result = target.FindAll();

                // Assert
                Assert.AreEqual(3, result.Count());
            }
        }
    }
}
