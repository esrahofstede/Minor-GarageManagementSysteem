using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappings
{
    public class OnderhoudsOpdrachtMapping : EntityTypeConfiguration<Onderhoudsopdracht>
    {
        public OnderhoudsOpdrachtMapping()
        {
            this.ToTable("OnderhoudsOpdracht");

            this.HasKey(o => o.ID);

            this.Property(o => o.Onderhoudsomschrijving)
                .HasMaxLength(300);

            this.HasRequired(o => o.Voertuig);
            this.HasOptional(o => o.Onderhoudswerkzaamheden);
        }
    }
}
