using Minor.Case2.ISRDW.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Minor.Case2.ISRDW.DAL.Tests
{
    public class RDWDatabaseInitializer : DropCreateDatabaseAlways<RDWContext>
    {
        protected override void Seed(RDWContext context)
        {
            List<Logging> logs = new List<Logging>();

            logs.Add(new Logging
            {
                Keuringsregistratie = DummyData.GetKeuringsregistratie(),
                Time = new DateTime(2015, 11, 18, 11, 32, 44),
            });

            logs.Add(new Logging
            {
                Keuringsverzoek = DummyData.GetKeuringsverzoek(),
                Time = new DateTime(2015, 11, 18, 11, 31, 16),
            });

            context.Logs.AddRange(logs);

            base.Seed(context);
        }
    }
}