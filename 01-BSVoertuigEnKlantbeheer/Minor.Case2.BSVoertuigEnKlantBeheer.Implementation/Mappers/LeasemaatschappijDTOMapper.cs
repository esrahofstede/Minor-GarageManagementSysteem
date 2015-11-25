using System;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    /// <summary>
    /// Maps DTO to Entity and vice versa, returns ArgumentNullException when paramter is null
    /// </summary>
    [CLSCompliant(false)]
    public static class LeasemaatschappijDTOMapper
    {
        public static Entities.Leasemaatschappij MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto", "dto is null");
            }
            Entities.Leasemaatschappij entity = new Entities.Leasemaatschappij
            {
                ID = dto.ID,
                Klantnummer = dto.Klantnummer,
                Naam = dto.Naam,
                Telefoonnummer = dto.Telefoonnummer,
            };
            return entity;
        }

        public static BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij MapEntityToDTO(Entities.Leasemaatschappij entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is null");
            }
            BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij dto = new BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij
            {
                ID = entity.ID,
                Klantnummer = entity.Klantnummer,
                Naam = entity.Naam,
                Telefoonnummer = entity.Telefoonnummer,
            };
            return dto;
        }
    }
}
