using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IConquistaService
    {
        Task AddConquista(ConquistaRequest conquistaRequest);
        Task<IEnumerable<ConquistaResponse>> GetAllConquistas();
        Task<ConquistaResponse> GetConquistaById(long id);
        Task UpdateConquista(long id, ConquistaRequest conquistaRequest);
        Task DeleteConquista(long id);
    }
}