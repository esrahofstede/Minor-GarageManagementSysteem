using Minor.Case2.BSVoertuigenEnKlantBeheer.V1.Schema;
using Minor.Case2.FEGMS.Client.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class InsertLeasemaatschappijGegevensVM
    {
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Telefoonnummer { get; set; }
        public IEnumerable<SelectListItem> Leasemaatschappijen { get; set; }
        [Display(Name = "Bestaande leasemaatschappij")]
        public bool Exist { get; set; }
    }
}
