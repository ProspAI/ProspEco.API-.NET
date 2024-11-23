using ProspEco.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IAparelhoRepository : IRepository<Aparelho>
    {
        Task<IEnumerable<Aparelho>> GetByUsuarioIdAsync(long usuarioId);
    }
}