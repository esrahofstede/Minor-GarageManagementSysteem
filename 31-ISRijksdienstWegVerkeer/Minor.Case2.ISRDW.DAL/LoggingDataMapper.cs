using Minor.Case2.ISRDW.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Minor.Case2.ISRDW.DAL
{
    /// <summary>
    /// Logging persistence
    /// </summary>
    public class LoggingDataMapper : IDataMapper<Logging, long>
    {

        /// <summary>
        /// Find all logging object in the database including the Keuringsverzoek and Keuringsregistratie
        /// </summary>
        /// <returns>All logging objects including Keuringsverzoek and Keuringsregistratie</returns>
        public IEnumerable<Logging> FindAll()
        {
            using (var context = new RDWContext())
            {
                return context.Logs
                    .Include(log => log.Keuringsverzoek)
                    .Include(log => log.Keuringsregistratie)
                    .ToList();
            }
        }

        /// <summary>
        /// Insert a logging object into the database
        /// </summary>
        /// <param name="item">Logging object</param>
        public void Insert(Logging item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Logging item cannot be null");
            }

            if (item.Keuringsregistratie == null && item.Keuringsverzoek == null)
            {
                throw new ArgumentException("Keuringsregistratie and Keuringsverzoek cannot both be null");
            }

            if(item.Time == DateTime.MinValue)
            {
                throw new ArgumentException("Time has to be specified when inserting a log");
            }

            using (var context = new RDWContext())
            {
                context.Logs.Add(item);
                context.SaveChanges();
            }
        }
    }
}