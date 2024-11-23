using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using System.Threading.Tasks;
using ProspEco.Database.Contexts;

namespace ProspEco.Repository.Implementations
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}