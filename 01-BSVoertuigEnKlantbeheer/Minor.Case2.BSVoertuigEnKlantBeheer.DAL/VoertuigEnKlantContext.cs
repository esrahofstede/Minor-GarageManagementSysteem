using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappings;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL
{
    public class VoertuigEnKlantContext : DbContext
    {
        public VoertuigEnKlantContext() : base("BSVoertuigEnKlantBeheerTestDB")
        {

        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Voertuig> Voertuigen { get; set; }
        public DbSet<OnderhoudsOpdracht> OnderhoudsOpdrachten { get; set; }
        public DbSet<OnderhoudsWerkzaamheden> OnderhoudsWerkzaamheden { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Klant>(new KlantMapping());
            modelBuilder.Configurations.Add<Persoon>(new PersoonMapping());
            modelBuilder.Configurations.Add<Leasemaatschappij>(new LeasemaatschappijMapping());
            modelBuilder.Configurations.Add<Voertuig>(new VoertuigMapping());
            modelBuilder.Configurations.Add<OnderhoudsOpdracht>(new OnderhoudsOpdrachtMapping());
            modelBuilder.Configurations.Add<OnderhoudsWerkzaamheden>(new OnderhoudsWerkzaamhedenMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
