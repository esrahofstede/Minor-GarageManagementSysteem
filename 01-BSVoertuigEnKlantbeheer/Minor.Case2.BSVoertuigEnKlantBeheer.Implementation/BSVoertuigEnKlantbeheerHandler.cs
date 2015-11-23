using Minor.Case2.BSVoertuigEnKlantBeheer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Minor.Case2.BSVoertuigEnKlantbeheer.V1.Schema;
using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers;
using Minor.Case2.BSVoertuigEnKlantBeheer.Implementation.Mappers;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation
{
    public class BSVoertuigEnKlantbeheerHandler : IBSVoertuigEnKlantbeheer
    {
        /// <summary>
        /// Get All Personen and Leasemaatschappijen from the database
        /// </summary>
        /// <returns>KlantenCollection of Personen and Leasemaatschappijen</returns>
        public KlantenCollection GetAllKlanten()
        {
            KlantenCollection klantenCollection = new KlantenCollection();

            PersoonDataMapper persoonDataMapper = new PersoonDataMapper();
            foreach (var persoonEntity in persoonDataMapper.FindAll())
            {
                klantenCollection.Add(PersoonDTOMapper.MapEntityToDTO(persoonEntity));
            }

            LeasemaatschappijDataMapper leasemaatschappijDataMapper = new LeasemaatschappijDataMapper();
            foreach (var leaseEntity in leasemaatschappijDataMapper.FindAll())
            {
                klantenCollection.Add(LeasemaatschappijDTOMapper.MapEntityToDTO(leaseEntity));
            }
            return klantenCollection;
        }

        public VoertuigCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria)
        {
            throw new NotImplementedException();
        }

        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            throw new NotImplementedException();
        }

        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            Entities.Voertuig v = VoertuigDTOMapper.MapDTOToEntity(voertuig);

            PersoonDataMapper persoonDataMapper = new PersoonDataMapper();
            if(v.Bestuurder != null)
            {
                Random generator = new Random();
                v.Bestuurder.Klantnummer = (long)generator.Next(0, 1000000);
                persoonDataMapper.Insert(v.Bestuurder);
            }
            //if (v.Eigenaar != null)
            //{
            //    v.Bestuurder.Klantnummer = 99;
            //    persoonDataMapper.Insert((Entities.Persoon)v.Eigenaar);
            //}
            VoertuigDataMapper voertuigDataMapper = new VoertuigDataMapper();
            voertuigDataMapper.Insert(v);
        }
    }
}
