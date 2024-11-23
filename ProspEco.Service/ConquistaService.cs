using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class ConquistaService : IConquistaService
    {
        private readonly IRepository<Conquista> _conquistaRepository;

        public ConquistaService(IRepository<Conquista> conquistaRepository)
        {
            _conquistaRepository = conquistaRepository;
        }

        public async Task AddConquista(ConquistaRequest conquistaRequest)
        {
            var conquista = new Conquista
            {
                DtConquista = conquistaRequest.DataConquista,
                DsDescricao = conquistaRequest.Descricao,
                DsTitulo = conquistaRequest.Titulo,
                IdUsuario = conquistaRequest.UsuarioId,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _conquistaRepository.AddAsync(conquista);
        }

        public async Task<IEnumerable<ConquistaResponse>> GetAllConquistas()
        {
            var conquistas = await _conquistaRepository.GetAllAsync();
            return conquistas.Select(conquista => new ConquistaResponse
            {
                IdConquista = conquista.IdConquista,
                DataConquista = conquista.DtConquista,
                Descricao = conquista.DsDescricao,
                Titulo = conquista.DsTitulo,
                IdUsuario = conquista.IdUsuario,
                DtCriacao = conquista.DtCriacao,
                DtModificacao = conquista.DtModificacao
            });
        }

        public async Task<ConquistaResponse> GetConquistaById(long id)
        {
            var conquista = await _conquistaRepository.GetByIdAsync(id);
            if (conquista == null)
            {
                throw new Exception("Conquista não encontrada.");
            }

            return new ConquistaResponse
            {
                IdConquista = conquista.IdConquista,
                DataConquista = conquista.DtConquista,
                Descricao = conquista.DsDescricao,
                Titulo = conquista.DsTitulo,
                IdUsuario = conquista.IdUsuario,
                DtCriacao = conquista.DtCriacao,
                DtModificacao = conquista.DtModificacao
            };
        }

        public async Task UpdateConquista(long id, ConquistaRequest conquistaRequest)
        {
            var conquistaExistente = await _conquistaRepository.GetByIdAsync(id);
            if (conquistaExistente == null)
            {
                throw new Exception("Conquista não encontrada.");
            }

            conquistaExistente.DtConquista = conquistaRequest.DataConquista;
            conquistaExistente.DsDescricao = conquistaRequest.Descricao;
            conquistaExistente.DsTitulo = conquistaRequest.Titulo;
            conquistaExistente.IdUsuario = conquistaRequest.UsuarioId;
            conquistaExistente.DtModificacao = DateTime.UtcNow;

            await _conquistaRepository.UpdateAsync(id, conquistaExistente);
        }

        public async Task DeleteConquista(long id)
        {
            var conquistaExistente = await _conquistaRepository.GetByIdAsync(id);
            if (conquistaExistente == null)
            {
                throw new Exception("Conquista não encontrada.");
            }

            await _conquistaRepository.DeleteAsync(id);
        }
    }
}
