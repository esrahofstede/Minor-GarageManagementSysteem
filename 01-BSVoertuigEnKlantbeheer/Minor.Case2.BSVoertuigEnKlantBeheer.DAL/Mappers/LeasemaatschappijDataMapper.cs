using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class LeasemaatschappijDataMapper : DataMapperBase<Leasemaatschappij, long, VoertuigEnKlantContext>
    {
        protected override IQueryable<Leasemaatschappij> GetCollection(VoertuigEnKlantContext context)
        {
            return context.Klanten.Include(k => k.Voertuigen).OfType<Leasemaatschappij>();
        }

        protected override Leasemaatschappij Find(long id, VoertuigEnKlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(Leasemaatschappij leasemaatschappij)
        {
            using (var context = new VoertuigEnKlantContext())
            {
                context.Klanten.Add(leasemaatschappij);
                context.SaveChanges();
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
