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
    public class KlantMapping : EntityTypeConfiguration<Klant>
    {
        public KlantMapping()
        {
            this.ToTable("Klant");

            this.HasKey(k => k.ID);

            this.Property(k => k.Klantnummer)
                .IsRequired();

            this.Property(k => k.Klantnummer)
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_Klantnr") { IsUnique = true }));

            this.Property(k => k.Telefoonnummer)
                .HasMaxLength(300);

        }
    }
}
