using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.BSVoertuigEnKlantBeheer.DAL.Mappers
{
    public interface IDataMapper<T, Key>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAllBy(Expression<Func<T, bool>> filter);
        T FindByID(Key id);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
    }
}
