//using minorcase2bsvoertuigenklantbeheer.v1.schema;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    public class PersoonDTOMapper
    {
        public static Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Persoon MapDTOToEntity(Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Persoon dto)
        {
            Persoon entity = new Persoon
            {
                ID = dto.id,
                Klantnummer = dto.klantnummer,
                Voornaam = dto.Voornaam,
                Tussenvoegsel = dto.Tussenvoegsel,
                Achternaam = dto.Achternaam,
                Adres = dto.Adres,
                Postcode = dto.Postcode,
                Emailadres = dto.Emailadres, 
                Telefoonnummer = dto.Telefoonnummer,
                Woonplaats = dto.Woonplaats
            };
            return entity;
        }

        public static Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Persoon MapEntityToDTO(Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Persoon entity)
        {
            Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Persoon dto = new Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Persoon
            {
                id = entity.ID,
                klantnummer = entity.Klantnummer,
                Voornaam = entity.Voornaam,
                Tussenvoegsel = entity.Tussenvoegsel,
                Achternaam = entity.Achternaam,
                Adres = entity.Adres,
                Postcode = entity.Postcode,
                Emailadres = entity.Emailadres,
                Telefoonnummer = entity.Telefoonnummer,
                Woonplaats = entity.Woonplaats
            };
            return dto;
        }
    }
}
