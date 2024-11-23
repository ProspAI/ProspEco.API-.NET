using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class AparelhoService : IAparelhoService
    {
        private readonly IAparelhoRepository _aparelhoRepository;
        private readonly IMapper _mapper;

        public AparelhoService(IAparelhoRepository aparelhoRepository, IMapper mapper)
        {
            _aparelhoRepository = aparelhoRepository;
            _mapper = mapper;
        }

        public async Task<AparelhoDTO> CreateAparelhoAsync(AparelhoDTO aparelhoDTO)
        {
            var aparelho = _mapper.Map<Aparelho>(aparelhoDTO);
            await _aparelhoRepository.AddAsync(aparelho);
            return _mapper.Map<AparelhoDTO>(aparelho);
        }

        public async Task DeleteAparelhoAsync(long id)
        {
            await _aparelhoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AparelhoDTO>> GetAllAparelhosAsync()
        {
            var aparelhos = await _aparelhoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AparelhoDTO>>(aparelhos);
        }

        public async Task<AparelhoDTO> GetAparelhoByIdAsync(long id)
        {
            var aparelho = await _aparelhoRepository.GetByIdAsync(id);
            return _mapper.Map<AparelhoDTO>(aparelho);
        }

        public async Task<IEnumerable<AparelhoDTO>> GetAparelhosByUsuarioIdAsync(long usuarioId)
        {
            var aparelhos = await _aparelhoRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<IEnumerable<AparelhoDTO>>(aparelhos);
        }

        public async Task UpdateAparelhoAsync(long id, AparelhoDTO aparelhoDTO)
        {
            var aparelhoExistente = await _aparelhoRepository.GetByIdAsync(id);
            if (aparelhoExistente != null)
            {
                _mapper.Map(aparelhoDTO, aparelhoExistente);
                await _aparelhoRepository.UpdateAsync(aparelhoExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que o aparelho não foi encontrado
                // throw new NotFoundException($"Aparelho com ID {id} não foi encontrado.");
            }
        }
    }
}
