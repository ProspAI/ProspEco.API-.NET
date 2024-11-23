using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProspEco.Database.Contexts;

namespace ProspEco.Repository.Implementations
{
    public class AparelhoRepository : Repository<Aparelho>, IAparelhoRepository
    {
        public AparelhoRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Aparelho>> GetByUsuarioIdAsync(long usuarioId)
        {
            return await _dbSet.Where(a => a.UsuarioId == usuarioId).ToListAsync();
        }
    }
}