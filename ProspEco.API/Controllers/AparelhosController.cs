using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AparelhosController : ControllerBase
    {
        private readonly IAparelhoService _aparelhoService;

        public AparelhosController(IAparelhoService aparelhoService)
        {
            _aparelhoService = aparelhoService;
        }

        // GET: api/Aparelhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AparelhoDTO>>> GetAparelhos()
        {
            var aparelhos = await _aparelhoService.GetAllAparelhosAsync();
            return Ok(aparelhos);
        }

        // GET: api/Aparelhos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AparelhoDTO>> GetAparelho(long id)
        {
            var aparelho = await _aparelhoService.GetAparelhoByIdAsync(id);
            if (aparelho == null)
            {
                return NotFound(new { message = $"Aparelho com ID {id} não encontrado." });
            }
            return Ok(aparelho);
        }

        // GET: api/Aparelhos/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<AparelhoDTO>>> GetAparelhosByUsuarioId(long usuarioId)
        {
            var aparelhos = await _aparelhoService.GetAparelhosByUsuarioIdAsync(usuarioId);
            return Ok(aparelhos);
        }

        // POST: api/Aparelhos
        [HttpPost]
        public async Task<ActionResult<AparelhoDTO>> CreateAparelho(AparelhoDTO aparelhoDTO)
        {
            var novoAparelho = await _aparelhoService.CreateAparelhoAsync(aparelhoDTO);
            return CreatedAtAction(nameof(GetAparelho), new { id = novoAparelho.Id }, novoAparelho);
        }

        // PUT: api/Aparelhos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAparelho(long id, AparelhoDTO aparelhoDTO)
        {
            if (id != aparelhoDTO.Id)
            {
                return BadRequest(new { message = "ID do aparelho na URL não corresponde ao ID no corpo da requisição." });
            }

            await _aparelhoService.UpdateAparelhoAsync(id, aparelhoDTO);
            return NoContent();
        }

        // DELETE: api/Aparelhos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAparelho(long id)
        {
            await _aparelhoService.DeleteAparelhoAsync(id);
            return NoContent();
        }
    }
}
