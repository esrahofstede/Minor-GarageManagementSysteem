using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    public class LeasemaatschappijDTOMapper
    {
        public static Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Leasemaatschappij MapDTOToEntity(Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij dto)
        {
            Leasemaatschappij entity = new Leasemaatschappij
            {
                ID = dto.id,
                Klantnummer = dto.klantnummer,
                Naam = dto.Naam,
                Adres = dto.Adres,
                Postcode = dto.Postcode,
                Emailadres = dto.Emailadres,
                Telefoonnummer = dto.Telefoonnummer,
                Woonplaats = dto.Woonplaats
            };
            return entity;
        }

        public static Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij MapEntityToDTO(Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Leasemaatschappij entity)
        {
            Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij dto = new Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij
            {
                id = entity.ID,
                klantnummer = entity.Klantnummer,
                Naam = entity.Naam,
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
