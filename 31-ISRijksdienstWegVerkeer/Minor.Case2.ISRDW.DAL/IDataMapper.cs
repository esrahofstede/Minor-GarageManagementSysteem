using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.ISRDW.DAL
{
    /// <summary>
    /// DataMapper interface
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    /// <typeparam name="TID">Type of the ID</typeparam>
    public interface IDataMapper<T, TID>
    {
        IEnumerable<T> FindAll();
        void Insert(T item);
    }
}
