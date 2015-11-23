using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Entities
{
    public class Onderhoudswerkzaamheden
    {
        public long ID { get; set; }
        public DateTime Afmeldingsdatum { get; set; }
        public long Kilometerstand { get; set; }
        public string Omschrijving { get; set; }

        public virtual Onderhoudsopdracht Onderhoudsopdracht { get; set; }

    }
}
