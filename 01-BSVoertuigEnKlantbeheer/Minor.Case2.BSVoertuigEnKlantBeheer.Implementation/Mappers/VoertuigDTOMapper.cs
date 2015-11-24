using System;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    [CLSCompliant(false)]
    public class VoertuigDTOMapper
    {
        public static Entities.Voertuig MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Voertuig dto)
        {
            Entities.Klant eigenaar = null;
            if (dto.Eigenaar.GetType() == typeof(BSVoertuigEnKlantbeheer.V1.Schema.Persoon))
            {
                eigenaar = PersoonDTOMapper.MapDTOToEntity((BSVoertuigEnKlantbeheer.V1.Schema.Persoon)dto.Eigenaar);
            }
            else
            {
                eigenaar = LeasemaatschappijDTOMapper.MapDTOToEntity((BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij)dto.Eigenaar);
            }

            Entities.Voertuig entity = new Entities.Voertuig
            {
                ID = dto.Id,
                Kenteken = dto.Kenteken,
                Bestuurder = PersoonDTOMapper.MapDTOToEntity(dto.Bestuurder),
                BestuurderID = PersoonDTOMapper.MapDTOToEntity(dto.Bestuurder).Klantnummer,
                Eigenaar = eigenaar,
                EigenaarID = eigenaar.Klantnummer,
                Merk = dto.Merk,
                Type = dto.Type,
                //TODO onderhoudsopdrachten
            };
            return entity;
        }

        public static BSVoertuigEnKlantbeheer.V1.Schema.Voertuig MapEntityToDTO(Entities.Voertuig entity)
        {
            BSVoertuigEnKlantbeheer.V1.Schema.Klant eigenaar = null;
            if (entity.Eigenaar.GetType() == typeof(Entities.Persoon))
            {
                eigenaar = PersoonDTOMapper.MapEntityToDTO((Entities.Persoon)entity.Eigenaar);
            }
            else
            {
                eigenaar = LeasemaatschappijDTOMapper.MapEntityToDTO((Entities.Leasemaatschappij)entity.Eigenaar);
            }

            BSVoertuigEnKlantbeheer.V1.Schema.Voertuig dto = new BSVoertuigEnKlantbeheer.V1.Schema.Voertuig
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
