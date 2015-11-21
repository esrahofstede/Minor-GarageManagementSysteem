using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappings
{
    public class LeasemaatschappijMapping : EntityTypeConfiguration<Leasemaatschappij>
    {
        public LeasemaatschappijMapping()
        {
            this.ToTable("Leasemaatschappij");

            this.Property(l => l.Naam).HasMaxLength(300);
        }
    }
}
