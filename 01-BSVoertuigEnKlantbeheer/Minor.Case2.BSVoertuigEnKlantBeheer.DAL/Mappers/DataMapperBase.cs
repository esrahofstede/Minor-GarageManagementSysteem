using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public abstract class DataMapperBase<T, Key, Context> : IDataMapper<T, Key>
                   where Context : DbContext, new()
    {
        protected abstract IQueryable<T> GetCollection(Context context);
        /// <summary>
        /// Return all entities in the database
        /// </summary>
        /// <returns>IEnumerable of type T</returns>
        public virtual IEnumerable<T> FindAll()
        {
            using (var context = new Context())
            {
                return GetCollection(context).ToList();
            }
        }

        /// <summary>
        /// Return all entities in the database filtered by a LINQ expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>IEnumerable of type T</returns>
        public virtual IEnumerable<T> FindAllBy(Expression<Func<T, bool>> filter)
        {
            using (var context = new Context())
            {
                return GetCollection(context).Where(filter).ToList();
            }
        }

        /// <summary>
        /// Return an entity by its ID and throws an exception if there is not exactly one entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        public T FindByID(Key id)
        {
            using (var context = new Context())
            {
                return Find(id, context);
            }
        }
        protected abstract T Find(Key id, Context context);


        public abstract void Insert(T item);
        public abstract void Update(T item);
        public abstract void Delete(T item);
    }
}
