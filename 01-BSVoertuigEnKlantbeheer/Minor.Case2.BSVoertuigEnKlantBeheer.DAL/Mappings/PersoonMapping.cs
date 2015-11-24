using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappings
{
    public class PersoonMapping : EntityTypeConfiguration<Persoon>
    {
        public PersoonMapping()
        {
            this.ToTable("Persoon");

            this.Property(p => p.Achternaam)
                   .HasMaxLength(300);
            this.Property(p => p.Tussenvoegsel)
                    .HasMaxLength(300);
            this.Property(p => p.Achternaam)
                    .HasMaxLength(300);
            this.Property(p => p.Adres)
                    .HasMaxLength(300);
            this.Property(p => p.Postcode)
                    .HasMaxLength(8);
            this.Property(p => p.Woonplaats)
                    .HasMaxLength(300);
            this.Property(k => k.Emailadres)
                    .HasMaxLength(300);
        }
    }
}
