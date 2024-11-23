using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNotificacao([FromBody] NotificacaoRequest notificacaoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _notificacaoService.AddNotificacao(notificacaoRequest);
                return Created("", "Notificação criada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar notificação: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificacoes()
        {
            try
            {
                var notificacoes = await _notificacaoService.GetAllNotificacoes();
                return Ok(notificacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar notificações: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetNotificacaoById(long id)
        {
            try
            {
                var notificacao = await _notificacaoService.GetNotificacaoById(id);
                return Ok(notificacao);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateNotificacao(long id, [FromBody] NotificacaoRequest notificacaoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _notificacaoService.UpdateNotificacao(id, notificacaoRequest);
                return NoContent(); // Atualização bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar notificação: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteNotificacao(long id)
        {
            try
            {
                await _notificacaoService.DeleteNotificacao(id);
                return NoContent(); // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
