using ProspEco.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IMetaService
    {
        Task<IEnumerable<MetaDTO>> GetAllMetasAsync();
        Task<MetaDTO> GetMetaByIdAsync(long id);
        Task<IEnumerable<MetaDTO>> GetMetasByUsuarioIdAsync(long usuarioId);
        Task<MetaDTO> GetActiveMetaAsync(long usuarioId);
        Task<MetaDTO> CreateMetaAsync(MetaDTO metaDTO);
        Task UpdateMetaAsync(long id, MetaDTO metaDTO);
        Task DeleteMetaAsync(long id);
    }
}