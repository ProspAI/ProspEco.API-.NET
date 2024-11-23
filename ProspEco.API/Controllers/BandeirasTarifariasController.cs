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
    public class BandeirasTarifariasController : ControllerBase
    {
        private readonly IBandeiraTarifariaService _bandeiraService;

        public BandeirasTarifariasController(IBandeiraTarifariaService bandeiraService)
        {
            _bandeiraService = bandeiraService;
        }

        // GET: api/BandeirasTarifarias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BandeiraTarifariaDTO>>> GetBandeirasTarifarias()
        {
            var bandeiras = await _bandeiraService.GetAllBandeirasTarifariasAsync();
            return Ok(bandeiras);
        }

        // GET: api/BandeirasTarifarias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BandeiraTarifariaDTO>> GetBandeiraTarifaria(long id)
        {
            var bandeira = await _bandeiraService.GetBandeiraTarifariaByIdAsync(id);
            if (bandeira == null)
            {
                return NotFound(new { message = $"Bandeira tarifária com ID {id} não encontrada." });
            }
            return Ok(bandeira);
        }

        // GET: api/BandeirasTarifarias/data-vigencia/2023-12-31
        [HttpGet("data-vigencia/{dataVigencia}")]
        public async Task<ActionResult<IEnumerable<BandeiraTarifariaDTO>>> GetBandeirasByDataVigencia(DateTime dataVigencia)
        {
            var bandeiras = await _bandeiraService.GetBandeirasByDataVigenciaAsync(dataVigencia);
            return Ok(bandeiras);
        }

        // GET: api/BandeirasTarifarias/latest
        [HttpGet("latest")]
        public async Task<ActionResult<BandeiraTarifariaDTO>> GetLatestBandeiraTarifaria()
        {
            var bandeira = await _bandeiraService.GetLatestBandeiraTarifariaAsync();
            if (bandeira == null)
            {
                return NotFound(new { message = "Nenhuma bandeira tarifária encontrada." });
            }
            return Ok(bandeira);
        }

        // POST: api/BandeirasTarifarias
        [HttpPost]
        public async Task<ActionResult<BandeiraTarifariaDTO>> CreateBandeiraTarifaria(BandeiraTarifariaDTO bandeiraDTO)
        {
            var novaBandeira = await _bandeiraService.CreateBandeiraTarifariaAsync(bandeiraDTO);
            return CreatedAtAction(nameof(GetBandeiraTarifaria), new { id = novaBandeira.Id }, novaBandeira);
        }

        // PUT: api/BandeirasTarifarias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBandeiraTarifaria(long id, BandeiraTarifariaDTO bandeiraDTO)
        {
            if (id != bandeiraDTO.Id)
            {
                return BadRequest(new { message = "ID da bandeira tarifária na URL não corresponde ao ID no corpo da requisição." });
            }

            await _bandeiraService.UpdateBandeiraTarifariaAsync(id, bandeiraDTO);
            return NoContent();
        }

        // DELETE: api/BandeirasTarifarias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBandeiraTarifaria(long id)
        {
            await _bandeiraService.DeleteBandeiraTarifariaAsync(id);
            return NoContent();
        }
    }
}
