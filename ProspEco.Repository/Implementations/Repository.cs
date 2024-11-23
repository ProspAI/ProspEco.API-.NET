using Microsoft.EntityFrameworkCore;
using ProspEco.Database.Contexts;
using ProspEco.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ProspEcoContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ProspEcoContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}