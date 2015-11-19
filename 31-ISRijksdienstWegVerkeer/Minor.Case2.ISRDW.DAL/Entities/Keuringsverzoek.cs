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
    public class Keuringsverzoek
    {
        public long ID { get; set; }
        public string CorrelatieId { get; set; }
        public string VoertuigType { get; set; }
        public string Kenteken { get; set; }
        public string NaamEigenaar { get; set; }
        public DateTime Keuringsdatum { get; set; }
        public string KeuringsinstantieType { get; set; }
        public string KVK { get; set; }
        public string KeuringsinstantieNaam { get; set; }
        public string KeuringsinstantiePlaats { get; set; }
    }
}
