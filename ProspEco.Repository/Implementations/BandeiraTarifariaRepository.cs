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
    public class BandeiraTarifariaRepository : Repository<BandeiraTarifaria>, IBandeiraTarifariaRepository
    {
        public BandeiraTarifariaRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BandeiraTarifaria>> GetByDataVigenciaAsync(DateTime dataVigencia)
        {
            return await _dbSet.Where(b => b.DataVigencia.Date == dataVigencia.Date).ToListAsync();
        }

        public async Task<BandeiraTarifaria> GetLatestAsync()
        {
            return await _dbSet.OrderByDescending(b => b.DataVigencia).FirstOrDefaultAsync();
        }
    }
}