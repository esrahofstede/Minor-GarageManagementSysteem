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
    internal class KlantDbInitializer : DropCreateDatabaseAlways<KlantContext>
    {
        protected override void Seed(KlantContext context)
        {
            Persoon p1 = new Persoon
            {
                Klantnummer = 000001,
                Voornaam = "Jan",
                Achternaam = "Jansen"

            };

            Persoon p2 = new Persoon
            {
                Klantnummer = 000002,
                Voornaam = "Kees",
                Telefoonnummer = "de",
                Achternaam = "Koning"
            };

            Leasemaatschappij l1 = new Leasemaatschappij
            {
                Klantnummer = 000010,
                Naam = "Lease"
            };
        
            context.Klanten.AddRange(new Persoon[] { p1, p2 });
            context.Klanten.AddRange(new Leasemaatschappij[] { l1 });

            base.Seed(context);
        }
    }
}
