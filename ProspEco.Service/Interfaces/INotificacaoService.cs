using ProspEco.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface INotificacaoService
    {
        Task<IEnumerable<NotificacaoDTO>> GetAllNotificacoesAsync();
        Task<NotificacaoDTO> GetNotificacaoByIdAsync(long id);
        Task<IEnumerable<NotificacaoDTO>> GetNotificacoesByUsuarioIdAsync(long usuarioId);
        Task<IEnumerable<NotificacaoDTO>> GetUnreadNotificacoesAsync(long usuarioId);
        Task<NotificacaoDTO> CreateNotificacaoAsync(NotificacaoDTO notificacaoDTO);
        Task UpdateNotificacaoAsync(long id, NotificacaoDTO notificacaoDTO);
        Task DeleteNotificacaoAsync(long id);
    }
}