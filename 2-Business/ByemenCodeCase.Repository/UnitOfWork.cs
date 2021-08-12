using BeymenCodeCase.Context;
using BeymenCodeCase.Entity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private static Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly StorageContext storageContext;
        private readonly IDbContextTransaction dbContextTransaction = null;
        private bool isDisposed = false;
        public Dictionary<Type, object> Repositories
        {
            get
            {
                return _repositories;
            }
        }

        public UnitOfWork(StorageContext storageContext)
        {
            this.storageContext = storageContext;
            dbContextTransaction = storageContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            if (!isDisposed)
                storageContext.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            object repo;
            if (!Repositories.TryGetValue(typeof(T), out repo))
                return new Repository<T>(storageContext);

            return repo as IRepository<T>;
        }

        public void SaveChanges()
        {
            try
            {
                storageContext.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
