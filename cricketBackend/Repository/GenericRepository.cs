using cricketBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DataContext _context;

        private DbSet<T> table;
        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task Delete(object id)
        {
            var existing = await table.FindAsync(id);
            if (existing != null)
            {
                table.Remove(existing);

            }
            //This will mark the Entity State as Deleted

        }

        public async Task<List<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            var entity = await table.FindAsync(id);
            return entity;
        }

        public IEnumerable<T> GetByPredicate(Func<T, bool> predicate)
        {
            return table.AsQueryable<T>().Where(predicate);
        }

        public async Task<EntityEntry<T>> Insert(T obj)
        {
            var entity = await table.AddAsync(obj);
            return entity;
        }

        public async Task Save()
        {
            _context.SaveChanges();
        }

        public async Task Update(T obj)
        {
            //First attach the object to the table
            table.Attach(obj);
            //Then set the state of the Entity as Modified
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
