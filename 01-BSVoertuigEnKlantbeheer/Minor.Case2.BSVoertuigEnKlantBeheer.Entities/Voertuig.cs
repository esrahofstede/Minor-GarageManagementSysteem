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
        public string Status { get; set; }
        public Klant Eigenaar { get; set; }
        public Persoon Bestuurder { get; set; }
        public long EigenaarID { get; set; }
        public long BestuurderID { get; set; }
        public virtual ICollection<Onderhoudsopdracht> OnderhoudsOpdrachten { get; set; }
    }
}
