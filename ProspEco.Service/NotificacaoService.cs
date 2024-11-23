using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly IRepository<Notificacao> _notificacaoRepository;

        public NotificacaoService(IRepository<Notificacao> notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }

        public async Task AddNotificacao(NotificacaoRequest notificacaoRequest)
        {
            var notificacao = new Notificacao
            {
                DtHora = notificacaoRequest.DataHora,
                FlLida = notificacaoRequest.Lida,
                DsMensagem = notificacaoRequest.Mensagem,
                IdUsuario = notificacaoRequest.UsuarioId,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _notificacaoRepository.AddAsync(notificacao);
        }

        public async Task<IEnumerable<NotificacaoResponse>> GetAllNotificacoes()
        {
            var notificacoes = await _notificacaoRepository.GetAllAsync();
            return notificacoes.Select(notificacao => new NotificacaoResponse
            {
                IdNotificacao = notificacao.IdNotificacao,
                DataHora = notificacao.DtHora,
                Lida = notificacao.FlLida,
                Mensagem = notificacao.DsMensagem,
                IdUsuario = notificacao.IdUsuario,
                DtCriacao = notificacao.DtCriacao,
                DtModificacao = notificacao.DtModificacao
            });
        }

        public async Task<NotificacaoResponse> GetNotificacaoById(long id)
        {
            var notificacao = await _notificacaoRepository.GetByIdAsync(id);
            if (notificacao == null)
            {
                throw new Exception("Notificação não encontrada.");
            }

            return new NotificacaoResponse
            {
                IdNotificacao = notificacao.IdNotificacao,
                DataHora = notificacao.DtHora,
                Lida = notificacao.FlLida,
                Mensagem = notificacao.DsMensagem,
                IdUsuario = notificacao.IdUsuario,
                DtCriacao = notificacao.DtCriacao,
                DtModificacao = notificacao.DtModificacao
            };
        }

        public async Task UpdateNotificacao(long id, NotificacaoRequest notificacaoRequest)
        {
            var notificacaoExistente = await _notificacaoRepository.GetByIdAsync(id);
            if (notificacaoExistente == null)
            {
                throw new Exception("Notificação não encontrada.");
            }

            notificacaoExistente.DtHora = notificacaoRequest.DataHora;
            notificacaoExistente.FlLida = notificacaoRequest.Lida;
            notificacaoExistente.DsMensagem = notificacaoRequest.Mensagem;
            notificacaoExistente.IdUsuario = notificacaoRequest.UsuarioId;
            notificacaoExistente.DtModificacao = DateTime.UtcNow;

            await _notificacaoRepository.UpdateAsync(id, notificacaoExistente);
        }

        public async Task DeleteNotificacao(long id)
        {
            var notificacaoExistente = await _notificacaoRepository.GetByIdAsync(id);
            if (notificacaoExistente == null)
            {
                throw new Exception("Notificação não encontrada.");
            }

            await _notificacaoRepository.DeleteAsync(id);
        }
    }
}
