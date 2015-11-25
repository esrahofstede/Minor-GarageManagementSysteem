using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    [CLSCompliant(false)]
    public static class OnderhoudsOpdrachtDTOMapper
    {
        public static Entities.Onderhoudsopdracht MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto", "dto is null");
            }

            Entities.Voertuig voertuig = null;
            if (dto.Voertuig != null) {
                voertuig = VoertuigDTOMapper.MapDTOToEntity(dto.Voertuig);
            }


            Entities.Onderhoudsopdracht entity = new Entities.Onderhoudsopdracht
            {
                ID = dto.ID,
                Aanmeldingsdatum = dto.Aanmeldingsdatum,
                APK = dto.APK,
                Kilometerstand = dto.Kilometerstand,
                Onderhoudsomschrijving = dto.Onderhoudsomschrijving,
                //TODO onderhoudswerkzaamgheden
                Voertuig = voertuig
            };

            return entity;
        }

        public static BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht MapEntityToDTO(Entities.Onderhoudsopdracht entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "entity is null");
            }
            BSVoertuigEnKlantbeheer.V1.Schema.Voertuig voertuig = null;
            if (entity.Voertuig != null)
            {
                voertuig = VoertuigDTOMapper.MapEntityToDTO(entity.Voertuig);
            }


            BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht dto = new BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht
            {
                ID = entity.ID,
                Aanmeldingsdatum = entity.Aanmeldingsdatum,
                APK = entity.APK,
                Kilometerstand = entity.Kilometerstand,
                Onderhoudsomschrijving = entity.Onderhoudsomschrijving,
                //TODO onderhoudswerkzaamgheden
                Voertuig = voertuig,
            };
            return dto;
        }
    }
}
