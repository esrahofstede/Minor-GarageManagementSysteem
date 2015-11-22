using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Entities
{
    public class Onderhoudsopdracht
    {
        public long ID { get; set; }
        public DateTime Aanmeldingsdatum { get; set; }
        public long Kilometerstand { get; set; }
        public string Onderhoudsomschrijving { get; set; }
        public bool APK { get; set; }

        public virtual Voertuig Voertuig { get; set; }
        public virtual Onderhoudswerkzaamheden Onderhoudswerkzaamheden { get; set; }
    }
}
