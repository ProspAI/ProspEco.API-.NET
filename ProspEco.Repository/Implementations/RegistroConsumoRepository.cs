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
    public class RegistroConsumoRepository : Repository<RegistroConsumo>, IRegistroConsumoRepository
    {
        public RegistroConsumoRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RegistroConsumo>> GetByAparelhoIdAsync(long aparelhoId)
        {
            return await _dbSet.Where(rc => rc.AparelhoId == aparelhoId).ToListAsync();
        }

        public async Task<IEnumerable<RegistroConsumo>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet.Where(rc => rc.DataHora >= startDate && rc.DataHora <= endDate).ToListAsync();
        }
    }
}