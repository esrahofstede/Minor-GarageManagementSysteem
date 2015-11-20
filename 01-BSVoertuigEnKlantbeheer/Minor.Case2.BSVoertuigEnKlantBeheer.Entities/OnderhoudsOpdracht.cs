using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Entities
{
    public class OnderhoudsOpdracht
    {
        public long ID { get; set; }
        public DateTime AanmeldingsDatum { get; set; }
        public long Kilometerstand { get; set; }
        public string OnderhoudsOmschrijving { get; set; }
        public bool APK { get; set; }

        public virtual Voertuig Voertuig { get; set; }
        public virtual OnderhoudsWerkzaamheden OnderhoudsWerkzaamheden { get; set; }
    }
}
