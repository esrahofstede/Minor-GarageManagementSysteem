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
        public string Kenteken { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public IEnumerable<SelectListItem> Voertuigen { get; set; }
        public string SelectedKenteken { get; set; }
        public bool Exist { get; set; }
    }
}