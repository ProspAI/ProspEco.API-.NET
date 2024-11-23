using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacoesController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacoesController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        // GET: api/Notificacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacaoDTO>>> GetNotificacoes()
        {
            var notificacoes = await _notificacaoService.GetAllNotificacoesAsync();
            return Ok(notificacoes);
        }

        // GET: api/Notificacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacaoDTO>> GetNotificacao(long id)
        {
            var notificacao = await _notificacaoService.GetNotificacaoByIdAsync(id);
            if (notificacao == null)
            {
                return NotFound(new { message = $"Notificação com ID {id} não encontrada." });
            }
            return Ok(notificacao);
        }

        // GET: api/Notificacoes/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<NotificacaoDTO>>> GetNotificacoesByUsuarioId(long usuarioId)
        {
            var notificacoes = await _notificacaoService.GetNotificacoesByUsuarioIdAsync(usuarioId);
            return Ok(notificacoes);
        }

        // GET: api/Notificacoes/unread/1
        [HttpGet("unread/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<NotificacaoDTO>>> GetUnreadNotificacoes(long usuarioId)
        {
            var notificacoes = await _notificacaoService.GetUnreadNotificacoesAsync(usuarioId);
            return Ok(notificacoes);
        }

        // POST: api/Notificacoes
        [HttpPost]
        public async Task<ActionResult<NotificacaoDTO>> CreateNotificacao(NotificacaoDTO notificacaoDTO)
        {
            var novaNotificacao = await _notificacaoService.CreateNotificacaoAsync(notificacaoDTO);
            return CreatedAtAction(nameof(GetNotificacao), new { id = novaNotificacao.Id }, novaNotificacao);
        }

        // PUT: api/Notificacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacao(long id, NotificacaoDTO notificacaoDTO)
        {
            if (id != notificacaoDTO.Id)
            {
                return BadRequest(new { message = "ID da notificação na URL não corresponde ao ID no corpo da requisição." });
            }

            await _notificacaoService.UpdateNotificacaoAsync(id, notificacaoDTO);
            return NoContent();
        }

        // DELETE: api/Notificacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacao(long id)
        {
            await _notificacaoService.DeleteNotificacaoAsync(id);
            return NoContent();
        }
    }
}
