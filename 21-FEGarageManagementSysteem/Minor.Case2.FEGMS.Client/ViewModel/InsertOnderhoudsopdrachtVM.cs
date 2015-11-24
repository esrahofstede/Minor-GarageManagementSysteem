using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class InsertOnderhoudsopdrachtVM
    {
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.Date)]
        public DateTime AanmeldingsDatum { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public int Kilometerstand { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Onderhoudsomschrijving { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public bool APK { get; set; }
    }
}