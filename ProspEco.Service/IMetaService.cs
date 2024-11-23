using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IMetaService
    {
        Task AddMeta(MetaRequest metaRequest);
        Task<IEnumerable<MetaResponse>> GetAllMetas();
        Task<MetaResponse> GetMetaById(long id);
        Task UpdateMeta(long id, MetaRequest metaRequest);
        Task DeleteMeta(long id);
    }
}