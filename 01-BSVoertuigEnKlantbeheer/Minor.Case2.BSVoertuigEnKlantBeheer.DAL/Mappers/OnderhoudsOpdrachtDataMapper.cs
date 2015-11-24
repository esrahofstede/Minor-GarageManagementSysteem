﻿using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public class OnderhoudsOpdrachtDataMapper : DataMapperBase<Onderhoudsopdracht, long, VoertuigContext>
    {
        protected override IQueryable<Onderhoudsopdracht> GetCollection(VoertuigContext context)
        {
            return context.OnderhoudsOpdrachten
                .Include(o => o.Onderhoudswerkzaamheden)
                .Include(o => o.Voertuig);
        }

        protected override Onderhoudsopdracht Find(long id, VoertuigContext context)
        {
            return GetCollection(context).Where(p => p.ID == id).Single();
        }

        public override long Insert(Onderhoudsopdracht onderhoudsOpdracht)
        {
            using (var context = new VoertuigContext())
            {
                if(onderhoudsOpdracht.Voertuig != null)
                {
                    Voertuig voertuig = context.Voertuigen.Where(v => v.Kenteken == onderhoudsOpdracht.Voertuig.Kenteken).SingleOrDefault();
                    if(voertuig != null)
                    {
                        onderhoudsOpdracht.Voertuig = voertuig;
                    }
                }
                context.OnderhoudsOpdrachten.Add(onderhoudsOpdracht);
                context.SaveChanges();
                return onderhoudsOpdracht.ID;
            }
        }

        public override void Update(Onderhoudsopdracht item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Onderhoudsopdracht item)
        {
            throw new NotImplementedException();
        }
    }
}
