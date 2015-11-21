using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Entities
{
    public class Voertuig
    {
        public long ID { get; set; }
        public string Kenteken { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }

        public virtual Klant Eigenaar { get; set; }
        public virtual Persoon Bestuurder { get; set; }
        public virtual ICollection<OnderhoudsOpdracht> OnderhoudsOpdrachten { get; set; }
    }
}
