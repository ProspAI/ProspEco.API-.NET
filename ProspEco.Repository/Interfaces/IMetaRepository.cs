using ProspEco.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IMetaRepository : IRepository<Meta>
    {
        Task<IEnumerable<Meta>> GetByUsuarioIdAsync(long usuarioId);
        Task<Meta> GetActiveMetaAsync(long usuarioId);
    }
}