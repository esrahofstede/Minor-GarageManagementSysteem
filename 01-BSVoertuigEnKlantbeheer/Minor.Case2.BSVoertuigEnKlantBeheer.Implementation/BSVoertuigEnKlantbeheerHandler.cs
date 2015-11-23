﻿using Minor.Case2.BSVoertuigEnKlantBeheer.Contract;
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
        IDataMapper<Entities.Persoon, long>  _persoonDataMapper;
        IDataMapper<Entities.Leasemaatschappij, long> _leaseDataMapper;
        IDataMapper<Entities.Voertuig, long> _voertuigDataMapper;

        public BSVoertuigEnKlantbeheerHandler()
        {
            _persoonDataMapper = new PersoonDataMapper();
            _leaseDataMapper = new LeasemaatschappijDataMapper();
            _voertuigDataMapper = new VoertuigDataMapper();
        }

        /// <summary>
        /// Creates instances of the dataMappers to inject with IDataMappers
        /// </summary>
        /// <param name="persoonDataMapper"></param>
        /// <param name="leaseDataMapper"></param>
        /// <param name="voertuigDataMapper"></param>
        public BSVoertuigEnKlantbeheerHandler(IDataMapper<Entities.Persoon, long> persoonDataMapper, 
                                              IDataMapper<Entities.Leasemaatschappij, long> leaseDataMapper, 
                                              IDataMapper<Entities.Voertuig, long> voertuigDataMapper)
        {
            _persoonDataMapper = persoonDataMapper;
            _leaseDataMapper = leaseDataMapper;
            _voertuigDataMapper = voertuigDataMapper;
        }

        /// <summary>
        /// Get all Personen and Leasemaatschappijen from the database
        /// </summary>
        /// <returns>KlantenCollection of Personen and Leasemaatschappijen</returns>
        public KlantenCollection GetAllKlanten()
        {
            KlantenCollection klantenCollection = new KlantenCollection();

            foreach (var persoonEntity in _persoonDataMapper.FindAll())
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

        /// <summary>
        /// Find voertuigen by search criteria defind in the VoertuigenSearchCriteria
        /// </summary>
        /// <param name="zoekCriteria"></param>
        /// <returns></returns>
        public VoertuigCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new onderhoudsopdracht to the database
        /// </summary>
        /// <param name="onderhoudsopdracht"></param>
        public void VoegOnderhoudsopdrachtToe(Onderhoudsopdracht onderhoudsopdracht)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Add a new voertuig to the database with new klant and leasemaatschappij
        /// </summary>
        /// <param name="voertuig"></param>
        public void VoegVoertuigMetKlantToe(Voertuig voertuig)
        {
            Entities.Voertuig v = VoertuigDTOMapper.MapDTOToEntity(voertuig);
            long bestuurderID = -1;

            Random rnd = new Random();

            //bestuurder is always a persoon
            if(v.Bestuurder != null)
            {
                v.Bestuurder.Klantnummer = rnd.Next(100000, 999999); //sorry
                bestuurderID = _persoonDataMapper.Insert(v.Bestuurder);
                v.Bestuurder.ID = bestuurderID;

            }
            //eigenaar is a persoon or a leasemaatschappij
            if (v.Eigenaar != null)
            {
                //persoon is already inserted into the database, we only have to check for a leasemaatschappij
                if (v.Eigenaar.GetType() == typeof(Entities.Leasemaatschappij))
                {
                    v.Eigenaar.Klantnummer = rnd.Next(100000, 999999); //sorry
                    v.Eigenaar.ID = _leaseDataMapper.Insert((Entities.Leasemaatschappij)v.Eigenaar);
                }
                else
                {
                    v.Eigenaar.ID = bestuurderID;
                }
            }
            _voertuigDataMapper.Insert(v);
        }
    }
}
