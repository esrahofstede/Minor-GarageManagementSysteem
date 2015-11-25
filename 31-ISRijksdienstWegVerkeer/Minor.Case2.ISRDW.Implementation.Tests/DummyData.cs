using Minor.Case2.ISRDW.DAL.Entities;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Messages;
using Minor.Case2.ISRijksdienstWegVerkeer.V1.Schema;
using minorcase2bsvoertuigenklantbeheer.v1.schema;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Minor.Case2.ISRDW.Implementation.Tests
{
    internal static class DummyData
    {
        internal static apkKeuringsverzoekRequestMessage GetApkKeuringsverzoekRequestMessage()
        {
            return new apkKeuringsverzoekRequestMessage
            {
                keuringsverzoek = new keuringsverzoek
                {
                    keuringsdatum = new DateTime(2008, 11, 19),
                    keuringsinstantie = new keuringsinstantie
                    {
                        kvk = "3013 5370",
                        type = "garage",
                        naam = "Garage Voorbeeld B.V.",
                        plaats = "Wijk bij Voorbeeld",
                    },
                    voertuig = new keuringsverzoekVoertuig
                    {
                        kenteken = "BV-01-EG",
                        kilometerstand = 12345,
                        naam = "A. Eigenaar",
                        type = voertuigtype.personenauto
                    },
                    correlatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                }
            };
        }

        internal static SendRdwKeuringsverzoekRequestMessage GetSendRdwKeuringsverzoekLeasemaatschappijRequestMessage()
        {
            return new SendRdwKeuringsverzoekRequestMessage
            {
                Garage = new Garage
                {
                    Naam = "test",
                    Kvk = "123112344",
                    Plaats = "Utrecht",
                    Type = "Garage",
                },
                Voertuig = new Voertuig
                {
                    Kenteken = "12-AA-BB",
                    Merk = "ford",
                    Type = "focus",
                    Bestuurder = new Persoon
                    {
                        Voornaam = "jan",
                        Achternaam = "jansen"
                    },
                    Eigenaar = new Leasemaatschappij
                    {
                        Naam = "Sixt"
                    },
                    ID = 1
                },
                Keuringsverzoek = new ISRijksdienstWegVerkeer.V1.Schema.Keuringsverzoek
                {
                    CorrolatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                    Date = DateTime.Now
                }
            };
        }

        internal static SendRdwKeuringsverzoekRequestMessage GetSendRdwKeuringsverzoekPersoonRequestMessage()
        {
            return new SendRdwKeuringsverzoekRequestMessage
            {
                Garage = new Garage
                {
                    Naam = "test",
                    Kvk = "123112344",
                    Plaats = "Utrecht",
                    Type = "Garage",
                },
                Voertuig = new Voertuig
                {
                    Kenteken = "12-AA-BB",
                    Merk = "ford",
                    Type = "focus",
                    Bestuurder = new Persoon
                    {
                        Voornaam = "jan",
                        Achternaam = "jansen"
                    },
                    Eigenaar = new Persoon
                    {
                        Voornaam = "jan",
                        Achternaam = "jansen"
                    },
                    ID = 1
                },
                Keuringsverzoek = new ISRijksdienstWegVerkeer.V1.Schema.Keuringsverzoek
                {
                    CorrolatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                    Date = DateTime.Now
                }
            };
        }

        internal static apkKeuringsverzoekResponseMessage GetApkKeuringsverzoekResponseMessage()
        {
            return new apkKeuringsverzoekResponseMessage
            {
                keuringsregistratie = new keuringsregistratie()
                {
                    correlatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                    kenteken = "BV-01-EG",
                    steekproef = null,
                    keuringsdatum = new DateTime(2008, 11, 19),
                    steekproefSpecified = true
                }
            };
        }

        internal static Logging GetLogging()
        {
            return new Logging
            {
                Time = new DateTime(2015, 11, 18, 11, 13, 00),
                Keuringsregistratie = new Keuringsregistratie
                {
                    CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                    Kenteken = "BV-01-EG",
                    Keuringsdatum = new DateTime(2008, 11, 19),
                    Steekproef = null,
                }
            };
        }

        internal static IEnumerable<Logging> GetAllKeuringsregistratieLoggings()
        {
            return new List<Logging>
            {
                new Logging
                {
                    Time = new DateTime(2015, 11, 18, 11, 13, 00),
                    Keuringsregistratie = new Keuringsregistratie
                    {
                        CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                        Kenteken = "BV-01-EG",
                        Keuringsdatum = new DateTime(2008, 11, 19),
                        Steekproef = null,
                    }
                },

            };
        }

        internal static IEnumerable<Logging> GetAllKeuringsverzoekLoggings()
        {
            return new List<Logging>
            {
                new Logging
                {
                    Time = new DateTime(2015, 11, 18, 11, 13, 00),
                    Keuringsverzoek = new DAL.Entities.Keuringsverzoek
                    {
                        CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                        Kenteken = "BV-01-EG",
                        NaamEigenaar = "A. Eigenaar",
                        VoertuigType = "personenauto",
                        Kilometerstand = 12345,
                        Keuringsdatum = new DateTime(2008, 11, 19),
                        KeuringsinstantieNaam = "Garage Voorbeeld B.V.",
                        KeuringsinstantiePlaats = "Wijk bij Voorbeeld",
                        KeuringsinstantieType = "garage",
                        KVK = "3013 5370",
                    }

                },

            };
        }

    }
}
