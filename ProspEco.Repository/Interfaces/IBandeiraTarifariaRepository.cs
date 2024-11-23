using ProspEco.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IBandeiraTarifariaRepository : IRepository<BandeiraTarifaria>
    {
        Task<IEnumerable<BandeiraTarifaria>> GetByDataVigenciaAsync(DateTime dataVigencia);
        Task<BandeiraTarifaria> GetLatestAsync();
    }
}