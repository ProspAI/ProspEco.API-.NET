using ProspEco.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IRegistroConsumoRepository : IRepository<RegistroConsumo>
    {
        Task<IEnumerable<RegistroConsumo>> GetByAparelhoIdAsync(long aparelhoId);
        Task<IEnumerable<RegistroConsumo>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}