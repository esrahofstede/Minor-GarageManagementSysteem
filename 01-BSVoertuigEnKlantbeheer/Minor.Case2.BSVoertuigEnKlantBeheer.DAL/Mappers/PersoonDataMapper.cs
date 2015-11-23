using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class PersoonDataMapper : DataMapperBase<Persoon, long, KlantContext>
    {
        protected override IQueryable<Persoon> GetCollection(KlantContext context)
        {
            return context.Klanten.OfType<Persoon>();
        }

        protected override Persoon Find(long id, KlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override long Insert(Persoon persoon)
        {
            using (var context = new KlantContext())
            {
                context.Klanten.Add(persoon);
                context.SaveChanges();
                return persoon.ID;
            }
        }

        public override void Update(Persoon item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Persoon item)
        {
            throw new NotImplementedException();
        }
    }
}
