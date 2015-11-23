using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    public class VoertuigDTOMapper
    {
        public static Voertuig MapDTOToEntity(Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Voertuig dto)
        {
            Klant eigenaar = null;
            if (dto.Eigenaar.GetType() == typeof(Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Persoon))
            {
                eigenaar = PersoonDTOMapper.MapDTOToEntity((Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Persoon)dto.Eigenaar);
            }
            else
            {
                eigenaar = LeasemaatschappijDTOMapper.MapDTOToEntity((Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij)dto.Eigenaar);
            }

            Voertuig entity = new Voertuig
            {
                ID = dto.Id,
                Kenteken = dto.Kenteken,
                Bestuurder = PersoonDTOMapper.MapDTOToEntity(dto.Bestuurder),
                BestuurderKlantnummer = PersoonDTOMapper.MapDTOToEntity(dto.Bestuurder).Klantnummer,
                Eigenaar = eigenaar,
                EigenaarKlantnummer = eigenaar.Klantnummer,
                Merk = dto.Merk,
                Type = dto.Type,
                //TODO onderhoudsopdrachten
            };
            return entity;
        }

        public static Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Voertuig MapEntityToDTO(Minor.Case2.BSVoertuigEnKlantBeheer.Entities.Voertuig entity)
        {
            Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Klant eigenaar = null;
            if (entity.Eigenaar.GetType() == typeof(Persoon))
            {
                eigenaar = PersoonDTOMapper.MapEntityToDTO((Persoon)entity.Eigenaar);
            }
            else
            {
                eigenaar = LeasemaatschappijDTOMapper.MapEntityToDTO((Leasemaatschappij)entity.Eigenaar);
            }

            Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Voertuig dto = new Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema.Voertuig
            {
                Id = entity.ID,
                Kenteken = entity.Kenteken,
                Bestuurder = PersoonDTOMapper.MapEntityToDTO(entity.Bestuurder),
                Eigenaar = eigenaar,
                Merk = entity.Merk,
                Type = entity.Type,
                //TODO onderhoudsopdrachten
            };
            return dto;
        }
    }
}
