using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcRestScaffolding.DAL
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        T Get(long id);
        void Save();
        T Add(T t);
        void Delete(T t);
    }
}
