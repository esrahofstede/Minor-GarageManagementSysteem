using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.FEGMS.Client.ViewModel
{
    public class InsertKlantgegevensVM
    {
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Achternaam { get; set; }
        public string Adres { get; set; }
        [RegularExpression("^[1-9][0-9]{3} ?[A-Z]{2}$", ErrorMessage = "Het formaat van een postcode is: 1234AB of 1234 AB")]
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Telefoonnummer { get; set; }
        [EmailAddress(ErrorMessage = "Dit is geen geldig emailadres")]
        public string Emailadres { get; set; }
        public bool Lease { get; set; }
    }
}
