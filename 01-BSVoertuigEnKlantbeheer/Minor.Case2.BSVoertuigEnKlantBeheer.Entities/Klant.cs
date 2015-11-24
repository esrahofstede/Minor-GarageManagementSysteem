using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Entities
{
    public abstract class Klant
    {
        public long ID { get; set; }
        public long Klantnummer { get; set; }
        public string Telefoonnummer { get; set; }

    }
}
