using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.DAL.Entities.Mapping
{
    public class LoggingMapping : EntityTypeConfiguration<Logging>
    {
        public LoggingMapping()
        {
            this.ToTable("Logging");
        }
    }
}
