using ProspEco.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IConquistaRepository : IRepository<Conquista>
    {
        Task<IEnumerable<Conquista>> GetByUsuarioIdAsync(long usuarioId);
    }
}