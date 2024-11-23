using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosConsumoController : ControllerBase
    {
        private readonly IRegistroConsumoService _registroConsumoService;

        public RegistrosConsumoController(IRegistroConsumoService registroConsumoService)
        {
            _registroConsumoService = registroConsumoService;
        }

        // GET: api/RegistrosConsumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroConsumoDTO>>> GetRegistrosConsumo()
        {
            var registros = await _registroConsumoService.GetAllRegistrosConsumoAsync();
            return Ok(registros);
        }

        // GET: api/RegistrosConsumo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroConsumoDTO>> GetRegistroConsumo(long id)
        {
            var registro = await _registroConsumoService.GetRegistroConsumoByIdAsync(id);
            if (registro == null)
            {
                return NotFound(new { message = $"Registro de consumo com ID {id} não encontrado." });
            }
            return Ok(registro);
        }

        // GET: api/RegistrosConsumo/aparelho/1
        [HttpGet("aparelho/{aparelhoId}")]
        public async Task<ActionResult<IEnumerable<RegistroConsumoDTO>>> GetRegistrosConsumoByAparelhoId(long aparelhoId)
        {
            var registros = await _registroConsumoService.GetRegistrosConsumoByAparelhoIdAsync(aparelhoId);
            return Ok(registros);
        }

        // GET: api/RegistrosConsumo/daterange?startDate=2023-01-01&endDate=2023-12-31
        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<RegistroConsumoDTO>>> GetRegistrosConsumoByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var registros = await _registroConsumoService.GetRegistrosConsumoByDateRangeAsync(startDate, endDate);
            return Ok(registros);
        }

        // POST: api/RegistrosConsumo
        [HttpPost]
        public async Task<ActionResult<RegistroConsumoDTO>> CreateRegistroConsumo(RegistroConsumoDTO registroConsumoDTO)
        {
            var novoRegistro = await _registroConsumoService.CreateRegistroConsumoAsync(registroConsumoDTO);
            return CreatedAtAction(nameof(GetRegistroConsumo), new { id = novoRegistro.Id }, novoRegistro);
        }

        // PUT: api/RegistrosConsumo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistroConsumo(long id, RegistroConsumoDTO registroConsumoDTO)
        {
            if (id != registroConsumoDTO.Id)
            {
                return BadRequest(new { message = "ID do registro de consumo na URL não corresponde ao ID no corpo da requisição." });
            }

            await _registroConsumoService.UpdateRegistroConsumoAsync(id, registroConsumoDTO);
            return NoContent();
        }

        // DELETE: api/RegistrosConsumo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroConsumo(long id)
        {
            await _registroConsumoService.DeleteRegistroConsumoAsync(id);
            return NoContent();
        }
    }
}
