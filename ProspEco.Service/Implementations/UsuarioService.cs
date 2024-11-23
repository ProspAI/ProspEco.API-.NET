using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> CreateUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            // Mapeia o DTO para a entidade
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            
            // Adiciona o usuário no banco de dados
            await _usuarioRepository.AddAsync(usuario);
            
            // Mapeia a entidade de volta para o DTO
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task DeleteUsuarioAsync(long id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task UpdateUsuarioAsync(long id, UsuarioDTO usuarioDTO)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente != null)
            {
                // Mapeia as alterações do DTO para a entidade existente
                _mapper.Map(usuarioDTO, usuarioExistente);
                
                // Atualiza o usuário no banco de dados
                await _usuarioRepository.UpdateAsync(usuarioExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que o usuário não foi encontrado
                // throw new NotFoundException($"Usuário com ID {id} não foi encontrado.");
            }
        }
    }
}
