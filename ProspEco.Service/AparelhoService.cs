using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class AparelhoService : IAparelhoService
    {
        private readonly IRepository<Aparelho> _aparelhoRepository;

        public AparelhoService(IRepository<Aparelho> aparelhoRepository)
        {
            _aparelhoRepository = aparelhoRepository;
        }

        public async Task AddAparelho(AparelhoRequest aparelhoRequest)
        {
            var aparelho = new Aparelho
            {
                DsDescricao = aparelhoRequest.Descricao,
                DsNome = aparelhoRequest.Nome,
                VlPotencia = aparelhoRequest.Potencia,
                DsTipo = aparelhoRequest.Tipo,
                IdUsuario = aparelhoRequest.UsuarioId,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _aparelhoRepository.AddAsync(aparelho);
        }

        public async Task<IEnumerable<AparelhoResponse>> GetAllAparelhos()
        {
            var aparelhos = await _aparelhoRepository.GetAllAsync();
            return aparelhos.Select(aparelho => new AparelhoResponse
            {
                IdAparelho = aparelho.IdAparelho,
                Descricao = aparelho.DsDescricao,
                Nome = aparelho.DsNome,
                Potencia = aparelho.VlPotencia,
                Tipo = aparelho.DsTipo,
                IdUsuario = aparelho.IdUsuario,
                DtCriacao = aparelho.DtCriacao,
                DtModificacao = aparelho.DtModificacao
            });
        }

        public async Task<AparelhoResponse> GetAparelhoById(long id)
        {
            var aparelho = await _aparelhoRepository.GetByIdAsync(id);
            if (aparelho == null)
            {
                throw new Exception("Aparelho não encontrado.");
            }

            return new AparelhoResponse
            {
                IdAparelho = aparelho.IdAparelho,
                Descricao = aparelho.DsDescricao,
                Nome = aparelho.DsNome,
                Potencia = aparelho.VlPotencia,
                Tipo = aparelho.DsTipo,
                IdUsuario = aparelho.IdUsuario,
                DtCriacao = aparelho.DtCriacao,
                DtModificacao = aparelho.DtModificacao
            };
        }

        public async Task UpdateAparelho(long id, AparelhoRequest aparelhoRequest)
        {
            var aparelhoExistente = await _aparelhoRepository.GetByIdAsync(id);
            if (aparelhoExistente == null)
            {
                throw new Exception("Aparelho não encontrado.");
            }

            aparelhoExistente.DsDescricao = aparelhoRequest.Descricao;
            aparelhoExistente.DsNome = aparelhoRequest.Nome;
            aparelhoExistente.VlPotencia = aparelhoRequest.Potencia;
            aparelhoExistente.DsTipo = aparelhoRequest.Tipo;
            aparelhoExistente.IdUsuario = aparelhoRequest.UsuarioId;
            aparelhoExistente.DtModificacao = DateTime.UtcNow;

            await _aparelhoRepository.UpdateAsync(id, aparelhoExistente);
        }

        public async Task DeleteAparelho(long id)
        {
            var aparelhoExistente = await _aparelhoRepository.GetByIdAsync(id);
            if (aparelhoExistente == null)
            {
                throw new Exception("Aparelho não encontrado.");
            }

            await _aparelhoRepository.DeleteAsync(id);
        }
    }
}
