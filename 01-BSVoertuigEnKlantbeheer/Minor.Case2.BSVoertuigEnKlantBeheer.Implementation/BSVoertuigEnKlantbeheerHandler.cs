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
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using Minor.Case2.All.V1.Schema;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.Implementation
{
    [CLSCompliant(false)]
    public class BSVoertuigEnKlantbeheerHandler : IBSVoertuigEnKlantbeheer
    {
        IDataMapper<Entities.Persoon, long> _persoonDataMapper;
        IDataMapper<Entities.Leasemaatschappij, long> _leaseDataMapper;
        IDataMapper<Entities.Voertuig, long> _voertuigDataMapper;
        IDataMapper<Entities.Onderhoudsopdracht, long> _onderhoudsDataMapper;

        public BSVoertuigEnKlantbeheerHandler()
        {
            _persoonDataMapper = new PersoonDataMapper();
            _leaseDataMapper = new LeasemaatschappijDataMapper();
            _voertuigDataMapper = new VoertuigDataMapper();
            _onderhoudsDataMapper = new OnderhoudsOpdrachtDataMapper();
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
        /// Creates an instance of the dataMapper to inject with IDataMappers
        /// </summary>
        /// <param name="voertuigDataMapper"></param>
        public BSVoertuigEnKlantbeheerHandler(IDataMapper<Entities.Voertuig, long> voertuigDataMapper)
        {
            _voertuigDataMapper = voertuigDataMapper;
        }

        /// <summary>
        /// Get all Leasemaatschappijen from the database
        /// </summary>
        /// <returns>KlantenCollection of Leasemaatschappijen</returns>
        public KlantenCollection GetAllLeasemaatschappijen()
        {
            KlantenCollection klantenCollection = new KlantenCollection();

            foreach (var leaseEntity in _leaseDataMapper.FindAll())
            {
                klantenCollection.Add(LeasemaatschappijDTOMapper.MapEntityToDTO(leaseEntity));
            }
            return klantenCollection;
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

            foreach (var leaseEntity in _leaseDataMapper.FindAll())
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
        public VoertuigenCollection GetVoertuigBy(VoertuigenSearchCriteria zoekCriteria)
        {
            VoertuigenCollection voertuigenCollection = new VoertuigenCollection();

            IEnumerable<Entities.Voertuig> voertuigen = _voertuigDataMapper.FindAll();

            if (!string.IsNullOrEmpty(zoekCriteria.Kenteken))
            {
                voertuigen = voertuigen.Where(v => v.Kenteken == zoekCriteria.Kenteken);
            }
            if (zoekCriteria.ID > 0)
            {
                voertuigen = voertuigen.Where(v => v.ID == zoekCriteria.ID);
            }
            if (!string.IsNullOrEmpty(zoekCriteria.Merk))
            {
                voertuigen = voertuigen.Where(v => v.Merk == zoekCriteria.Merk);
            }
            if (!string.IsNullOrEmpty(zoekCriteria.Type))
            {
                voertuigen = voertuigen.Where(v => v.Type == zoekCriteria.Type);
            }
            if (zoekCriteria.Bestuurder != null && zoekCriteria.Bestuurder.ID > 0)
            {
                voertuigen = voertuigen.Where(v => v.Bestuurder.ID == zoekCriteria.Bestuurder.ID);
            }
            if (zoekCriteria.Eigenaar != null && zoekCriteria.Eigenaar.ID > 0)
            {
                voertuigen = voertuigen.Where(v => v.Eigenaar.ID == zoekCriteria.Eigenaar.ID);
            }

            foreach (var voertuigEntity in voertuigen.ToList())
            {
                voertuigenCollection.Add(VoertuigDTOMapper.MapEntityToDTO(voertuigEntity));
            }
            return voertuigenCollection;

        }

        /// <summary>
        /// Add a new voertuig to the database with new klant and leasemaatschappij
        /// </summary>
        /// <param name="voertuig"></param>
        public void VoegVoertuigMetKlantToe(BSVoertuigEnKlantbeheer.V1.Schema.Voertuig voertuig)
        {
            var list = new List<FunctionalErrorDetail>();
            Entities.Voertuig nieuwVoertuig;

            try
            {
                nieuwVoertuig = VoertuigDTOMapper.MapDTOToEntity(voertuig);
            }
            catch (ArgumentNullException)
            {
                var ex = new FunctionalErrorDetail
                {
                    ErrorCode = 404,
                    Message = "voertuig is null"
                };
                throw new FaultException<FunctionalErrorDetail[]>(new FunctionalErrorDetail[] { ex });
            }
            
            if(_voertuigDataMapper.FindAllBy(v => v.Kenteken == nieuwVoertuig.Kenteken).FirstOrDefault() != null)
            {
                //Voertuig already exist with kenteken
                list.Add(new FunctionalErrorDetail
                {
                    ErrorCode = 302,
                    Message = "voertuig with kenteken already exist in the database"
                });

            }
            else
            {
                //Voertuig doesnt exist with kenteken
                long bestuurderID = -1;

                Random rnd = new Random();

                //bestuurder is always a persoon
                if (nieuwVoertuig.Bestuurder != null)
                {
                    nieuwVoertuig.Bestuurder.Klantnummer = rnd.Next(100000, 999999); //sorry
                    bestuurderID = _persoonDataMapper.Insert(nieuwVoertuig.Bestuurder);
                    nieuwVoertuig.Bestuurder.ID = bestuurderID;

                }
                //eigenaar is a persoon or a leasemaatschappij
                if (nieuwVoertuig.Eigenaar != null)
                {
                    //persoon is already inserted into the database, we only have to check for a leasemaatschappij
                    if (nieuwVoertuig.Eigenaar.GetType() == typeof(Entities.Leasemaatschappij))
                    {
                        nieuwVoertuig.Eigenaar.Klantnummer = rnd.Next(100000, 999999); //sorry
                        nieuwVoertuig.Eigenaar.ID = _leaseDataMapper.Insert((Entities.Leasemaatschappij)nieuwVoertuig.Eigenaar);
                    }
                    else
                    {
                        nieuwVoertuig.Eigenaar.ID = bestuurderID;
                    }
                }
                _voertuigDataMapper.Insert(nieuwVoertuig);
            }

            if (list.Any())
            {
                throw new FaultException<FunctionalErrorDetail[]>(list.ToArray());
            }

        }

        /// <summary>
        /// Add a new onderhoudsopdracht to the database
        /// </summary>
        /// <param name="onderhoudsopdracht"></param>
        public void VoegOnderhoudsopdrachtToe(BSVoertuigEnKlantbeheer.V1.Schema.Onderhoudsopdracht onderhoudsopdracht)
        {
            try
            {
                Entities.Onderhoudsopdracht o = OnderhoudsOpdrachtDTOMapper.MapDTOToEntity(onderhoudsopdracht);
                _onderhoudsDataMapper.Insert(o);
            }
            catch (ArgumentNullException)
            {
                var ex = new FunctionalErrorDetail
                {
                    ErrorCode = 404,
                    Message = "onderhoudsopdracht is null"
                };
                throw new FaultException<FunctionalErrorDetail[]>(new FunctionalErrorDetail[] { ex });
            }
            
        }

        /// <summary>
        /// Get all onderhoudsopdrachten filtered by zoekCriteria, for now we only filter on kenteken from a voertuig
        /// </summary>
        /// <param name="zoekCriteria"></param>
        /// <returns></returns>
        public OnderhoudsopdrachtenCollection GetOnderhoudsopdrachtenBy(OnderhoudsopdrachtZoekCriteria zoekCriteria)
        {
            OnderhoudsopdrachtenCollection onderhoudsopdrachtenCollection = new OnderhoudsopdrachtenCollection();

            IEnumerable<Entities.Onderhoudsopdracht> onderhoudsopdrachten = _onderhoudsDataMapper.FindAll();
  
            if (zoekCriteria.Voertuig != null && !string.IsNullOrEmpty(zoekCriteria.Voertuig.Kenteken))
            {
                onderhoudsopdrachten = onderhoudsopdrachten.Where(v => v.Voertuig.Kenteken == zoekCriteria.Voertuig.Kenteken);
            }

            foreach (var onderhoudsEntity in onderhoudsopdrachten.ToList())
            {
                onderhoudsopdrachtenCollection.Add(OnderhoudsOpdrachtDTOMapper.MapEntityToDTO(onderhoudsEntity));
            }
            return onderhoudsopdrachtenCollection;
        }

        /// <summary>
        /// Update the voertuig object
        /// </summary>
        /// <param name="voertuig"></param>
        public void UpdateVoertuig(BSVoertuigEnKlantbeheer.V1.Schema.Voertuig voertuig)
        {
            try
            {
                Entities.Voertuig v = VoertuigDTOMapper.MapDTOToEntity(voertuig);
                _voertuigDataMapper.Update(v);
            }
            catch (ArgumentNullException)
            {
                var ex = new FunctionalErrorDetail
                {
                    ErrorCode = 404,
                    Message = "voertuig is null"
                };
                throw new FaultException<FunctionalErrorDetail[]>(new FunctionalErrorDetail[] { ex });
            }           
        }

    }
}
