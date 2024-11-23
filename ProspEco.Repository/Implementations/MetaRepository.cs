using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProspEco.Database.Contexts;

namespace ProspEco.Repository.Implementations
{
    public class MetaRepository : Repository<Meta>, IMetaRepository
    {
        public MetaRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Meta>> GetByUsuarioIdAsync(long usuarioId)
        {
            return await _dbSet.Where(m => m.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<Meta> GetActiveMetaAsync(long usuarioId)
        {
            return await _dbSet
                .Where(m => m.UsuarioId == usuarioId && !m.Atingida && m.DataInicio <= DateTime.Now && m.DataFim >= DateTime.Now)
                .FirstOrDefaultAsync();
        }
    }
}