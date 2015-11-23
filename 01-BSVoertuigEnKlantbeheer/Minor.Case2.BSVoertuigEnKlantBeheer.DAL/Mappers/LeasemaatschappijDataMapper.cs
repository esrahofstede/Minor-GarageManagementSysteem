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
    public class LeasemaatschappijDataMapper : DataMapperBase<Leasemaatschappij, long, KlantContext>
    {
        protected override IQueryable<Leasemaatschappij> GetCollection(KlantContext context)
        {
            return context.Klanten.OfType<Leasemaatschappij>();
        }

        protected override Leasemaatschappij Find(long id, KlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override long Insert(Leasemaatschappij leasemaatschappij)
        {
            using (var context = new KlantContext())
            {
                context.Klanten.Add(leasemaatschappij);
                context.SaveChanges();
                return leasemaatschappij.ID;
            }
        }

        public override void Update(Leasemaatschappij item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Leasemaatschappij item)
        {
            throw new NotImplementedException();
        }
    }
}
