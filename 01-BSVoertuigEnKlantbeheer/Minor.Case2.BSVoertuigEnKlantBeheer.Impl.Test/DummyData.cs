using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Impl.Test
{
    internal static class DummyData
    {
        public static IEnumerable<Onderhoudsopdracht> GetDummyOnderhoudsopdrachtenCollection()
        {
            List<Onderhoudsopdracht> voertuigen = new List<Onderhoudsopdracht>();
            var o1 = new Onderhoudsopdracht
            {
                ID = 1,
                Aanmeldingsdatum = new DateTime(2015, 10, 09),
                Kilometerstand = 1000,
                Onderhoudsomschrijving = "Onderhoudsomschrijving 1",
                Voertuig = new Voertuig
                {
                    Kenteken = "GG-WP-13"
                }                  
            };
            var o2 = new Onderhoudsopdracht
            {
                ID = 2,
                Aanmeldingsdatum = new DateTime(2015, 10, 09),
                Kilometerstand = 2000,
                Onderhoudsomschrijving = "Onderhoudsomschrijving 2",
                Voertuig = new Voertuig
                {
                    Kenteken = "GG-WP-13"
                }
            };
            var o3 = new Onderhoudsopdracht
            {
                ID = 3,
                Aanmeldingsdatum = new DateTime(2015, 09, 09),
                Kilometerstand = 3000,
                Onderhoudsomschrijving = "Onderhoudsomschrijving 3",
                Voertuig = new Voertuig
                {
                    Kenteken = "BG-NP-12"
                }
            };

            voertuigen.AddRange(new Onderhoudsopdracht[] { o1, o2, o3 });
            return voertuigen;
        }

        internal static IEnumerable<Voertuig> GetDummyVoertuigCollection()
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            var v1 = new Voertuig
            {
                ID = 1,
                Kenteken = "NL-123-1",
                Merk = "Volkswagen",
                Type = "Polo",
                Bestuurder = new Persoon(),
                Eigenaar = new Persoon(),
            };
            var v2 = new Voertuig
            {
                ID = 2,
                Kenteken = "NL-123-2",
                Merk = "Volkswagen",
                Type = "Golf",
                Bestuurder = new Persoon(),
                Eigenaar = new Persoon(),
            };
            var v3 = new Voertuig
            {
                ID = 3,
                Kenteken = "NL-123-3",
                Merk = "Citroen",
                Type = "C3",
                Bestuurder = new Persoon(),
                Eigenaar = new Persoon(),
            };

            voertuigen.AddRange(new Voertuig[] { v1, v2, v3 });
            return voertuigen;
        }

        internal static IEnumerable<Persoon> GetDummyPersoonCollection()
        {
            List<Persoon> personen = new List<Persoon>();
            var p1 = new Persoon
            {
                ID = 1,
                Voornaam = "Jan"
            };
            var p2 = new Persoon
            {
                ID = 2,
                Voornaam = "Peter"
            };

            personen.AddRange(new Persoon[] { p1, p2 });

            return personen;
        }

        internal static IEnumerable<Leasemaatschappij> GetDummyLeasemaatschappijCollection()
        {
            List<Leasemaatschappij> leasemaatschappijen = new List<Leasemaatschappij>();
            var l1 = new Leasemaatschappij
            {
                ID = 3,
                Naam = "Sixt"
                
            };
            leasemaatschappijen.AddRange(new Leasemaatschappij[] { l1 });

            return leasemaatschappijen;
        }
    }
}
