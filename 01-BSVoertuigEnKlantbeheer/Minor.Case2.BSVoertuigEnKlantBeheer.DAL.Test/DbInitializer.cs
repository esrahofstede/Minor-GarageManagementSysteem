using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Test
{
    internal class DbInitializer : DropCreateDatabaseAlways<VoertuigEnKlantContext>
    {
        protected override void Seed(VoertuigEnKlantContext context)
        {
            Persoon p1 = new Persoon
            {
                Klantnummer = 123456,
                Voornaam = "Jan",
                Achternaam = "Jansen"

            };

            Leasemaatschappij l1 = new Leasemaatschappij
            {
                Klantnummer = 555666,
                Naam = "Lease"
            };

            Voertuig v1 = new Voertuig
            {
                Kenteken = "NL-GN-12",
                Merk = "Ford",
                Type = "Focus",
                Bestuurder = p1,
                Eigenaar = l1,
                
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
                Aanmeldingsdatum = new DateTime(2014, 12, 1),
                APK = false,
                Kilometerstand = 14050,
                Onderhoudsomschrijving = "Oliepeil is laag",
                Voertuig = v1
            };

            context.Klanten.AddRange(new Persoon[] { p1 });
            context.Klanten.AddRange(new Leasemaatschappij[] { l1 });
            context.Voertuigen.AddRange(new Voertuig[] { v1 });

            context.OnderhoudsOpdrachten.AddRange(new Onderhoudsopdracht[] { o1, o2 });
            context.OnderhoudsWerkzaamheden.AddRange(new Onderhoudswerkzaamheden[] { ow1 });


            base.Seed(context);
        }
    }
}
