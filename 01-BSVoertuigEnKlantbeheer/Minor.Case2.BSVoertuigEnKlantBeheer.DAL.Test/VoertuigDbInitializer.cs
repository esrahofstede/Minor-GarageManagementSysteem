using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    internal class VoertuigDbInitializer : DropCreateDatabaseAlways<VoertuigContext>
    {
        protected override void Seed(VoertuigContext context)
        {

            Voertuig v1 = new Voertuig
            {
                Kenteken = "NL-GN-12",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = new Persoon
                {
                    Klantnummer = 1
                },
                BestuurderID = 1,
                Eigenaar = new Persoon
                {
                    Klantnummer = 1
                },
                EigenaarID = 1
               
            };

            Onderhoudswerkzaamheden ow1 = new Onderhoudswerkzaamheden
            {
                Afmeldingsdatum = new DateTime(2014, 12, 1),
                Kilometerstand = 14050,
                Omschrijving = "Olie vervangen"

            };

            Onderhoudsopdracht o1 = new Onderhoudsopdracht
            {
                Aanmeldingsdatum = new DateTime(2014, 12, 1),
                APK = false,
                Kilometerstand = 14050,
                Onderhoudsomschrijving = "Oliepeil is laag",
                Voertuig = v1,
                Onderhoudswerkzaamheden = ow1
            };

            Onderhoudsopdracht o2 = new Onderhoudsopdracht
            {
                Aanmeldingsdatum = new DateTime(2014, 12, 2),
                APK = false,
                Kilometerstand = 14050,
                Onderhoudsomschrijving = "Oliepeil te hoog",
                Voertuig = v1
            };

            context.Voertuigen.AddRange(new Voertuig[] { v1 });

            context.OnderhoudsOpdrachten.AddRange(new Onderhoudsopdracht[] { o1, o2 });
            context.OnderhoudsWerkzaamheden.AddRange(new Onderhoudswerkzaamheden[] { ow1 });


            base.Seed(context);
        }
    }
}
