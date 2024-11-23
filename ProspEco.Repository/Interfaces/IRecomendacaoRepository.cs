using ProspEco.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IRecomendacaoRepository : IRepository<Recomendacao>
    {
        Task<IEnumerable<Recomendacao>> GetByUsuarioIdAsync(long usuarioId);
    }
}