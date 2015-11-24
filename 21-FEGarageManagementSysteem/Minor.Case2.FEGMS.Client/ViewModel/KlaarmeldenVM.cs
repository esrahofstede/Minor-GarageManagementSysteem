using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class KlaarmeldenVM
    {
        [Required]
        public string Kenteken { get; set; }
        public Voertuig Voertuig { get; set; }
    }
}
