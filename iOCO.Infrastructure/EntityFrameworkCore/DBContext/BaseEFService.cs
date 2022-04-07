using iOCO.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iOCO.Infrastructure.EntityFrameworkCore.DBContext
{
    public class BaseEFService
    {
        protected IOCODbContext _dbContext;
        private bool disposed = false;

        public Dictionary<Type, object> Repositories { get; set; } = new Dictionary<Type, object>();

        #region Consturctor
        public BaseEFService()
        {
            _dbContext = new IOCODbContext();

        }

        #endregion

        #region Public Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Protected Methods

        protected IGenericEfRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as GenericEfRepository<T>;
            }

            IGenericEfRepository<T> repo = new GenericEfRepository<T>(_dbContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        #endregion
    }
}
