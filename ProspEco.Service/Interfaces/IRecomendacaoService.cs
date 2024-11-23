using ProspEco.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IRecomendacaoService
    {
        Task<IEnumerable<RecomendacaoDTO>> GetAllRecomendacoesAsync();
        Task<RecomendacaoDTO> GetRecomendacaoByIdAsync(long id);
        Task<IEnumerable<RecomendacaoDTO>> GetRecomendacoesByUsuarioIdAsync(long usuarioId);
        Task<RecomendacaoDTO> CreateRecomendacaoAsync(RecomendacaoDTO recomendacaoDTO);
        Task UpdateRecomendacaoAsync(long id, RecomendacaoDTO recomendacaoDTO);
        Task DeleteRecomendacaoAsync(long id);
    }
}