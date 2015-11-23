using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.FEGMS.Client.Helper;
using Minor.Case2.FEGMS.Client.ViewModel;

namespace Minor.Case2.FEGMS.Client.Tests
{
    [TestClass]
    public class MapperTest
    {

        [TestMethod]
        public void MapToOnderhoudsopdrachtWithLeaseTest()
        {
            // Arrange
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(true);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens();
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            InsertOnderhoudsopdrachtVM onderhoudopdracht = DummyData.GetOnderhoudsopdracht();

            // Act
            var result = Mapper.MapToOnderhoudsopdracht(onderhoudopdracht, leasemaatschappij, klantgegevens, voertuiggegevens);

            // Assert
            Assert.AreEqual(klantgegevens.Voornaam, result.Voertuig.Bestuurder.Voornaam);
            Assert.AreEqual(klantgegevens.Tussenvoegsel, result.Voertuig.Bestuurder.Tussenvoegsel);
            Assert.AreEqual(klantgegevens.Achternaam, result.Voertuig.Bestuurder.Achternaam);
            Assert.AreEqual(klantgegevens.Adres, result.Voertuig.Bestuurder.Adres);
            Assert.AreEqual(klantgegevens.Postcode, result.Voertuig.Bestuurder.Postcode);
            Assert.AreEqual(klantgegevens.Woonplaats, result.Voertuig.Bestuurder.Woonplaats);
            Assert.AreEqual(klantgegevens.Emailadres, result.Voertuig.Bestuurder.Emailadres);
            Assert.AreEqual(klantgegevens.Telefoonnummer, result.Voertuig.Bestuurder.Telefoonnummer);

            Assert.AreEqual(leasemaatschappij.Telefoonnummer, result.Voertuig.Eigenaar.Telefoonnummer);
           //Assert.AreEqual(leasemaatschappij.Naam, (Leasemaatschappij) result.Voertuig.Eigenaar.n);

        }
    }
}
