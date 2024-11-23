using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class MetaService : IMetaService
    {
        private readonly IRepository<Meta> _metaRepository;

        public MetaService(IRepository<Meta> metaRepository)
        {
            _metaRepository = metaRepository;
        }

        public async Task AddMeta(MetaRequest metaRequest)
        {
            var meta = new Meta
            {
                FlAtingida = metaRequest.Atingida,
                VlConsumoAlvo = metaRequest.ConsumoAlvo,
                DtInicio = metaRequest.DataInicio,
                DtFim = metaRequest.DataFim,
                IdUsuario = metaRequest.UsuarioId,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _metaRepository.AddAsync(meta);
        }

        public async Task<IEnumerable<MetaResponse>> GetAllMetas()
        {
            var metas = await _metaRepository.GetAllAsync();
            return metas.Select(meta => new MetaResponse
            {
                IdMeta = meta.IdMeta,
                Atingida = meta.FlAtingida,
                ConsumoAlvo = meta.VlConsumoAlvo,
                DataInicio = meta.DtInicio,
                DataFim = meta.DtFim,
                IdUsuario = meta.IdUsuario,
                DtCriacao = meta.DtCriacao,
                DtModificacao = meta.DtModificacao
            });
        }

        public async Task<MetaResponse> GetMetaById(long id)
        {
            var meta = await _metaRepository.GetByIdAsync(id);
            if (meta == null)
            {
                throw new Exception("Meta não encontrada.");
            }

            return new MetaResponse
            {
                IdMeta = meta.IdMeta,
                Atingida = meta.FlAtingida,
                ConsumoAlvo = meta.VlConsumoAlvo,
                DataInicio = meta.DtInicio,
                DataFim = meta.DtFim,
                IdUsuario = meta.IdUsuario,
                DtCriacao = meta.DtCriacao,
                DtModificacao = meta.DtModificacao
            };
        }

        public async Task UpdateMeta(long id, MetaRequest metaRequest)
        {
            var metaExistente = await _metaRepository.GetByIdAsync(id);
            if (metaExistente == null)
            {
                throw new Exception("Meta não encontrada.");
            }

            metaExistente.FlAtingida = metaRequest.Atingida;
            metaExistente.VlConsumoAlvo = metaRequest.ConsumoAlvo;
            metaExistente.DtInicio = metaRequest.DataInicio;
            metaExistente.DtFim = metaRequest.DataFim;
            metaExistente.IdUsuario = metaRequest.UsuarioId;
            metaExistente.DtModificacao = DateTime.UtcNow;

            await _metaRepository.UpdateAsync(id, metaExistente);
        }

        public async Task DeleteMeta(long id)
        {
            var metaExistente = await _metaRepository.GetByIdAsync(id);
            if (metaExistente == null)
            {
                throw new Exception("Meta não encontrada.");
            }

            await _metaRepository.DeleteAsync(id);
        }
    }
}
