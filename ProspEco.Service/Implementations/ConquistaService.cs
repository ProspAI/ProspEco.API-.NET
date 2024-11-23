using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class ConquistaService : IConquistaService
    {
        private readonly IConquistaRepository _conquistaRepository;
        private readonly IMapper _mapper;

        public ConquistaService(IConquistaRepository conquistaRepository, IMapper mapper)
        {
            _conquistaRepository = conquistaRepository;
            _mapper = mapper;
        }

        public async Task<ConquistaDTO> CreateConquistaAsync(ConquistaDTO conquistaDTO)
        {
            var conquista = _mapper.Map<Conquista>(conquistaDTO);
            await _conquistaRepository.AddAsync(conquista);
            return _mapper.Map<ConquistaDTO>(conquista);
        }

        public async Task DeleteConquistaAsync(long id)
        {
            await _conquistaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ConquistaDTO>> GetAllConquistasAsync()
        {
            var conquistas = await _conquistaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConquistaDTO>>(conquistas);
        }

        public async Task<ConquistaDTO> GetConquistaByIdAsync(long id)
        {
            var conquista = await _conquistaRepository.GetByIdAsync(id);
            return _mapper.Map<ConquistaDTO>(conquista);
        }

        public async Task<IEnumerable<ConquistaDTO>> GetConquistasByUsuarioIdAsync(long usuarioId)
        {
            var conquistas = await _conquistaRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<IEnumerable<ConquistaDTO>>(conquistas);
        }

        public async Task UpdateConquistaAsync(long id, ConquistaDTO conquistaDTO)
        {
            var conquistaExistente = await _conquistaRepository.GetByIdAsync(id);
            if (conquistaExistente != null)
            {
                _mapper.Map(conquistaDTO, conquistaExistente);
                await _conquistaRepository.UpdateAsync(conquistaExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que a conquista não foi encontrada
                // throw new NotFoundException($"Conquista com ID {id} não foi encontrada.");
            }
        }
    }
}
