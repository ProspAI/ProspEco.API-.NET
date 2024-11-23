using ProspEco.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IAparelhoService
    {
        Task<IEnumerable<AparelhoDTO>> GetAllAparelhosAsync();
        Task<AparelhoDTO> GetAparelhoByIdAsync(long id);
        Task<IEnumerable<AparelhoDTO>> GetAparelhosByUsuarioIdAsync(long usuarioId);
        Task<AparelhoDTO> CreateAparelhoAsync(AparelhoDTO aparelhoDTO);
        Task UpdateAparelhoAsync(long id, AparelhoDTO aparelhoDTO);
        Task DeleteAparelhoAsync(long id);
    }
}