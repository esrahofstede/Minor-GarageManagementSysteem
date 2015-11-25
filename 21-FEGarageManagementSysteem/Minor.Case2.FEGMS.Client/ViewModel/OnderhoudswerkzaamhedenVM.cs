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
        [Required(ErrorMessage = "{0} moet worden ingevoerd om de onderhoudsopdracht te kunnen inzien")]
        public string Kenteken { get; set; }
        public string Message { get; set; }
        [DataType(DataType.Date)]
        public DateTime Afmeldingsdatum { get; set; }
        public Onderhoudsopdracht Onderhoudsopdracht { get; set; }
        public string Onderhoudsomschrijving { get; set; }
        public long Kilometerstand { get; set; }
        public bool Steekproef { get; set; }
        public long OnderhoudsopdrachtID { get; set; }
    }
}