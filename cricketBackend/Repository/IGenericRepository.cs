using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(object id);
        Task<EntityEntry<T>> Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        IEnumerable<T> GetByPredicate(Func<T, bool> predicate);
        Task Save();
    }
}
