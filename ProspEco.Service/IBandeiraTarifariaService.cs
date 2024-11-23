using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IBandeiraTarifariaService
    {
        Task AddBandeiraTarifaria(BandeiraTarifariaRequest bandeiraRequest);
        Task<IEnumerable<BandeiraTarifariaResponse>> GetAllBandeirasTarifarias();
        Task<BandeiraTarifariaResponse> GetBandeiraTarifariaById(long id);
        Task UpdateBandeiraTarifaria(long id, BandeiraTarifariaRequest bandeiraRequest);
        Task DeleteBandeiraTarifaria(long id);
    }
}