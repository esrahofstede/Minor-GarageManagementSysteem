using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class OnderhoudsOpdrachtDataMapper : DataMapperBase<OnderhoudsOpdracht, long, VoertuigEnKlantContext>
    {
        protected override IQueryable<OnderhoudsOpdracht> GetCollection(VoertuigEnKlantContext context)
        {
            return context.OnderhoudsOpdrachten
                .Include(o => o.OnderhoudsWerkzaamheden)
                .Include(o => o.Voertuig);
        }

        protected override OnderhoudsOpdracht Find(long id, VoertuigEnKlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(OnderhoudsOpdracht onderhoudsOpdracht)
        {
            using (var context = new VoertuigEnKlantContext())
            {
                context.OnderhoudsOpdrachten.Add(onderhoudsOpdracht);
                context.SaveChanges();
            }
        }

        public override void Update(OnderhoudsOpdracht item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(OnderhoudsOpdracht item)
        {
            throw new NotImplementedException();
        }
    }
}
