using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IAparelhoService
    {
        Task AddAparelho(AparelhoRequest aparelhoRequest);
        Task<IEnumerable<AparelhoResponse>> GetAllAparelhos();
        Task<AparelhoResponse> GetAparelhoById(long id);
        Task UpdateAparelho(long id, AparelhoRequest aparelhoRequest);
        Task DeleteAparelho(long id);
    }
}