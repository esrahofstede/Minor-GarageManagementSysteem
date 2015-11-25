using System;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    /// <summary>
    /// Maps DTO to Entity and vice versa, returns ArgumentNullException when paramter is null
    /// </summary>
    [CLSCompliant(false)]
    public static class PersoonDTOMapper
    {
        public static Entities.Persoon MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Persoon dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto", "dto is null");
            }
            Entities.Persoon entity = new Entities.Persoon
            {
                ID = dto.ID,
                Klantnummer = dto.Klantnummer,
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

        public static BSVoertuigEnKlantbeheer.V1.Schema.Persoon MapEntityToDTO(Entities.Persoon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is null");
            }
            BSVoertuigEnKlantbeheer.V1.Schema.Persoon dto = new BSVoertuigEnKlantbeheer.V1.Schema.Persoon
            {
                ID = entity.ID,
                Klantnummer = entity.Klantnummer,
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
