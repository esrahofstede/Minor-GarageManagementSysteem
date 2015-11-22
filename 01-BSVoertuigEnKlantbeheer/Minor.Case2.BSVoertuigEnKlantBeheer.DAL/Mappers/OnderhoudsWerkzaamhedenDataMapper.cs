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
    public class OnderhoudsWerkzaamhedenDataMapper : DataMapperBase<Onderhoudswerkzaamheden, long, VoertuigContext>
    {
        protected override IQueryable<Onderhoudswerkzaamheden> GetCollection(VoertuigContext context)
        {
            return context.OnderhoudsWerkzaamheden.Include(o => o.Onderhoudsopdracht);
        }

        protected override Onderhoudswerkzaamheden Find(long id, VoertuigContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override void Insert(Onderhoudswerkzaamheden onderhoudsWerkzaamheden)
        {
            using (var context = new VoertuigContext())
            {
                context.OnderhoudsWerkzaamheden.Add(onderhoudsWerkzaamheden);
                context.SaveChanges();
            }
        }

        public override void Update(Onderhoudswerkzaamheden item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Onderhoudswerkzaamheden item)
        {
            throw new NotImplementedException();
        }
    }
}
