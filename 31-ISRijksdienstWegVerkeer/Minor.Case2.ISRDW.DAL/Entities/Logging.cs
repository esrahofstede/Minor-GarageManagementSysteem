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
    public class Logging
    {
        public int ID { get; set; }
        public Keuringsregistratie Keuringsregistratie { get; set; }
        public Keuringsverzoek Keuringsverzoek { get; set; }
        public DateTime Time { get; set; }
    }
}
