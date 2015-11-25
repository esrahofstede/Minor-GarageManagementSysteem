using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using AgentSchema = Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema.Agent;

namespace Minor.Case2.PcSOnderhoud.Agent
{
    public class BSKlantEnVoertuigMapper
    {
        public AgentSchema.Persoon SchemaToAgentPersoonMapper(Schema.Persoon persoon)
        {
            if (persoon == null)
            {
                return null;
            }
            return new AgentSchema.Persoon
            {
                ID = persoon.ID,
                Voornaam = persoon.Voornaam,
                Tussenvoegsel = persoon.Tussenvoegsel,
                Achternaam = persoon.Achternaam,
                Adres = persoon.Adres,
                Emailadres = persoon.Adres,
                Klantnummer = persoon.Klantnummer,
                Postcode = persoon.Postcode,
                Telefoonnummer = persoon.Telefoonnummer,
                Woonplaats = persoon.Woonplaats
            };
        }

        public Schema.Persoon AgentToSchemaPersoonMapper(AgentSchema.Persoon persoon)
        {
            if (persoon == null)
            {
                return null;
            }
            return new Schema.Persoon
            {
                ID = persoon.ID,
                Voornaam = persoon.Voornaam,
                Tussenvoegsel = persoon.Tussenvoegsel,
                Achternaam = persoon.Achternaam,
                Adres = persoon.Adres,
                Emailadres = persoon.Adres,
                Klantnummer = persoon.Klantnummer,
                Postcode = persoon.Postcode,
                Telefoonnummer = persoon.Telefoonnummer,
                Woonplaats = persoon.Woonplaats
            };
        }

        public AgentSchema.Leasemaatschappij SchemaToAgentLeasemaatschappijMapper(Schema.Leasemaatschappij leasemaatschappij)
        {
            if (leasemaatschappij == null)
            {
                return null;
            }
            return new AgentSchema.Leasemaatschappij
            {
                ID = leasemaatschappij.ID,
                Naam = leasemaatschappij.Naam,
                Klantnummer = leasemaatschappij.Klantnummer,
                Telefoonnummer = leasemaatschappij.Telefoonnummer,
            };
        }

        public Schema.Leasemaatschappij AgentToSchemaLeasemaatschappijMapper(AgentSchema.Leasemaatschappij leasemaatschappij)
        {
            if (leasemaatschappij == null)
            {
                return null;
            }

            return new Schema.Leasemaatschappij
            {
                ID = leasemaatschappij.ID,
                Naam = leasemaatschappij.Naam,
                Klantnummer = leasemaatschappij.Klantnummer,
                Telefoonnummer = leasemaatschappij.Telefoonnummer,
            };
        }

        public AgentSchema.Klant SchemaToAgentKlantMapper(Schema.Klant klant)
        {
            if (klant == null)
            {
                return null;
            }
            if (klant.GetType() == typeof(Schema.Persoon))
            {
                return SchemaToAgentPersoonMapper((Schema.Persoon)klant);
            }
            return SchemaToAgentLeasemaatschappijMapper((Schema.Leasemaatschappij)klant);
        }

        public Schema.Klant AgentToSchemaKlantMapper(AgentSchema.Klant klant)
        {
            if (klant == null)
            {
                return null;
            }
            if (klant.GetType() == typeof(AgentSchema.Persoon))
            {
                return AgentToSchemaPersoonMapper((AgentSchema.Persoon)klant);
            }
            return AgentToSchemaLeasemaatschappijMapper((AgentSchema.Leasemaatschappij)klant);
        }

        public AgentSchema.Voertuig SchemaToAgentVoertuigMapper(Schema.Voertuig voertuig)
        {
            if (voertuig == null)
            {
                return null;
            }

            return new AgentSchema.Voertuig
            {
                 ID = voertuig.ID,
                 Kenteken = voertuig.Kenteken,
                 Merk = voertuig.Merk,
                 Type = voertuig.Type,
                 Eigenaar = SchemaToAgentKlantMapper(voertuig.Eigenaar),
                 Bestuurder = SchemaToAgentPersoonMapper(voertuig.Bestuurder),
                 Status = voertuig.Status
            };
        }
        public Schema.Voertuig AgentToSchemaVoertuigMapper(AgentSchema.Voertuig voertuig)
        {
            if (voertuig == null)
            {
                return null;
            }

            return new Schema.Voertuig
            {
                ID = voertuig.ID,
                Kenteken = voertuig.Kenteken,
                Merk = voertuig.Merk,
                Type = voertuig.Type,
                Eigenaar = AgentToSchemaKlantMapper(voertuig.Eigenaar),
                Bestuurder = AgentToSchemaPersoonMapper(voertuig.Bestuurder),
                Status = voertuig.Status
            };
        }

        public AgentSchema.Onderhoudsopdracht SchemaToAgentOnderhoudsopdrachtMapper(Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            if (onderhoudsopdracht == null)
            {
                return null;
            }
            return new AgentSchema.Onderhoudsopdracht
            {
                ID = onderhoudsopdracht.ID,
                Aanmeldingsdatum = onderhoudsopdracht.Aanmeldingsdatum,
                Onderhoudsomschrijving = onderhoudsopdracht.Onderhoudsomschrijving,
                APK = onderhoudsopdracht.APK,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                Voertuig = SchemaToAgentVoertuigMapper(onderhoudsopdracht.Voertuig)
            };
        }

