using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IRegistroConsumoService
    {
        Task AddRegistroConsumo(RegistroConsumoRequest registroRequest);
        Task<IEnumerable<RegistroConsumoResponse>> GetAllRegistrosConsumo();
        Task<RegistroConsumoResponse> GetRegistroConsumoById(long id);
        Task UpdateRegistroConsumo(long id, RegistroConsumoRequest registroRequest);
        Task DeleteRegistroConsumo(long id);
    }
}