using BeymenCodeCase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Add(T t);
        T Update(T t);
        IList<T> GetList();
        void Delete(T t);
    }
}
