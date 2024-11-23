using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IRecomendacaoService
    {
        Task AddRecomendacao(RecomendacaoRequest recomendacaoRequest);
        Task<IEnumerable<RecomendacaoResponse>> GetAllRecomendacoes();
        Task<RecomendacaoResponse> GetRecomendacaoById(long id);
        Task UpdateRecomendacao(long id, RecomendacaoRequest recomendacaoRequest);
        Task DeleteRecomendacao(long id);
        IEnumerable<RecomendacaoResponse> GetRecomendacoesByUsuarioId(long usuarioId);
    }
}