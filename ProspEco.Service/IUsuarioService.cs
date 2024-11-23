using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface IUsuarioService
    {
        Task AddUsuario(UsuarioRequest usuarioRequest);
        Task<IEnumerable<UsuarioResponse>> GetAllUsuarios();
        Task<UsuarioResponse> GetUsuarioById(long id);
        Task UpdateUsuario(long id, UsuarioRequest usuarioRequest);
        Task DeleteUsuario(long id);
    }
}