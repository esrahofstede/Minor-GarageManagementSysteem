using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class OnderhoudsWerkzaamhedenDataMapper : DataMapperBase<OnderhoudsWerkzaamheden, long, VoertuigEnKlantContext>
    {
        protected override IQueryable<OnderhoudsWerkzaamheden> GetCollection(VoertuigEnKlantContext context)
        {
            return context.OnderhoudsWerkzaamheden.Include(o => o.OnderhoudsOpdracht);
        }

        protected override OnderhoudsWerkzaamheden Find(long id, VoertuigEnKlantContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(OnderhoudsWerkzaamheden onderhoudsWerkzaamheden)
        {
            using (var context = new VoertuigEnKlantContext())
            {
                context.OnderhoudsWerkzaamheden.Add(onderhoudsWerkzaamheden);
                context.SaveChanges();
            }
        }

        public override void Update(OnderhoudsWerkzaamheden item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(OnderhoudsWerkzaamheden item)
        {
            throw new NotImplementedException();
        }
    }
}
