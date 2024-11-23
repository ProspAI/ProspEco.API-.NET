using Microsoft.EntityFrameworkCore;
using ProspEco.Database;

namespace ProspEco.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProspEcoDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ProspEcoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(long id, T entity)
        {
            var existing = await _dbSet.FindAsync(id);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}