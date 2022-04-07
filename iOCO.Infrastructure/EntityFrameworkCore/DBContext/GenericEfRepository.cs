using iOCO.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iOCO.Infrastructure.EntityFrameworkCore.DBContext
{
    public class GenericEfRepository<T> : IGenericEfRepository<T> where T : class
    {
        public GenericEfRepository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException("Null DbContext");
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> ListAll() => DbSet;

        public virtual async Task<List<T>> ListAllAsync() => await DbSet.ToListAsync();

        public virtual async Task<T> GetByIdAsync(long id) => await DbSet.FindAsync(id);

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }


        public void AddRange(List<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public virtual void Delete(List<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
