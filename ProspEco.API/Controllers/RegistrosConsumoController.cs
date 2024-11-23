using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroConsumoController : ControllerBase
    {
        private readonly IRegistroConsumoService _registroConsumoService;

        public RegistroConsumoController(IRegistroConsumoService registroConsumoService)
        {
            _registroConsumoService = registroConsumoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRegistroConsumo([FromBody] RegistroConsumoRequest registroRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _registroConsumoService.AddRegistroConsumo(registroRequest);
                return Created("", "Registro de consumo criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar registro de consumo: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegistrosConsumo()
        {
            try
            {
                var registros = await _registroConsumoService.GetAllRegistrosConsumo();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar registros de consumo: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetRegistroConsumoById(long id)
        {
            try
            {
                var registro = await _registroConsumoService.GetRegistroConsumoById(id);
                return Ok(registro);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateRegistroConsumo(long id, [FromBody] RegistroConsumoRequest registroRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _registroConsumoService.UpdateRegistroConsumo(id, registroRequest);
                return NoContent(); // Atualização bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar registro de consumo: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteRegistroConsumo(long id)
        {
            try
            {
                await _registroConsumoService.DeleteRegistroConsumo(id);
                return NoContent(); // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
