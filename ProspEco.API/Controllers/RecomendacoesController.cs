using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly IRecomendacaoService _recomendacaoService;

        public RecomendacaoController(IRecomendacaoService recomendacaoService)
        {
            _recomendacaoService = recomendacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecomendacao([FromBody] RecomendacaoRequest recomendacaoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _recomendacaoService.AddRecomendacao(recomendacaoRequest);
                return Created("", "Recomendação criada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar recomendação: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecomendacoes()
        {
            try
            {
                var recomendacoes = await _recomendacaoService.GetAllRecomendacoes();
                return Ok(recomendacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar recomendações: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetRecomendacaoById(long id)
        {
            try
            {
                var recomendacao = await _recomendacaoService.GetRecomendacaoById(id);
                return Ok(recomendacao);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("usuario/{usuarioId:long}")]
        public IActionResult GetRecomendacoesByUsuarioId(long usuarioId)
        {
            try
            {
                var recomendacoes = _recomendacaoService.GetRecomendacoesByUsuarioId(usuarioId);
                return Ok(recomendacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar recomendações do usuário: {ex.Message}");
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateRecomendacao(long id, [FromBody] RecomendacaoRequest recomendacaoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _recomendacaoService.UpdateRecomendacao(id, recomendacaoRequest);
                return NoContent(); // Atualização bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar recomendação: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteRecomendacao(long id)
        {
            try
            {
                await _recomendacaoService.DeleteRecomendacao(id);
                return NoContent(); // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
