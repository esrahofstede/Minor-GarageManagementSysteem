﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class OnderhoudsopdrachtTests
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
        public void GetOnderhoudsOpdrachtFromDatabaseTest()
        {
            // Arrange
            var target = new OnderhoudsOpdrachtDataMapper();

            // Act
            IEnumerable<Onderhoudsopdracht> result = target.FindAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void AddOnderhoudsOpdrachtToDatabaseTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Arrange
                var target = new OnderhoudsOpdrachtDataMapper();

                // Act
                target.Insert(DummyData.GetDummyOnderhoudsOpdracht());
                IEnumerable<Onderhoudsopdracht> result = target.FindAll();

                // Assert
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void HasOnderhoudsOpdrachtAVoertuigTest()
        {
            // Arrange
            var target = new OnderhoudsOpdrachtDataMapper();

            // Act
            IEnumerable<Onderhoudsopdracht> result = target.FindAll();

            // Assert
            Assert.IsNotNull(result.First().Voertuig);
        }

        [TestMethod]
        public void HasOnderhoudsOpdrachtAnOnderhoudsWerkzaamhedenTest()
        {
            // Arrange
            var target = new OnderhoudsOpdrachtDataMapper();

            // Act
            IEnumerable<Onderhoudsopdracht> result = target.FindAll();

            // Assert
            Assert.IsNotNull(result.First().Onderhoudswerkzaamheden);
        }
    }
}