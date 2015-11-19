using Minor.Case2.ISRDW.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Minor.Case2.ISRDW.DAL.Tests
{
    public class RDWDatabaseInitializer : DropCreateDatabaseAlways<RDWContext>
    {
        protected override void Seed(RDWContext context)
        {
            List<Logging> logs = new List<Logging>();

            logs.Add(new Logging
            {
                Keuringsregistratie = new Keuringsregistratie
                {
                    CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                    Kenteken = "BV-01-EG",
                    Keuringsdatum = new DateTime(2008, 11, 19),
                    Steekproef = null,
                },
                Time = new DateTime(2015, 11, 18, 11, 32, 44),
            });

            logs.Add(new Logging
            {
                Keuringsverzoek = new Keuringsverzoek
                {
                    CorrelatieId = "0038c17b-aa10-4f93-8569-d184fdfc265b",
                    Kenteken = "BV-01-EG",
                    Keuringsdatum = new DateTime(2008, 11, 19),
                    KeuringsinstantieNaam = "Garage Voorbeeld B.V",
                    KeuringsinstantiePlaats = "Wijk bij Voorbeeld",
                    KeuringsinstantieType = "garage",
                    KVK = "3013 5370",
                    NaamEigenaar = "A. Eigenaar",
                },
                Time = new DateTime(2015, 11, 18, 11, 31, 16),
            });

            context.Logs.AddRange(logs);

            base.Seed(context);
        }
    }
}