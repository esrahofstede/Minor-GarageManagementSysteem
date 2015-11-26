using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class InsertVoertuiggegevensVM
    {
        [StringLength(8, MinimumLength = 8, ErrorMessage = "{0} moet precies 8 tekens bevatten")]
        [Required(ErrorMessage = "{0} is een verplicht veld voor de voertuiggegevens")]
        public string Kenteken { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld voor de voertuiggegevens")]
        public string Merk { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld voor de voertuiggegevens")]
        public string Type { get; set; }
        public IEnumerable<SelectListItem> Voertuigen { get; set; }
        public long SelectedVoertuigID { get; set; }
    }
}