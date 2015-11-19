using Minor.Case2.ISRDW.DAL.Entities;
using Minor.Case2.ISRDW.DAL.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.DAL
{
    public class RDWContext : DbContext
    {
        public RDWContext() : base("RDWDB"){ }

        public DbSet<Logging> Logs { get; set; }
        public DbSet<Keuringsverzoek> Keuringsverzoek { get; set; }
        public DbSet<Keuringsregistratie> Keuringsregistratie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if(modelBuilder != null)
            {
                modelBuilder.Configurations.Add(new LoggingMapping());
                modelBuilder.Configurations.Add(new KeuringsverzoekMapping());
                modelBuilder.Configurations.Add(new KeuringsregistratieMapping());
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
