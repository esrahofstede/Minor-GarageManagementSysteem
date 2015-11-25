using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class OnderhoudswerkzaamhedenVM
    {
        [Required(ErrorMessage = "{0} is verplicht om een onderhoud werkzaamheid in te kunnen voeren")]
        public long Kilometerstand { get; set; }
        [Required(ErrorMessage = "{0} is verplicht om een onderhoud werkzaamheid in te kunnen voeren")]
        public string Onderhoudsomschrijving { get; set; }
        public long OnderhoudsopdrachtID { get; set; }
    }
}