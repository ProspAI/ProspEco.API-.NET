using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class RecomendacaoService : IRecomendacaoService
    {
        private readonly IRecomendacaoRepository _recomendacaoRepository;
        private readonly IMapper _mapper;

        public RecomendacaoService(IRecomendacaoRepository recomendacaoRepository, IMapper mapper)
        {
            _recomendacaoRepository = recomendacaoRepository;
            _mapper = mapper;
        }

        public async Task<RecomendacaoDTO> CreateRecomendacaoAsync(RecomendacaoDTO recomendacaoDTO)
        {
            var recomendacao = _mapper.Map<Recomendacao>(recomendacaoDTO);
            await _recomendacaoRepository.AddAsync(recomendacao);
            return _mapper.Map<RecomendacaoDTO>(recomendacao);
        }

        public async Task DeleteRecomendacaoAsync(long id)
        {
            await _recomendacaoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RecomendacaoDTO>> GetAllRecomendacoesAsync()
        {
            var recomendacoes = await _recomendacaoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecomendacaoDTO>>(recomendacoes);
        }

        public async Task<RecomendacaoDTO> GetRecomendacaoByIdAsync(long id)
        {
            var recomendacao = await _recomendacaoRepository.GetByIdAsync(id);
            return _mapper.Map<RecomendacaoDTO>(recomendacao);
        }

        public async Task<IEnumerable<RecomendacaoDTO>> GetRecomendacoesByUsuarioIdAsync(long usuarioId)
        {
            var recomendacoes = await _recomendacaoRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<IEnumerable<RecomendacaoDTO>>(recomendacoes);
        }

        public async Task UpdateRecomendacaoAsync(long id, RecomendacaoDTO recomendacaoDTO)
        {
            var recomendacaoExistente = await _recomendacaoRepository.GetByIdAsync(id);
            if (recomendacaoExistente != null)
            {
                _mapper.Map(recomendacaoDTO, recomendacaoExistente);
                await _recomendacaoRepository.UpdateAsync(recomendacaoExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que a recomendação não foi encontrada
                // throw new NotFoundException($"Recomendação com ID {id} não foi encontrada.");
            }
        }
    }
}
