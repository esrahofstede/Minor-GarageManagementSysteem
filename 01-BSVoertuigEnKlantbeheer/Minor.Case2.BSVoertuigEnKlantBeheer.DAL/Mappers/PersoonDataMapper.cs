using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class PersoonDataMapper : DataMapperBase<Persoon, long, VoertuigEnKlantContext>
    {
        protected override IQueryable<Persoon> GetCollection(VoertuigEnKlantContext context)
        {
            return context.Klanten.Include(k => k.Voertuigen).OfType<Persoon>();
        }

        protected override Persoon Find(long id, VoertuigEnKlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(Persoon persoon)
        {
            using (var context = new VoertuigEnKlantContext())
            {
                context.Klanten.Add(persoon);
                context.SaveChanges();
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
