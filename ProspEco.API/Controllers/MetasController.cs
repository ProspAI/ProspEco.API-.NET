using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetasController : ControllerBase
    {
        private readonly IMetaService _metaService;

        public MetasController(IMetaService metaService)
        {
            _metaService = metaService;
        }

        // GET: api/Metas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetaDTO>>> GetMetas()
        {
            var metas = await _metaService.GetAllMetasAsync();
            return Ok(metas);
        }

        // GET: api/Metas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetaDTO>> GetMeta(long id)
        {
            var meta = await _metaService.GetMetaByIdAsync(id);
            if (meta == null)
            {
                return NotFound(new { message = $"Meta com ID {id} não encontrada." });
            }
            return Ok(meta);
        }

        // GET: api/Metas/usuario/1
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<MetaDTO>>> GetMetasByUsuarioId(long usuarioId)
        {
            var metas = await _metaService.GetMetasByUsuarioIdAsync(usuarioId);
            return Ok(metas);
        }

        // GET: api/Metas/active/1
        [HttpGet("active/{usuarioId}")]
        public async Task<ActionResult<MetaDTO>> GetActiveMeta(long usuarioId)
        {
            var meta = await _metaService.GetActiveMetaAsync(usuarioId);
            if (meta == null)
            {
                return NotFound(new { message = $"Nenhuma meta ativa encontrada para o usuário com ID {usuarioId}." });
            }
            return Ok(meta);
        }

        // POST: api/Metas
        [HttpPost]
        public async Task<ActionResult<MetaDTO>> CreateMeta(MetaDTO metaDTO)
        {
            var novaMeta = await _metaService.CreateMetaAsync(metaDTO);
            return CreatedAtAction(nameof(GetMeta), new { id = novaMeta.Id }, novaMeta);
        }

        // PUT: api/Metas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeta(long id, MetaDTO metaDTO)
        {
            if (id != metaDTO.Id)
            {
                return BadRequest(new { message = "ID da meta na URL não corresponde ao ID no corpo da requisição." });
            }

            await _metaService.UpdateMetaAsync(id, metaDTO);
            return NoContent();
        }

        // DELETE: api/Metas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeta(long id)
        {
            await _metaService.DeleteMetaAsync(id);
            return NoContent();
        }
    }
}
