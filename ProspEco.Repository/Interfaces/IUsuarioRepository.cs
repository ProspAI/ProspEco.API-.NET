using ProspEco.Model.Entities;
using System.Threading.Tasks;

namespace ProspEco.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetByEmailAsync(string email);
    }
}