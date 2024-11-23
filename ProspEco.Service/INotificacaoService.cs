using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;

namespace ProspEco.Service
{
    public interface INotificacaoService
    {
        Task AddNotificacao(NotificacaoRequest notificacaoRequest);
        Task<IEnumerable<NotificacaoResponse>> GetAllNotificacoes();
        Task<NotificacaoResponse> GetNotificacaoById(long id);
        Task UpdateNotificacao(long id, NotificacaoRequest notificacaoRequest);
        Task DeleteNotificacao(long id);
    }
}