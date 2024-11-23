using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class RecomendacaoService : IRecomendacaoService
    {
        private readonly IRepository<Recomendacao> _recomendacaoRepository;

        public RecomendacaoService(IRepository<Recomendacao> recomendacaoRepository)
        {
            _recomendacaoRepository = recomendacaoRepository;
        }

        // Adicionar uma recomendação
        public async Task AddRecomendacao(RecomendacaoRequest recomendacaoRequest)
        {
            var recomendacao = new Recomendacao
            {
                DtHora = recomendacaoRequest.DataHora,
                DsMensagem = recomendacaoRequest.Mensagem,
                IdUsuario = recomendacaoRequest.UsuarioId,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _recomendacaoRepository.AddAsync(recomendacao);
        }

        // Obter todas as recomendações
        public async Task<IEnumerable<RecomendacaoResponse>> GetAllRecomendacoes()
        {
            var recomendacoes = await _recomendacaoRepository.GetAllAsync();
            return recomendacoes.Select(recomendacao => new RecomendacaoResponse
            {
                IdRecomendacao = recomendacao.IdRecomendacao,
                DataHora = recomendacao.DtHora,
                Mensagem = recomendacao.DsMensagem,
                IdUsuario = recomendacao.IdUsuario,
                DtCriacao = recomendacao.DtCriacao,
                DtModificacao = recomendacao.DtModificacao
            });
        }

        // Obter uma recomendação por ID
        public async Task<RecomendacaoResponse> GetRecomendacaoById(long id)
        {
            var recomendacao = await _recomendacaoRepository.GetByIdAsync(id);
            if (recomendacao == null)
            {
                throw new Exception("Recomendação não encontrada.");
            }

            return new RecomendacaoResponse
            {
                IdRecomendacao = recomendacao.IdRecomendacao,
                DataHora = recomendacao.DtHora,
                Mensagem = recomendacao.DsMensagem,
                IdUsuario = recomendacao.IdUsuario,
                DtCriacao = recomendacao.DtCriacao,
                DtModificacao = recomendacao.DtModificacao
            };
        }

        // Atualizar uma recomendação
        public async Task UpdateRecomendacao(long id, RecomendacaoRequest recomendacaoRequest)
        {
            var recomendacaoExistente = await _recomendacaoRepository.GetByIdAsync(id);
            if (recomendacaoExistente == null)
            {
                throw new Exception("Recomendação não encontrada.");
            }

            recomendacaoExistente.DtHora = recomendacaoRequest.DataHora;
            recomendacaoExistente.DsMensagem = recomendacaoRequest.Mensagem;
            recomendacaoExistente.IdUsuario = recomendacaoRequest.UsuarioId;
            recomendacaoExistente.DtModificacao = DateTime.UtcNow;

            await _recomendacaoRepository.UpdateAsync(id, recomendacaoExistente);
        }

        // Deletar uma recomendação
        public async Task DeleteRecomendacao(long id)
        {
            var recomendacaoExistente = await _recomendacaoRepository.GetByIdAsync(id);
            if (recomendacaoExistente == null)
            {
                throw new Exception("Recomendação não encontrada.");
            }

            await _recomendacaoRepository.DeleteAsync(id);
        }

        // Obter recomendações de um usuário específico
        public IEnumerable<RecomendacaoResponse> GetRecomendacoesByUsuarioId(long usuarioId)
        {
            var recomendacoes = _recomendacaoRepository.GetAllAsync().Result
                .Where(r => r.IdUsuario == usuarioId);

            return recomendacoes.Select(recomendacao => new RecomendacaoResponse
            {
                IdRecomendacao = recomendacao.IdRecomendacao,
                DataHora = recomendacao.DtHora,
                Mensagem = recomendacao.DsMensagem,
                IdUsuario = recomendacao.IdUsuario,
                DtCriacao = recomendacao.DtCriacao,
                DtModificacao = recomendacao.DtModificacao
            });
        }
    }
}
