using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AparelhoController : ControllerBase
    {
        private readonly IAparelhoService _aparelhoService;

        public AparelhoController(IAparelhoService aparelhoService)
        {
            _aparelhoService = aparelhoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAparelho([FromBody] AparelhoRequest aparelhoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _aparelhoService.AddAparelho(aparelhoRequest);
                return Created("", "Aparelho criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar aparelho: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAparelhos()
        {
            try
            {
                var aparelhos = await _aparelhoService.GetAllAparelhos();
                return Ok(aparelhos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar aparelhos: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAparelhoById(long id)
        {
            try
            {
                var aparelho = await _aparelhoService.GetAparelhoById(id);
                return Ok(aparelho);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAparelho(long id, [FromBody] AparelhoRequest aparelhoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _aparelhoService.UpdateAparelho(id, aparelhoRequest);
                return NoContent(); // Atualização concluída sem retornar conteúdo
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar aparelho: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAparelho(long id)
        {
            try
            {
                await _aparelhoService.DeleteAparelho(id);
                return NoContent(); // Exclusão concluída sem retornar conteúdo
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
