using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class VoertuigDataMapper : DataMapperBase<Voertuig, long, VoertuigEnKlantContext>
    {
        protected override IQueryable<Voertuig> GetCollection(VoertuigEnKlantContext context)
        {
            return context.Voertuigen
                .Include(v => v.Bestuurder)
                .Include(v => v.Eigenaar)
                .Include(v => v.OnderhoudsOpdrachten);
        }

        protected override Voertuig Find(long id, VoertuigEnKlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(Voertuig voertuig)
        {
            using (var context = new VoertuigEnKlantContext())
            {
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
