using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iOCO.Core.Repositories
{
    public interface IGenericEfRepository<T> where T : class
    {
        IQueryable<T> ListAll();
        Task<List<T>> ListAllAsync();
        Task<T> GetByIdAsync(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Delete(int id);
        void AddRange(List<T> entityList);
        void Delete(List<T> entities);
    }
}