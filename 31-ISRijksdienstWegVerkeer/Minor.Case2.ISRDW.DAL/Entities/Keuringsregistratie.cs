using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.DAL.Entities
{
    /// <summary>
    /// Logging object
    /// </summary>
    public class Keuringsregistratie
    {
        public long ID { get; set; }
        public string CorrelatieId { get; set; }
        public string Kenteken { get; set; }
        public DateTime Keuringsdatum { get; set; }
        public DateTime? Steekproef { get; set; }
    }
}
