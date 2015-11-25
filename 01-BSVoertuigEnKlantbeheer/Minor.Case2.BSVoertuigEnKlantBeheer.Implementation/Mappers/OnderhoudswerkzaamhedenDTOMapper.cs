using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    /// <summary>
    /// Maps DTO to Entity and vice versa, returns ArgumentNullException when paramter is null
    /// </summary>
    [CLSCompliant(false)]
    public static class OnderhoudswerkzaamhedenDTOMapper
    {
        public static Entities.Onderhoudswerkzaamheden MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudswerkzaamheden dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto", "dto is null");
            }
            Entities.Onderhoudswerkzaamheden entity = new Entities.Onderhoudswerkzaamheden
            {
                ID = dto.ID,
                Afmeldingsdatum = dto.Afmeldingsdatum,
                Kilometerstand = dto.Kilometerstand,
                Omschrijving = dto.Onderhoudswerkzaamhedenomschrijving,
                Onderhoudsopdracht = OnderhoudsOpdrachtDTOMapper.MapDTOToEntity(dto.Onderhoudsopdracht),
            };

            return entity;
        }

        public static BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudswerkzaamheden MapEntityToDTO(Entities.Onderhoudswerkzaamheden entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is null");
            }
            BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudswerkzaamheden dto = new BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudswerkzaamheden
            {
                ID = entity.ID,
                Afmeldingsdatum = entity.Afmeldingsdatum,
                Kilometerstand = entity.Kilometerstand,
                Onderhoudswerkzaamhedenomschrijving = entity.Omschrijving,
                Onderhoudsopdracht = OnderhoudsOpdrachtDTOMapper.MapEntityToDTO(entity.Onderhoudsopdracht),
            };

            return dto;
        }
    }
}
