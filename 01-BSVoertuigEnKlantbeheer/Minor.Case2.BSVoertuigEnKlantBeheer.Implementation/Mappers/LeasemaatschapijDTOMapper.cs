using System;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    [CLSCompliant(false)]
    public class LeasemaatschappijDTOMapper
    {
        public static Entities.Leasemaatschappij MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij dto)
        {
            Entities.Leasemaatschappij entity = new Entities.Leasemaatschappij
            {
                ID = dto.ID,
                Klantnummer = dto.Klantnummer,
                Naam = dto.Naam,
                //Adres = dto.Adres,
                //Postcode = dto.Postcode,
                //Emailadres = dto.Emailadres,
                Telefoonnummer = dto.Telefoonnummer,
                //Woonplaats = dto.Woonplaats
            };
            return entity;
        }

        public static BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij MapEntityToDTO(Entities.Leasemaatschappij entity)
        {
            BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij dto = new BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij
            {
                ID = entity.ID,
                Klantnummer = entity.Klantnummer,
                Naam = entity.Naam,
                //Adres = entity.Adres,
                //Postcode = entity.Postcode,
                //Emailadres = entity.Emailadres,
                Telefoonnummer = entity.Telefoonnummer,
                //Woonplaats = entity.Woonplaats
            };
            return dto;
        }
    }
}
