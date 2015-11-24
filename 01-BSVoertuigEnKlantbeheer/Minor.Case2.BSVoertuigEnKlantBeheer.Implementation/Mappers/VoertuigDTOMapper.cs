using System;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers
{
    [CLSCompliant(false)]
    public static class VoertuigDTOMapper
    {
        public static Entities.Voertuig MapDTOToEntity(BSVoertuigEnKlantbeheer.V1.Schema.Voertuig dto)
        {
            Entities.Klant eigenaar = null;
            Entities.Persoon bestuurder = null;

            if (dto.Eigenaar != null)
            {
                if (dto.Eigenaar.GetType() == typeof(BSVoertuigEnKlantbeheer.V1.Schema.Persoon))
                {
                    eigenaar = PersoonDTOMapper.MapDTOToEntity((BSVoertuigEnKlantbeheer.V1.Schema.Persoon)dto.Eigenaar);
                }
                else
                {
                    eigenaar = LeasemaatschappijDTOMapper.MapDTOToEntity((BSVoertuigEnKlantbeheer.V1.Schema.Leasemaatschappij)dto.Eigenaar);
                }
            }
            if (dto.Bestuurder != null)
            {
                bestuurder = PersoonDTOMapper.MapDTOToEntity(dto.Bestuurder);
            }


            Entities.Voertuig entity = new Entities.Voertuig
            {
                ID = dto.ID,
                Kenteken = dto.Kenteken,
                Bestuurder = bestuurder,
                BestuurderID = bestuurder?.Klantnummer ?? 0,
                Eigenaar = eigenaar,
                EigenaarID = eigenaar?.Klantnummer ?? 0,
                Merk = dto.Merk,
                Type = dto.Type,
                Status = dto.Status,
            };
            return entity;
        }

        public static BSVoertuigEnKlantbeheer.V1.Schema.Voertuig MapEntityToDTO(Entities.Voertuig entity)
        {
            BSVoertuigEnKlantbeheer.V1.Schema.Klant eigenaar = null;
            BSVoertuigEnKlantbeheer.V1.Schema.Persoon bestuurder = null;

            if (entity.Eigenaar != null)
            {
                if (entity.Eigenaar.GetType() == typeof(Entities.Persoon))
                {
                    eigenaar = PersoonDTOMapper.MapEntityToDTO((Entities.Persoon)entity.Eigenaar);
                }
                else
                {
                    eigenaar = LeasemaatschappijDTOMapper.MapEntityToDTO((Entities.Leasemaatschappij)entity.Eigenaar);
                }
            }
            if(entity.Bestuurder != null)
            {
                bestuurder = PersoonDTOMapper.MapEntityToDTO(entity.Bestuurder);
            }

            BSVoertuigEnKlantbeheer.V1.Schema.Voertuig dto = new BSVoertuigEnKlantbeheer.V1.Schema.Voertuig
            {
                ID = entity.ID,
                Kenteken = entity.Kenteken,
                Bestuurder = bestuurder,
                Eigenaar = eigenaar,
                Merk = entity.Merk,
                Type = entity.Type,
                Status = entity.Status,
            };
            return dto;
        }
    }
}
