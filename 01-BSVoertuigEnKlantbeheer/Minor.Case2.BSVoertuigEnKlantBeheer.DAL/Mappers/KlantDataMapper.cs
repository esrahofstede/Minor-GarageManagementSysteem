using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class KlantDataMapper : DataMapperBase<Klant, long, KlantContext>
    {
        protected override IQueryable<Klant> GetCollection(KlantContext context)
        {
            return context.Klanten;
        }

        protected override Klant Find(long id, KlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(Klant klant)
        {
            using (var context = new KlantContext())
            {
                context.Klanten.Add(klant);
                context.SaveChanges();
            }
        }

        public override void Update(Klant item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Klant item)
        {
            throw new NotImplementedException();
        }
    }
}
