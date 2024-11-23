using ProspEco.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface INotificacaoRepository : IRepository<Notificacao>
    {
        Task<IEnumerable<Notificacao>> GetByUsuarioIdAsync(long usuarioId);
        Task<IEnumerable<Notificacao>> GetUnreadNotificationsAsync(long usuarioId);
    }
}