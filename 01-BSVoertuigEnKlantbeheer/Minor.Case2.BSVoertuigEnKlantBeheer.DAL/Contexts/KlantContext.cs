using Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappings;
using Minor.Case2.BSVoertuigEnKlantBeheer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Contexts
{
    public class KlantContext : DbContext
    {
        public KlantContext() : base("BSKlantBeheerDB")
        {
        }

        public DbSet<Klant> Klanten { get; set; }

        /// <summary>
        /// Add MappingObjects to the DbModelBuilder
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Klant>(new KlantMapping());
            modelBuilder.Configurations.Add<Persoon>(new PersoonMapping());
            modelBuilder.Configurations.Add<Leasemaatschappij>(new LeasemaatschappijMapping());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Override the SaveChanges method to catch the DbEntityValidationException
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}
