using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProspEco.Database.Contexts;

namespace ProspEco.Repository.Implementations
{
    public class NotificacaoRepository : Repository<Notificacao>, INotificacaoRepository
    {
        public NotificacaoRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notificacao>> GetByUsuarioIdAsync(long usuarioId)
        {
            return await _dbSet.Where(n => n.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<IEnumerable<Notificacao>> GetUnreadNotificationsAsync(long usuarioId)
        {
            return await _dbSet.Where(n => n.UsuarioId == usuarioId && !n.Lida).ToListAsync();
        }
    }
}