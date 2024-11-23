using ProspEco.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IConquistaService
    {
        Task<IEnumerable<ConquistaDTO>> GetAllConquistasAsync();
        Task<ConquistaDTO> GetConquistaByIdAsync(long id);
        Task<IEnumerable<ConquistaDTO>> GetConquistasByUsuarioIdAsync(long usuarioId);
        Task<ConquistaDTO> CreateConquistaAsync(ConquistaDTO conquistaDTO);
        Task UpdateConquistaAsync(long id, ConquistaDTO conquistaDTO);
        Task DeleteConquistaAsync(long id);
    }
}