        public Schema.Onderhoudsopdracht AgentToSchemaOnderhoudsopdrachtMapper(AgentSchema.Onderhoudsopdracht onderhoudsopdracht)
        {
            if (onderhoudsopdracht == null)
            {
                return null;
            }
            return new Schema.Onderhoudsopdracht
            {
                ID = onderhoudsopdracht.ID,
                Aanmeldingsdatum = onderhoudsopdracht.Aanmeldingsdatum,
                Onderhoudsomschrijving = onderhoudsopdracht.Onderhoudsomschrijving,
                APK = onderhoudsopdracht.APK,
                Kilometerstand = onderhoudsopdracht.Kilometerstand,
                Voertuig = AgentToSchemaVoertuigMapper(onderhoudsopdracht.Voertuig)
            };
        }

        public Schema.VoertuigenSearchCriteria AgentToSchemaVoertuigSearchCriteriaMapper(AgentSchema.VoertuigenSearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                return null;
            }
            return new Schema.VoertuigenSearchCriteria
            {
                ID = searchCriteria.ID,
                Kenteken = searchCriteria.Kenteken,
                Merk = searchCriteria.Merk,
                Type = searchCriteria.Type,
            };
        }

        public AgentSchema.VoertuigenSearchCriteria SchemaToAgentVoertuigSearchCriteriaMapper(Schema.VoertuigenSearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                return null;
            }
            return new AgentSchema.VoertuigenSearchCriteria
            {
                ID = searchCriteria.ID,
                Kenteken = searchCriteria.Kenteken,
                Merk = searchCriteria.Merk,
                Type = searchCriteria.Type,
            };
        }

        public AgentSchema.OnderhoudsopdrachtZoekCriteria SchemaToAgentOnderhoudsopdrachtSearchCriteriaMapper(Schema.OnderhoudsopdrachtZoekCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                return null;
            }
            return new AgentSchema.OnderhoudsopdrachtZoekCriteria
            {
                ID = searchCriteria.ID,
                Aanmeldingsdatum = searchCriteria.Aanmeldingsdatum,
                APK = searchCriteria.APK,
                Kilometerstand = searchCriteria.Kilometerstand,
                Onderhoudsomschrijving = searchCriteria.Onderhoudsomschrijving,
                VoertuigenSearchCriteria = SchemaToAgentVoertuigSearchCriteriaMapper(searchCriteria.VoertuigenSearchCriteria),
            };
        }

        public Schema.OnderhoudsopdrachtZoekCriteria AgentToSchemaOnderhoudsopdrachtSearchCriteriaMapper(AgentSchema.OnderhoudsopdrachtZoekCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                return null;
            }
            return new Schema.OnderhoudsopdrachtZoekCriteria
            {
                ID = searchCriteria.ID,
                Aanmeldingsdatum = searchCriteria.Aanmeldingsdatum,
                APK = searchCriteria.APK,
                Kilometerstand = searchCriteria.Kilometerstand,
                Onderhoudsomschrijving = searchCriteria.Onderhoudsomschrijving,
                VoertuigenSearchCriteria = AgentToSchemaVoertuigSearchCriteriaMapper(searchCriteria.VoertuigenSearchCriteria),
            };
        }

        public AgentSchema.Onderhoudswerkzaamheden SchemaToAgentOnderhoudswerkzaamhedenMapper(Schema.Onderhoudswerkzaamheden onderhoudswerkzaamheden)
        {
            if (onderhoudswerkzaamheden == null)
            {
                return null;
            }
            return new AgentSchema.Onderhoudswerkzaamheden
            {
                ID = onderhoudswerkzaamheden.ID,
                Kilometerstand = onderhoudswerkzaamheden.Kilometerstand,
                Afmeldingsdatum= onderhoudswerkzaamheden.Afmeldingsdatum,
                Onderhoudswerkzaamhedenomschrijving= onderhoudswerkzaamheden.Onderhoudswerkzaamhedenomschrijving,
                Onderhoudsopdracht= SchemaToAgentOnderhoudsopdrachtMapper(onderhoudswerkzaamheden.Onderhoudsopdracht),
            };
        }

        public Schema.Onderhoudswerkzaamheden AgentToSchemaOnderhoudswerkzaamhedenMapper(AgentSchema.Onderhoudswerkzaamheden onderhoudswerkzaamheden)
        {
            if (onderhoudswerkzaamheden == null)
            {
                return null;
            }
            return new Schema.Onderhoudswerkzaamheden
            {
                ID = onderhoudswerkzaamheden.ID,
                Kilometerstand = onderhoudswerkzaamheden.Kilometerstand,
                Afmeldingsdatum = onderhoudswerkzaamheden.Afmeldingsdatum,
                Onderhoudswerkzaamhedenomschrijving = onderhoudswerkzaamheden.Onderhoudswerkzaamhedenomschrijving,
                Onderhoudsopdracht = AgentToSchemaOnderhoudsopdrachtMapper(onderhoudswerkzaamheden.Onderhoudsopdracht),
            };
        }
    }
}
