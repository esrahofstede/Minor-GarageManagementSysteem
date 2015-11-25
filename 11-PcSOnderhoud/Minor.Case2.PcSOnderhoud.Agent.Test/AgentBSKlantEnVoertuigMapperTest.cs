using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent.Tests
{
    [TestClass]
    public class AgentBSKlantEnVoertuigMapperTest
    {
        #region Persoon
        [TestMethod]
        public void SchemaToAgentPersoonMapperReturnsPersoon()
        {
            //Arrange
            Schema.Persoon persoon = new Schema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentPersoonMapper(persoon);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Persoon), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentPersoonMapperReturnsCorrectData()
        {
            //Arrange
            Schema.Persoon persoon = new Schema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentPersoonMapper(persoon);

            //Assert
            Assert.AreEqual(persoon.Voornaam, result.Voornaam);
            Assert.AreEqual(persoon.Achternaam, result.Achternaam);
            Assert.AreEqual(persoon.Telefoonnummer, result.Telefoonnummer);
            Assert.AreEqual(persoon.Tussenvoegsel, result.Tussenvoegsel);
            Assert.AreEqual(persoon.Adres, result.Adres);
        }

        [TestMethod]
        public void SchemaToAgentPersoonMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentPersoonMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaPersoonMapperReturnsPersoon()
        {
            //Arrange
            AgentSchema.Persoon persoon = new AgentSchema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaPersoonMapper(persoon);

            //Assert
            Assert.AreEqual(typeof(Schema.Persoon), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaPersoonMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.Persoon persoon = new AgentSchema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaPersoonMapper(persoon);

            //Assert
            Assert.AreEqual(persoon.Voornaam, result.Voornaam);
            Assert.AreEqual(persoon.Achternaam, result.Achternaam);
            Assert.AreEqual(persoon.Telefoonnummer, result.Telefoonnummer);
            Assert.AreEqual(persoon.Tussenvoegsel, result.Tussenvoegsel);
            Assert.AreEqual(persoon.Adres, result.Adres);
        }

        [TestMethod]
        public void AgentToSchemaPersoonMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaPersoonMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }
        #endregion
        #region Leasemaatschappij
        [TestMethod]
        public void SchemaToAgentLeasemaatschappijMapperReturnsLeasemaatschappij()
        {
            //Arrange
            Schema.Leasemaatschappij leasemaatschappij = new Schema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentLeaseMaatschappijMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Leasemaatschappij), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentLeasemaatschappijMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentLeaseMaatschappijMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentLeasemaatschappijMapperReturnsCorrectData()
        {
            //Arrange
            Schema.Leasemaatschappij leasemaatschappij = new Schema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentLeaseMaatschappijMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(leasemaatschappij.Naam, result.Naam);
            Assert.AreEqual(leasemaatschappij.Telefoonnummer, result.Telefoonnummer);
            Assert.AreEqual(leasemaatschappij.Klantnummer , result.Klantnummer);
            Assert.AreEqual(leasemaatschappij.ID , result.ID);
        }

        [TestMethod]
        public void AgentToSchemaLeasemaatschappijMapperReturnsLeasemaatschappij()
        {
            //Arrange
            AgentSchema.Leasemaatschappij leasemaatschappij = new AgentSchema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaLeaseMaatschappijMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(typeof(Schema.Leasemaatschappij), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaLeasemaatschappijMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaLeaseMaatschappijMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaLeasemaatschappijMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.Leasemaatschappij leasemaatschappij = new AgentSchema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaLeaseMaatschappijMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(leasemaatschappij.Naam, result.Naam);
            Assert.AreEqual(leasemaatschappij.Telefoonnummer, result.Telefoonnummer);
            Assert.AreEqual(leasemaatschappij.Klantnummer, result.Klantnummer);
            Assert.AreEqual(leasemaatschappij.ID, result.ID);
        }
        #endregion
        #region Klant
        [TestMethod]
        public void SchemaToAgentKlantMapperReturnsLeasemaatschappij()
        {
            //Arrange
            Schema.Klant leasemaatschappij = new Schema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentKlantMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Leasemaatschappij), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentKlantMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentKlantMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentKlantMapperReturnsCorrectDataLeasemaatschappij()
        {
            //Arrange
            Schema.Klant leasemaatschappij = new Schema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            Schema.Leasemaatschappij expected = (Schema.Leasemaatschappij) leasemaatschappij;
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            AgentSchema.Leasemaatschappij result = (AgentSchema.Leasemaatschappij) mapper.SchemaToAgentKlantMapper(leasemaatschappij);
            
            //Assert
            Assert.AreEqual(expected.Naam, result.Naam);
            Assert.AreEqual(expected.Klantnummer, result.Klantnummer);
            Assert.AreEqual(expected.Telefoonnummer, result.Telefoonnummer);
        }

        [TestMethod]
        public void AgentToSchemaKlantMapperReturnsLeasemaatschappij()
        {
            //Arrange
            AgentSchema.Klant leasemaatschappij = new AgentSchema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaKlantMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(typeof(Schema.Leasemaatschappij), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaKlantMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaKlantMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaKlantMapperReturnsCorrectDataLeasemaatschappij()
        {
            //Arrange
            AgentSchema.Klant leasemaatschappij = new AgentSchema.Leasemaatschappij
            {
                ID = 111111,
                Klantnummer = 123456,
                Telefoonnummer = "038-40154541",
                Naam = "Infosupport"
            };
            AgentSchema.Leasemaatschappij expected = (AgentSchema.Leasemaatschappij)leasemaatschappij;
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            Schema.Leasemaatschappij result = (Schema.Leasemaatschappij)  mapper.AgentToSchemaKlantMapper(leasemaatschappij);

            //Assert
            Assert.AreEqual(expected.Naam, result.Naam);
            Assert.AreEqual(expected.Klantnummer, result.Klantnummer);
            Assert.AreEqual(expected.Telefoonnummer, result.Telefoonnummer);
        }

        [TestMethod]
        public void AgentToSchemaKlantMapperReturnsPersoon()
        {
            //Arrange
            AgentSchema.Klant persoon = new AgentSchema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaKlantMapper(persoon);

            //Assert
            Assert.AreEqual(typeof(Schema.Persoon), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaKlantMapperReturnsCorrectDataPersoon()
        {
            //Arrange
            AgentSchema.Klant persoon = new AgentSchema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            AgentSchema.Persoon expected = (AgentSchema.Persoon) persoon;
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            Schema.Persoon result = (Schema.Persoon)mapper.AgentToSchemaKlantMapper(persoon);

            //Assert
            Assert.AreEqual(expected.Voornaam, result.Voornaam);
            Assert.AreEqual(expected.Achternaam, result.Achternaam);
            Assert.AreEqual(expected.Telefoonnummer, result.Telefoonnummer);
            Assert.AreEqual(expected.Tussenvoegsel, result.Tussenvoegsel);
            Assert.AreEqual(expected.Adres, result.Adres);
        }

        [TestMethod]
        public void SchemaToAgentKlantMapperReturnsPersoon()
        {
            //Arrange
            Schema.Klant persoon = new Schema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentKlantMapper(persoon);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Persoon), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentKlantMapperReturnsCorrectDataPersoon()
        {
            //Arrange
            Schema.Klant persoon = new Schema.Persoon
            {
                ID = 111111,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };
            Schema.Persoon expected = (Schema.Persoon) persoon;
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            AgentSchema.Persoon result = (AgentSchema.Persoon) mapper.SchemaToAgentKlantMapper(persoon);

            //Assert
            Assert.AreEqual(expected.Voornaam, result.Voornaam);
            Assert.AreEqual(expected.Achternaam, result.Achternaam);
            Assert.AreEqual(expected.Telefoonnummer, result.Telefoonnummer);
            Assert.AreEqual(expected.Tussenvoegsel, result.Tussenvoegsel);
            Assert.AreEqual(expected.Adres, result.Adres);
        }
        #endregion
        #region Voertuig
        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsVoertuig()
        {
            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(voertuig);

            //Assert
            Assert.AreEqual(typeof(Schema.Voertuig), result.GetType());
        }

        [TestMethod]
        public void AgentToAgentVoertuigMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(voertuig);

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
        }

        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsCorrectDataBestuurderPersoon()
        {
            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = new AgentSchema.Persoon
                {
                    ID = 111211,
                    Voornaam = "Marco",
                    Achternaam = "Pil",
                    Adres = "St. Jacobsstraat 18",
                    Emailadres = "marcop@gmail.com",
                    Klantnummer = 123456,
                    Postcode = "1574YD",
                    Telefoonnummer = "038-40154541",
                    Woonplaats = "Utrecht"
                }
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(voertuig);

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
            Assert.AreEqual(voertuig.Bestuurder.Voornaam, result.Bestuurder.Voornaam);
        }

        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsCorrectDataBestuurderPersoonEigenaarPersoon()
        {
            AgentSchema.Persoon persoon = new AgentSchema.Persoon
            {
                ID = 111211,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };

            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = persoon,
                Eigenaar = persoon
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(voertuig);
            Schema.Persoon actual = (Schema.Persoon) result.Eigenaar;

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
            Assert.AreEqual(voertuig.Bestuurder.Voornaam, result.Bestuurder.Voornaam);
            Assert.AreEqual(voertuig.Eigenaar.Klantnummer, result.Eigenaar.Klantnummer);
            Assert.AreEqual(persoon.Voornaam, actual.Voornaam);
        }


        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsCorrectDataBestuurderPersoonEigenaarLease()
        {
            AgentSchema.Persoon persoon = new AgentSchema.Persoon
            {
                ID = 111211,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };

            AgentSchema.Leasemaatschappij leasemaatschappij = new AgentSchema.Leasemaatschappij
            {
                ID = 111211,
                Klantnummer = 123456,
                Naam = "Infosupprt",
                Telefoonnummer = "038-40154541",
            };

            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = persoon,
                Eigenaar = leasemaatschappij
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(voertuig);
            Schema.Leasemaatschappij actual = (Schema.Leasemaatschappij) result.Eigenaar;

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
            Assert.AreEqual(voertuig.Bestuurder.Voornaam, result.Bestuurder.Voornaam);
            Assert.AreEqual(voertuig.Eigenaar.Klantnummer, result.Eigenaar.Klantnummer);
            Assert.AreEqual(leasemaatschappij.Naam, actual.Naam);
        }

        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsEigenaarLease()
        {
            AgentSchema.Leasemaatschappij leasemaatschappij = new AgentSchema.Leasemaatschappij
            {
                ID = 111211,
                Klantnummer = 123456,
                Naam = "Infosupprt",
                Telefoonnummer = "038-40154541",
            };

            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Eigenaar = leasemaatschappij
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigMapper(voertuig);
            var actual = result.Eigenaar;

            //Assert
            Assert.AreEqual(typeof(Schema.Leasemaatschappij), actual.GetType());
        }

        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsVoertuig()
        {
            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Voertuig), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsCorrectData()
        {
            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus"
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
        }

        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsCorrectDataBestuurderPersoon()
        {
            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = new Schema.Persoon
                {
                    ID = 111211,
                    Voornaam = "Marco",
                    Achternaam = "Pil",
                    Adres = "St. Jacobsstraat 18",
                    Emailadres = "marcop@gmail.com",
                    Klantnummer = 123456,
                    Postcode = "1574YD",
                    Telefoonnummer = "038-40154541",
                    Woonplaats = "Utrecht"
                }
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
            Assert.AreEqual(voertuig.Bestuurder.Voornaam, result.Bestuurder.Voornaam);
        }

        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsCorrectDataBestuurderPersoonEigenaarPersoon()
        {
            Schema.Persoon persoon = new Schema.Persoon
            {
                ID = 111211,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };

            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = persoon,
                Eigenaar = persoon
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);
            AgentSchema.Persoon actual = (AgentSchema.Persoon)result.Eigenaar;

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
            Assert.AreEqual(voertuig.Bestuurder.Voornaam, result.Bestuurder.Voornaam);
            Assert.AreEqual(voertuig.Eigenaar.Klantnummer, result.Eigenaar.Klantnummer);
            Assert.AreEqual(persoon.Voornaam, actual.Voornaam);
        }
        
        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsCorrectDataBestuurderPersoonEigenaarLease()
        {
            Schema.Persoon persoon = new Schema.Persoon
            {
                ID = 111211,
                Voornaam = "Marco",
                Achternaam = "Pil",
                Adres = "St. Jacobsstraat 18",
                Emailadres = "marcop@gmail.com",
                Klantnummer = 123456,
                Postcode = "1574YD",
                Telefoonnummer = "038-40154541",
                Woonplaats = "Utrecht"
            };

            Schema.Leasemaatschappij leasemaatschappij = new Schema.Leasemaatschappij
            {
                ID = 111211,
                Klantnummer = 123456,
                Naam = "Infosupprt",
                Telefoonnummer = "038-40154541",
            };

            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = persoon,
                Eigenaar = leasemaatschappij
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);
            AgentSchema.Leasemaatschappij actual = (AgentSchema.Leasemaatschappij)result.Eigenaar;

            //Assert
            Assert.AreEqual(voertuig.Kenteken, result.Kenteken);
            Assert.AreEqual(voertuig.Merk, result.Merk);
            Assert.AreEqual(voertuig.Type, result.Type);
            Assert.AreEqual(voertuig.Bestuurder.Voornaam, result.Bestuurder.Voornaam);
            Assert.AreEqual(voertuig.Eigenaar.Klantnummer, result.Eigenaar.Klantnummer);
            Assert.AreEqual(leasemaatschappij.Naam, actual.Naam);
        }

        [TestMethod]
        public void SchemaToAgentVoertuigMapperReturnsEigenaarLease()
        {
            Schema.Leasemaatschappij leasemaatschappij = new Schema.Leasemaatschappij
            {
                ID = 111211,
                Klantnummer = 123456,
                Naam = "Infosupprt",
                Telefoonnummer = "038-40154541",
            };

            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Eigenaar = leasemaatschappij
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);
            var actual = result.Eigenaar;

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Leasemaatschappij), actual.GetType());
        }
        #endregion
        #region Onderhoudsopdracht
        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtMapperReturnsOnderhoudsopdracht()
        {
            //Arrange
            Schema.Onderhoudsopdracht onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
            };

            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Status = "New"
            };
            
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtMapper(onderhoudsopdracht);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Onderhoudsopdracht), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtMapperReturnsCorrectData()
        {
            //Arrange
            Schema.Onderhoudsopdracht onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
                Onderhoudsomschrijving = "Olie verversen"
            };
            
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtMapper(onderhoudsopdracht);

            //Assert
            Assert.AreEqual(onderhoudsopdracht.APK, result.APK);
            Assert.AreEqual(onderhoudsopdracht.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(onderhoudsopdracht.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudsopdracht.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtMapperReturnsCorrectDataVoertuig()
        {
            //Arrange
            Schema.Voertuig voertuig = new Schema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Status = "New",
            };

            Schema.Onderhoudsopdracht onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
                Voertuig = voertuig
            };
            

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtMapper(onderhoudsopdracht);

            //Assert
            Assert.AreEqual(onderhoudsopdracht.APK, result.APK);
            Assert.AreEqual(onderhoudsopdracht.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(onderhoudsopdracht.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudsopdracht.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
            Assert.AreEqual(onderhoudsopdracht.Voertuig.Status, result.Voertuig.Status);
            Assert.AreEqual(onderhoudsopdracht.Voertuig.Kenteken, result.Voertuig.Kenteken);

        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtMapperReturnsOnderhoudsopdracht()
        {
            //Arrange
            AgentSchema.Onderhoudsopdracht onderhoudsopdracht = new AgentSchema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
                
            };

            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Status = "New"
            };
            
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtMapper(onderhoudsopdracht);

            //Assert
            Assert.AreEqual(typeof(Schema.Onderhoudsopdracht), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.Onderhoudsopdracht onderhoudsopdracht = new AgentSchema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
                Onderhoudsomschrijving = "Olie verversen"
            };
            
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtMapper(onderhoudsopdracht);

            //Assert
            Assert.AreEqual(onderhoudsopdracht.APK, result.APK);
            Assert.AreEqual(onderhoudsopdracht.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(onderhoudsopdracht.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudsopdracht.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtMapperReturnsCorrectDataVoertuig()
        {
            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Status = "New",
            };

            AgentSchema.Onderhoudsopdracht onderhoudsopdracht = new AgentSchema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
                Voertuig = voertuig
            };
            

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtMapper(onderhoudsopdracht);

            //Assert
            Assert.AreEqual(onderhoudsopdracht.APK, result.APK);
            Assert.AreEqual(onderhoudsopdracht.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(onderhoudsopdracht.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudsopdracht.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
            Assert.AreEqual(onderhoudsopdracht.Voertuig.Status, result.Voertuig.Status);
            Assert.AreEqual(onderhoudsopdracht.Voertuig.Kenteken, result.Voertuig.Kenteken);
        }
        #endregion
        #region VoertuigenSearchCriteria
        [TestMethod]
        public void SchemaToAgentVoertuigenSearchCriteriaMapperReturnsSearchCrit()
        {
            //Arrange
            Schema.VoertuigenSearchCriteria searchCriteria = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.VoertuigenSearchCriteria), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentVoertuigenSearchCriteriaMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigSearchCriteriaMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentVoertuigenSearchCriteriaMapperReturnsCorrectData()
        {
            //Arrange
            Schema.VoertuigenSearchCriteria searchCriteria = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(searchCriteria.ID, result.ID);
            Assert.AreEqual(searchCriteria.Merk, result.Merk);
            Assert.AreEqual(searchCriteria.Kenteken, result.Kenteken);
            Assert.AreEqual(searchCriteria.Type, result.Type);
        }


        [TestMethod]
        public void AgentToSchemaVoertuigenSearchCriteriaMapperReturnsSearchCrit()
        {
            //Arrange
            AgentSchema.VoertuigenSearchCriteria searchCriteria = new AgentSchema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(typeof(Schema.VoertuigenSearchCriteria), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaVoertuigenSearchCriteriaMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigSearchCriteriaMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaVoertuigenSearchCriteriaMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.VoertuigenSearchCriteria searchCriteria = new AgentSchema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaVoertuigSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(searchCriteria.ID, result.ID);
            Assert.AreEqual(searchCriteria.Merk, result.Merk);
            Assert.AreEqual(searchCriteria.Kenteken, result.Kenteken);
            Assert.AreEqual(searchCriteria.Type, result.Type);
        }

        #endregion
        #region OnderhoudsopdrachtenSearchCriteria
        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtenSearchCriteriaMapperReturnsSearchCrit()
        {
            //Arrange
            Schema.OnderhoudsopdrachtZoekCriteria searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                ID = 111111,
                APK = true,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "test",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.OnderhoudsopdrachtZoekCriteria), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtenSearchCriteriaMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtSearchCriteriaMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtnSearchCriteriaMapperReturnsCorrectData()
        {
            //Arrange
            Schema.OnderhoudsopdrachtZoekCriteria searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                ID = 111111,
                APK = true,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "test",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(searchCriteria.ID, result.ID);
            Assert.AreEqual(searchCriteria.APK, result.APK);
            Assert.AreEqual(searchCriteria.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(searchCriteria.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(searchCriteria.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudsopdrachtnSearchCriteriaMapperReturnsCorrectDataWithVoertuigSearchCriteria()
        {
            //Arrange

            Schema.VoertuigenSearchCriteria voertuigenSearchCriteria = new Schema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            Schema.OnderhoudsopdrachtZoekCriteria searchCriteria = new Schema.OnderhoudsopdrachtZoekCriteria
            {
                ID = 111111,
                APK = true,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "test",
                VoertuigenSearchCriteria = voertuigenSearchCriteria
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudsopdrachtSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(searchCriteria.ID, result.ID);
            Assert.AreEqual(searchCriteria.APK, result.APK);
            Assert.AreEqual(searchCriteria.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(searchCriteria.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(searchCriteria.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
            Assert.AreEqual(searchCriteria.VoertuigenSearchCriteria.Kenteken, result.VoertuigenSearchCriteria.Kenteken);
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtenSearchCriteriaMapperReturnsSearchCrit()
        {
            //Arrange
            AgentSchema.OnderhoudsopdrachtZoekCriteria searchCriteria = new AgentSchema.OnderhoudsopdrachtZoekCriteria
            {
                ID = 111111,
                APK = true,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "test",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(typeof(Schema.OnderhoudsopdrachtZoekCriteria), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtenSearchCriteriaMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtSearchCriteriaMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtnSearchCriteriaMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.OnderhoudsopdrachtZoekCriteria searchCriteria = new AgentSchema.OnderhoudsopdrachtZoekCriteria
            {
                ID = 111111,
                APK = true,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "test",
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(searchCriteria.ID, result.ID);
            Assert.AreEqual(searchCriteria.APK, result.APK);
            Assert.AreEqual(searchCriteria.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(searchCriteria.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(searchCriteria.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudsopdrachtnSearchCriteriaMapperReturnsCorrectDataWithVoertuigSearchCriteria()
        {
            //Arrange

            AgentSchema.VoertuigenSearchCriteria voertuigenSearchCriteria = new AgentSchema.VoertuigenSearchCriteria
            {
                ID = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
            };

            AgentSchema.OnderhoudsopdrachtZoekCriteria searchCriteria = new AgentSchema.OnderhoudsopdrachtZoekCriteria
            {
                ID = 111111,
                APK = true,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "test",
                VoertuigenSearchCriteria = voertuigenSearchCriteria
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudsopdrachtSearchCriteriaMapper(searchCriteria);

            //Assert
            Assert.AreEqual(searchCriteria.ID, result.ID);
            Assert.AreEqual(searchCriteria.APK, result.APK);
            Assert.AreEqual(searchCriteria.Aanmeldingsdatum, result.Aanmeldingsdatum);
            Assert.AreEqual(searchCriteria.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(searchCriteria.Onderhoudsomschrijving, result.Onderhoudsomschrijving);
            Assert.AreEqual(searchCriteria.VoertuigenSearchCriteria.Kenteken, result.VoertuigenSearchCriteria.Kenteken);
        }

        #endregion
        #region Onderhoudswerkzaamheden
        [TestMethod]
        public void SchemaToAgentOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsSearchCrit()
        {
            //Arrange
            Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                ID = 111111,
                Kilometerstand = 100000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen"
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden);

            //Assert
            Assert.AreEqual(typeof(AgentSchema.Onderhoudswerkzaamheden), result.GetType());
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudswerkzaamhedenMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsCorrectData()
        {
            //Arrange
            Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                ID = 111111,
                Kilometerstand = 100000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen"
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden);

            //Assert
            Assert.AreEqual(onderhoudswerkzaamheden.ID, result.ID);
            Assert.AreEqual(onderhoudswerkzaamheden.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudswerkzaamheden.Afmeldingsdatum, result.Afmeldingsdatum);
            Assert.AreEqual(onderhoudswerkzaamheden.Onderhoudswerkzaamhedenomschrijving, result.Onderhoudswerkzaamhedenomschrijving);
            
        }

        [TestMethod]
        public void SchemaToAgentOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsCorrectDataWithOpdracht()
        {
            //Arrange
            Schema.Onderhoudsopdracht onderhoudsopdracht = new Schema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
            };

            Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden = new Schema.Onderhoudswerkzaamheden
            {
                ID = 111111,
                Kilometerstand = 100000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Onderhoudsopdracht = onderhoudsopdracht
            };
            
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden);

            //Assert
            Assert.AreEqual(onderhoudswerkzaamheden.ID, result.ID);
            Assert.AreEqual(onderhoudswerkzaamheden.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudswerkzaamheden.Afmeldingsdatum, result.Afmeldingsdatum);
            Assert.AreEqual(onderhoudswerkzaamheden.Onderhoudswerkzaamhedenomschrijving, result.Onderhoudswerkzaamhedenomschrijving);
            Assert.AreEqual(onderhoudswerkzaamheden.Onderhoudsopdracht.Kilometerstand, result.Onderhoudsopdracht.Kilometerstand);

        }

        [TestMethod]
        public void AgentToSchemaOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsSearchCrit()
        {
            //Arrange
            AgentSchema.Onderhoudswerkzaamheden onderhoudswerkzaamheden = new AgentSchema.Onderhoudswerkzaamheden
            {
                ID = 111111,
                Kilometerstand = 100000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen"
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden);

            //Assert
            Assert.AreEqual(typeof(Schema.Onderhoudswerkzaamheden), result.GetType());
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsNullIfEmpty()
        {
            //Arrange
            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudswerkzaamhedenMapper(null);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void AgentToSchemaOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsCorrectData()
        {
            //Arrange
            AgentSchema.Onderhoudswerkzaamheden onderhoudswerkzaamheden = new AgentSchema.Onderhoudswerkzaamheden
            {
                ID = 111111,
                Kilometerstand = 100000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen"
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden);

            //Assert
            Assert.AreEqual(onderhoudswerkzaamheden.ID, result.ID);
            Assert.AreEqual(onderhoudswerkzaamheden.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudswerkzaamheden.Afmeldingsdatum, result.Afmeldingsdatum);
            Assert.AreEqual(onderhoudswerkzaamheden.Onderhoudswerkzaamhedenomschrijving, result.Onderhoudswerkzaamhedenomschrijving);

        }

        [TestMethod]
        public void AgentToSchemaOnderhoudswerkzaamhedenSearchCriteriaMapperReturnsCorrectDataWithOpdracht()
        {
            //Arrange
            AgentSchema.Onderhoudsopdracht onderhoudsopdracht = new AgentSchema.Onderhoudsopdracht
            {
                ID = 111111,
                APK = false,
                Aanmeldingsdatum = DateTime.Now,
                Kilometerstand = 100000,
            };

            AgentSchema.Onderhoudswerkzaamheden onderhoudswerkzaamheden = new AgentSchema.Onderhoudswerkzaamheden
            {
                ID = 111111,
                Kilometerstand = 100000,
                Afmeldingsdatum = DateTime.Now,
                Onderhoudswerkzaamhedenomschrijving = "uitlaat vervangen",
                Onderhoudsopdracht = onderhoudsopdracht
            };

            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.AgentToSchemaOnderhoudswerkzaamhedenMapper(onderhoudswerkzaamheden);

            //Assert
            Assert.AreEqual(onderhoudswerkzaamheden.ID, result.ID);
            Assert.AreEqual(onderhoudswerkzaamheden.Kilometerstand, result.Kilometerstand);
            Assert.AreEqual(onderhoudswerkzaamheden.Afmeldingsdatum, result.Afmeldingsdatum);
            Assert.AreEqual(onderhoudswerkzaamheden.Onderhoudswerkzaamhedenomschrijving, result.Onderhoudswerkzaamhedenomschrijving);
            Assert.AreEqual(onderhoudswerkzaamheden.Onderhoudsopdracht.Kilometerstand, result.Onderhoudsopdracht.Kilometerstand);

        }
        #endregion

    }
}
