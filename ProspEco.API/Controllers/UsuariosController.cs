using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Service;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] UsuarioRequest usuarioRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioService.AddUsuario(usuarioRequest);
                return Created("", "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar usuários: {ex.Message}");
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUsuarioById(long id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioById(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUsuario(long id, [FromBody] UsuarioRequest usuarioRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioService.UpdateUsuario(id, usuarioRequest);
                return NoContent(); // Atualização bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            try
            {
                await _usuarioService.DeleteUsuario(id);
                return NoContent(); // Exclusão bem-sucedida
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
