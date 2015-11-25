using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class SearchVM
    {
        [Required(ErrorMessage = "{0} moet worden ingevoerd om de onderhoudswerkzaamheden te kunnen invoeren")]
        public string Kenteken { get; set; }
    }
}