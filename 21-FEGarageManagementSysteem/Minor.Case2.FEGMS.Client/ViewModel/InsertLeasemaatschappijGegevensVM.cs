using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class InsertLeasemaatschappijGegevensVM
    {
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Telefoonnummer { get; set; }
    }
}
