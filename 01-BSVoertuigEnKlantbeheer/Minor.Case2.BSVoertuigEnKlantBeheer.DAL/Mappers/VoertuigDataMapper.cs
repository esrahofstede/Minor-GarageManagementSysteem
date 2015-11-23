using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class VoertuigDataMapper : DataMapperBase<Voertuig, long, VoertuigContext>
    {
        protected override IQueryable<Voertuig> GetCollection(VoertuigContext context)
        {
            return context.Voertuigen
                .Include(v => v.OnderhoudsOpdrachten);
        }

        public override IEnumerable<Voertuig> FindAll()
        {
            using (var context = new VoertuigContext())
            {
                List<Voertuig> voertuigen = GetCollection(context).ToList();
                foreach (var voertuig in voertuigen)
                {
                    voertuig.Bestuurder = AddKlantenToVoertuig(voertuig).Bestuurder;
                    voertuig.Eigenaar = AddKlantenToVoertuig(voertuig).Eigenaar;
                }
                return voertuigen;
            }
        }

        public override IEnumerable<Voertuig> FindAllBy(Expression<Func<Voertuig, bool>> filter)
        {
            using (var context = new VoertuigContext())
            {
                List<Voertuig> voertuigen = GetCollection(context).Where(filter).ToList();
                foreach (var voertuig in voertuigen)
                {
                    voertuig.Bestuurder = AddKlantenToVoertuig(voertuig).Bestuurder;
                    voertuig.Eigenaar = AddKlantenToVoertuig(voertuig).Eigenaar;
                }
                return voertuigen;
            }
        }

        protected override Voertuig Find(long id, VoertuigContext context)
        {
            Voertuig voertuig = GetCollection(context).Where(p => p.ID == id).Single();
            return AddKlantenToVoertuig(voertuig);

        }

        private Voertuig AddKlantenToVoertuig(Voertuig voertuig)
        {
            if (voertuig.BestuurderKlantnummer != 0)
            {
                voertuig.Bestuurder = new PersoonDataMapper().FindAllBy(k => k.Klantnummer == voertuig.BestuurderKlantnummer).SingleOrDefault();
            }
            if (voertuig.EigenaarKlantnummer != 0)
            {
                voertuig.Eigenaar = new KlantDataMapper().FindAllBy(k => k.Klantnummer == voertuig.EigenaarKlantnummer).SingleOrDefault();
            }
            return voertuig;
        }

        public override void Insert(Voertuig voertuig)
        {
            using (var context = new VoertuigContext())
            {
                if (voertuig.Bestuurder != null)
                {
                    Persoon persoon = new PersoonDataMapper().FindAllBy(k => k.Klantnummer == voertuig.Bestuurder.Klantnummer).SingleOrDefault();
                    if (persoon != null)
                    {
                        voertuig.BestuurderKlantnummer = persoon.Klantnummer;
                    }
                }
                if (voertuig.Eigenaar != null)
                {
                    Klant klant = new KlantDataMapper().FindAllBy(k => k.Klantnummer == voertuig.Eigenaar.Klantnummer).SingleOrDefault();
                    if (klant != null)
                    {
                        voertuig.EigenaarKlantnummer = klant.Klantnummer;
                    }
                }
                context.Voertuigen.Add(voertuig);
                context.SaveChanges();
            }
        }

        public override void Update(Voertuig item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Voertuig item)
        {
            throw new NotImplementedException();
        }
    }
}
