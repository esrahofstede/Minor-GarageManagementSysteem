using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent.Tests
{
    [TestClass]
    public class AgentBSKlantEnVoertuigMapperTest
    {
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

        [TestMethod]
        public void AgentToSchemaVoertuigMapperReturnsVoertuig()
        {
            //Arrange
            AgentSchema.Voertuig voertuig = new AgentSchema.Voertuig
            {
                Id = 111111,
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
                Id = 111111,
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
                Id = 111111,
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
        public void SchemaToAgentVoertuigMapperReturnsCorrectDataBestuurderPersoonEigenaarPersoon()
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
                Id = 111111,
                Kenteken = "14-TT-KJ",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = persoon,
                Eigenaar = persoon
            };


            BSKlantEnVoertuigMapper mapper = new BSKlantEnVoertuigMapper();

            //Act
            var result = mapper.SchemaToAgentVoertuigMapper(voertuig);
            AgentSchema.Persoon actual = (AgentSchema.Persoon) result.Eigenaar;

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
            AgentSchema.Leasemaatschappij actual = (AgentSchema.Leasemaatschappij) result.Eigenaar;

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
    }
}
