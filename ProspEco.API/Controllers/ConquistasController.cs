using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConquistasController : ControllerBase
    {
        private readonly IConquistaService _conquistaService;

        public ConquistasController(IConquistaService conquistaService)
        {
            _conquistaService = conquistaService;
        }

        // GET: api/Conquistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConquistaDTO>>> GetConquistas()
        {
            var conquistas = await _conquistaService.GetAllConquistasAsync();
            return Ok(conquistas);
        }

        // GET: api/Conquistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConquistaDTO>> GetConquista(long id)
        {
            var conquista = await _conquistaService.GetConquistaByIdAsync(id);
            if (conquista == null)
            {
                return NotFound(new { message = $"Conquista com ID {id} não encontrada." });
            }
            return Ok(conquista);
        }

        // GET: api/Conquistas/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ConquistaDTO>>> GetConquistasByUsuarioId(long usuarioId)
        {
            var conquistas = await _conquistaService.GetConquistasByUsuarioIdAsync(usuarioId);
            return Ok(conquistas);
        }

        // POST: api/Conquistas
        [HttpPost]
        public async Task<ActionResult<ConquistaDTO>> CreateConquista(ConquistaDTO conquistaDTO)
        {
            var novaConquista = await _conquistaService.CreateConquistaAsync(conquistaDTO);
            return CreatedAtAction(nameof(GetConquista), new { id = novaConquista.Id }, novaConquista);
        }

        // PUT: api/Conquistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConquista(long id, ConquistaDTO conquistaDTO)
        {
            if (id != conquistaDTO.Id)
            {
                return BadRequest(new { message = "ID da conquista na URL não corresponde ao ID no corpo da requisição." });
            }

            await _conquistaService.UpdateConquistaAsync(id, conquistaDTO);
            return NoContent();
        }

        // DELETE: api/Conquistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConquista(long id)
        {
            await _conquistaService.DeleteConquistaAsync(id);
            return NoContent();
        }
    }
}
