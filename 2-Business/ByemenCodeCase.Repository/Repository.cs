using BeymenCodeCase.Context;
using BeymenCodeCase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly StorageContext storageContext;
        private readonly DbSet<T> dbset;

        public Repository(StorageContext storageContext)
        {
            this.storageContext = storageContext;
            dbset = storageContext.Set<T>();
        }
        public T Add(T t)
        {
            dbset.Add(t);
            return t;
        }

        public void Delete(T t)
        {
            dbset.Remove(t);
        }

        public IList<T> GetList()
        {
            return dbset.ToList();
        }

        public T Update(T t)
        {
            dbset.Attach(t);
            storageContext.Entry(t).State = EntityState.Modified;
            return t;
        }
    }
}
