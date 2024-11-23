using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;
        private readonly IMapper _mapper;

        public NotificacaoService(INotificacaoRepository notificacaoRepository, IMapper mapper)
        {
            _notificacaoRepository = notificacaoRepository;
            _mapper = mapper;
        }

        public async Task<NotificacaoDTO> CreateNotificacaoAsync(NotificacaoDTO notificacaoDTO)
        {
            var notificacao = _mapper.Map<Notificacao>(notificacaoDTO);
            await _notificacaoRepository.AddAsync(notificacao);
            return _mapper.Map<NotificacaoDTO>(notificacao);
        }

        public async Task DeleteNotificacaoAsync(long id)
        {
            await _notificacaoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<NotificacaoDTO>> GetAllNotificacoesAsync()
        {
            var notificacoes = await _notificacaoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificacaoDTO>>(notificacoes);
        }

        public async Task<NotificacaoDTO> GetNotificacaoByIdAsync(long id)
        {
            var notificacao = await _notificacaoRepository.GetByIdAsync(id);
            return _mapper.Map<NotificacaoDTO>(notificacao);
        }

        public async Task<IEnumerable<NotificacaoDTO>> GetNotificacoesByUsuarioIdAsync(long usuarioId)
        {
            var notificacoes = await _notificacaoRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<IEnumerable<NotificacaoDTO>>(notificacoes);
        }

        public async Task<IEnumerable<NotificacaoDTO>> GetUnreadNotificacoesAsync(long usuarioId)
        {
            var notificacoes = await _notificacaoRepository.GetUnreadNotificationsAsync(usuarioId);
            return _mapper.Map<IEnumerable<NotificacaoDTO>>(notificacoes);
        }

        public async Task UpdateNotificacaoAsync(long id, NotificacaoDTO notificacaoDTO)
        {
            var notificacaoExistente = await _notificacaoRepository.GetByIdAsync(id);
            if (notificacaoExistente != null)
            {
                _mapper.Map(notificacaoDTO, notificacaoExistente);
                await _notificacaoRepository.UpdateAsync(notificacaoExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que a notificação não foi encontrada
                // throw new NotFoundException($"Notificação com ID {id} não foi encontrada.");
            }
        }
    }
}
