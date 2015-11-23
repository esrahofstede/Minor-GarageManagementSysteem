using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappings
{
    public class VoertuigMapping : EntityTypeConfiguration<Voertuig>
    {
        public VoertuigMapping()
        {
            this.ToTable("Voertuig");

            this.HasKey(v => v.ID);

            this.Property(v => v.Kenteken)
                .IsRequired()
                .HasMaxLength(8);

            //this.Property(v => v.Kenteken)
            //    .HasColumnAnnotation("Index",
            //    new IndexAnnotation(new IndexAttribute("IX_Kenteken") { IsUnique = true }));

            this.Property(v => v.Merk)
                .HasMaxLength(300);

            this.Property(v => v.Type)
                .HasMaxLength(300);

            this.HasMany(v => v.OnderhoudsOpdrachten);

            this.Ignore(v => v.Bestuurder);
            this.Ignore(v => v.Eigenaar);
            //this.HasOptional(v => v.Eigenaar);
            //this.HasRequired(v => v.Bestuurder);

        }
    }
}
