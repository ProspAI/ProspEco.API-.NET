using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProspEco.Database.Contexts;

namespace ProspEco.Repository.Implementations
{
    public class ConquistaRepository : Repository<Conquista>, IConquistaRepository
    {
        public ConquistaRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Conquista>> GetByUsuarioIdAsync(long usuarioId)
        {
            return await _dbSet.Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }
    }
}