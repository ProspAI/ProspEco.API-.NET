using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacoesController : ControllerBase
    {
        private readonly IRecomendacaoService _recomendacaoService;

        public RecomendacoesController(IRecomendacaoService recomendacaoService)
        {
            _recomendacaoService = recomendacaoService;
        }

        // GET: api/Recomendacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecomendacaoDTO>>> GetRecomendacoes()
        {
            var recomendacoes = await _recomendacaoService.GetAllRecomendacoesAsync();
            return Ok(recomendacoes);
        }

        // GET: api/Recomendacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecomendacaoDTO>> GetRecomendacao(long id)
        {
            var recomendacao = await _recomendacaoService.GetRecomendacaoByIdAsync(id);
            if (recomendacao == null)
            {
                return NotFound(new { message = $"Recomendação com ID {id} não encontrada." });
            }
            return Ok(recomendacao);
        }

        // GET: api/Recomendacoes/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<RecomendacaoDTO>>> GetRecomendacoesByUsuarioId(long usuarioId)
        {
            var recomendacoes = await _recomendacaoService.GetRecomendacoesByUsuarioIdAsync(usuarioId);
            return Ok(recomendacoes);
        }

        // POST: api/Recomendacoes
        [HttpPost]
        public async Task<ActionResult<RecomendacaoDTO>> CreateRecomendacao(RecomendacaoDTO recomendacaoDTO)
        {
            var novaRecomendacao = await _recomendacaoService.CreateRecomendacaoAsync(recomendacaoDTO);
            return CreatedAtAction(nameof(GetRecomendacao), new { id = novaRecomendacao.Id }, novaRecomendacao);
        }

        // PUT: api/Recomendacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecomendacao(long id, RecomendacaoDTO recomendacaoDTO)
        {
            if (id != recomendacaoDTO.Id)
            {
                return BadRequest(new { message = "ID da recomendação na URL não corresponde ao ID no corpo da requisição." });
            }

            await _recomendacaoService.UpdateRecomendacaoAsync(id, recomendacaoDTO);
            return NoContent();
        }

        // DELETE: api/Recomendacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomendacao(long id)
        {
            await _recomendacaoService.DeleteRecomendacaoAsync(id);
            return NoContent();
        }
    }
}
