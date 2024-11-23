using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _usuarioRepository;

        public UsuarioService(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task AddUsuario(UsuarioRequest usuarioRequest)
        {
            var usuario = new Usuario
            {
                DsEmail = usuarioRequest.Email,
                DsNome = usuarioRequest.Nome,
                DsSenha = usuarioRequest.Senha,
                DsRole = usuarioRequest.Role,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> GetAllUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => new UsuarioResponse
            {
                IdUsuario = u.IdUsuario,
                Email = u.DsEmail,
                Nome = u.DsNome,
                PontuacaoEconomia = u.VlPontuacaoEconomia,
                Role = u.DsRole,
                DtCriacao = u.DtCriacao,
                DtModificacao = u.DtModificacao
            });
        }

        public async Task<UsuarioResponse> GetUsuarioById(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Email = usuario.DsEmail,
                Nome = usuario.DsNome,
                PontuacaoEconomia = usuario.VlPontuacaoEconomia,
                Role = usuario.DsRole,
                DtCriacao = usuario.DtCriacao,
                DtModificacao = usuario.DtModificacao
            };
        }

        public async Task UpdateUsuario(long id, UsuarioRequest usuarioRequest)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuarioExistente.DsEmail = usuarioRequest.Email;
            usuarioExistente.DsNome = usuarioRequest.Nome;
            usuarioExistente.DsSenha = usuarioRequest.Senha;
            usuarioExistente.DsRole = usuarioRequest.Role;
            usuarioExistente.DtModificacao = DateTime.UtcNow;

            await _usuarioRepository.UpdateAsync(id, usuarioExistente);
        }

        public async Task DeleteUsuario(long id)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            await _usuarioRepository.DeleteAsync(id);
        }
    }
}
