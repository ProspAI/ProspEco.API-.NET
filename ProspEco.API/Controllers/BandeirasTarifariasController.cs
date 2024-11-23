using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BandeiraTarifariaController : ControllerBase
    {
        private readonly IBandeiraTarifariaService _bandeiraTarifariaService;

        public BandeiraTarifariaController(IBandeiraTarifariaService bandeiraTarifariaService)
        {
            _bandeiraTarifariaService = bandeiraTarifariaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBandeiraTarifaria([FromBody] BandeiraTarifariaRequest bandeiraRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bandeiraTarifariaService.AddBandeiraTarifaria(bandeiraRequest);
                return Created("", "Bandeira tarifária criada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar bandeira tarifária: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBandeirasTarifarias()
        {
            try
            {
                var bandeiras = await _bandeiraTarifariaService.GetAllBandeirasTarifarias();
                return Ok(bandeiras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar bandeiras tarifárias: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetBandeiraTarifariaById(long id)
        {
            try
            {
                var bandeira = await _bandeiraTarifariaService.GetBandeiraTarifariaById(id);
                return Ok(bandeira);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateBandeiraTarifaria(long id, [FromBody] BandeiraTarifariaRequest bandeiraRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bandeiraTarifariaService.UpdateBandeiraTarifaria(id, bandeiraRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar bandeira tarifária: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteBandeiraTarifaria(long id)
        {
            try
            {
                await _bandeiraTarifariaService.DeleteBandeiraTarifaria(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
