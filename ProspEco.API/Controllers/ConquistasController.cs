using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConquistaController : ControllerBase
    {
        private readonly IConquistaService _conquistaService;

        public ConquistaController(IConquistaService conquistaService)
        {
            _conquistaService = conquistaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddConquista([FromBody] ConquistaRequest conquistaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _conquistaService.AddConquista(conquistaRequest);
                return Created("", "Conquista criada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar conquista: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConquistas()
        {
            try
            {
                var conquistas = await _conquistaService.GetAllConquistas();
                return Ok(conquistas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar conquistas: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetConquistaById(long id)
        {
            try
            {
                var conquista = await _conquistaService.GetConquistaById(id);
                return Ok(conquista);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateConquista(long id, [FromBody] ConquistaRequest conquistaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _conquistaService.UpdateConquista(id, conquistaRequest);
                return NoContent(); // Atualização bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar conquista: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteConquista(long id)
        {
            try
            {
                await _conquistaService.DeleteConquista(id);
                return NoContent(); // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
