using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly IMetaService _metaService;

        public MetaController(IMetaService metaService)
        {
            _metaService = metaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMeta([FromBody] MetaRequest metaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _metaService.AddMeta(metaRequest);
                return Created("", "Meta criada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar meta: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMetas()
        {
            try
            {
                var metas = await _metaService.GetAllMetas();
                return Ok(metas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar metas: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetMetaById(long id)
        {
            try
            {
                var meta = await _metaService.GetMetaById(id);
                return Ok(meta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateMeta(long id, [FromBody] MetaRequest metaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _metaService.UpdateMeta(id, metaRequest);
                return NoContent(); // Atualização bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar meta: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteMeta(long id)
        {
            try
            {
                await _metaService.DeleteMeta(id);
                return NoContent(); // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
