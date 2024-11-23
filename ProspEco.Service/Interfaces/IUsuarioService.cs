using ProspEco.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
        Task<UsuarioDTO> GetUsuarioByIdAsync(long id);
        Task<UsuarioDTO> GetUsuarioByEmailAsync(string email);
        Task<UsuarioDTO> CreateUsuarioAsync(UsuarioDTO usuarioDTO);
        Task UpdateUsuarioAsync(long id, UsuarioDTO usuarioDTO);
        Task DeleteUsuarioAsync(long id);
    }
}