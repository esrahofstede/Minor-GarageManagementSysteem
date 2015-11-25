using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case2.FEGMS.Client.Helper;
using Minor.Case2.FEGMS.Client.ViewModel;
using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;

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
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
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
            Assert.AreEqual(leasemaatschappij.Naam, ((Leasemaatschappij) result.Voertuig.Eigenaar).Naam);

            Assert.AreEqual(voertuiggegevens.Kenteken, result.Voertuig.Kenteken);
            Assert.AreEqual(voertuiggegevens.Merk, result.Voertuig.Merk);
            Assert.AreEqual(voertuiggegevens.Type, result.Voertuig.Type);

            Assert.AreEqual(onderhoudopdracht.AanmeldingsDatum, result.Aanmeldingsdatum);
            Assert.AreEqual(onderhoudopdracht.APK, result.APK);
            Assert.AreEqual(onderhoudopdracht.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudopdracht.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
        }




        [TestMethod]
        public void MapToVoertuigWithLeaseTest()
        {
            // Arrange
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(true);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();

            // Act
            var result = Mapper.MapToVoertuig(leasemaatschappij, klantgegevens, voertuiggegevens);

            // Assert
            Assert.AreEqual(klantgegevens.Voornaam, result.Bestuurder.Voornaam);
            Assert.AreEqual(klantgegevens.Tussenvoegsel, result.Bestuurder.Tussenvoegsel);
            Assert.AreEqual(klantgegevens.Achternaam, result.Bestuurder.Achternaam);
            Assert.AreEqual(klantgegevens.Adres, result.Bestuurder.Adres);
            Assert.AreEqual(klantgegevens.Postcode, result.Bestuurder.Postcode);
            Assert.AreEqual(klantgegevens.Woonplaats, result.Bestuurder.Woonplaats);
            Assert.AreEqual(klantgegevens.Emailadres, result.Bestuurder.Emailadres);
            Assert.AreEqual(klantgegevens.Telefoonnummer, result.Bestuurder.Telefoonnummer);

            Assert.AreEqual(leasemaatschappij.SelectedLeasemaatschappijID, result.Eigenaar.ID);
            Assert.AreEqual(leasemaatschappij.Telefoonnummer, result.Eigenaar.Telefoonnummer);
            Assert.AreEqual(leasemaatschappij.Naam, ((Leasemaatschappij)result.Eigenaar).Naam);

            Assert.AreEqual(voertuiggegevens.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuiggegevens.Merk, result.Merk);
            Assert.AreEqual(voertuiggegevens.Type, result.Type);
        }

        [TestMethod]
        public void MapToVoertuigWithoutLeaseTest()
        {
            // Arrange
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(false);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();

            // Act
            var result = Mapper.MapToVoertuig(leasemaatschappij, klantgegevens, voertuiggegevens);

            // Assert
            Assert.AreEqual(klantgegevens.Voornaam, result.Bestuurder.Voornaam);
            Assert.AreEqual(klantgegevens.Tussenvoegsel, result.Bestuurder.Tussenvoegsel);
            Assert.AreEqual(klantgegevens.Achternaam, result.Bestuurder.Achternaam);
            Assert.AreEqual(klantgegevens.Adres, result.Bestuurder.Adres);
            Assert.AreEqual(klantgegevens.Postcode, result.Bestuurder.Postcode);
            Assert.AreEqual(klantgegevens.Woonplaats, result.Bestuurder.Woonplaats);
            Assert.AreEqual(klantgegevens.Emailadres, result.Bestuurder.Emailadres);
            Assert.AreEqual(klantgegevens.Telefoonnummer, result.Bestuurder.Telefoonnummer);

            Assert.IsInstanceOfType(result.Bestuurder, typeof(Persoon));
            var eigenaar = result.Eigenaar as Persoon;
            Assert.AreEqual(klantgegevens.Voornaam, eigenaar.Voornaam);
            Assert.AreEqual(klantgegevens.Tussenvoegsel, eigenaar.Tussenvoegsel);
            Assert.AreEqual(klantgegevens.Achternaam, eigenaar.Achternaam);
            Assert.AreEqual(klantgegevens.Adres, eigenaar.Adres);
            Assert.AreEqual(klantgegevens.Postcode, eigenaar.Postcode);
            Assert.AreEqual(klantgegevens.Woonplaats, eigenaar.Woonplaats);
            Assert.AreEqual(klantgegevens.Emailadres, eigenaar.Emailadres);
            Assert.AreEqual(klantgegevens.Telefoonnummer, eigenaar.Telefoonnummer);

            Assert.AreEqual(voertuiggegevens.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuiggegevens.Merk, result.Merk);
            Assert.AreEqual(voertuiggegevens.Type, result.Type);
        }

        [TestMethod]
        public void MapToVoertuigKlantgegevensNullTest()
        {
            // Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            InsertKlantgegevensVM klantgegevens = null;
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();
            
            try {
                // Act
                var result = Mapper.MapToVoertuig(leasemaatschappij, klantgegevens, voertuiggegevens);
            }
            catch(ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("Value cannot be null\r\nParameter name: klantgegevens", exceptionMessage);
        }

        [TestMethod]
        public void MapToVoertuigLeasemaatschappijNullAndLeaseIsTrueTest()
        {
            // Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(true);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = null;
            InsertVoertuiggegevensVM voertuiggegevens = DummyData.GetVoertuiggegevens();

            try
            {
                // Act
                var result = Mapper.MapToVoertuig(leasemaatschappij, klantgegevens, voertuiggegevens);
            }
            catch (ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("Value cannot be null\r\nParameter name: leasemaatschappijgegevens", exceptionMessage);
        }

        [TestMethod]
        public void MapToVoertuigVoertuiggegevensNullTest()
        {
            // Arrange
            bool exceptionWasThrown = false;
            string exceptionMessage = string.Empty;
            InsertKlantgegevensVM klantgegevens = DummyData.GetKlantGegevens(false);
            InsertLeasemaatschappijGegevensVM leasemaatschappij = DummyData.GetLeasemaatschappijGegevens(false);
            InsertVoertuiggegevensVM voertuiggegevens = null;

            try
            {
                // Act
                var result = Mapper.MapToVoertuig(leasemaatschappij, klantgegevens, voertuiggegevens);
            }
            catch (ArgumentNullException ex)
            {
                exceptionWasThrown = true;
                exceptionMessage = ex.Message;
            }

            Assert.IsTrue(exceptionWasThrown);
            Assert.AreEqual("Value cannot be null\r\nParameter name: voertuiggegevens", exceptionMessage);
        }
    }
}
