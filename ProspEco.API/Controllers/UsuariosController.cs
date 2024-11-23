using Microsoft.AspNetCore.Mvc;
using ProspEco.Model.DTOs;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(long id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = $"Usuário com ID {id} não encontrado." });
            }
            return Ok(usuario);
        }

        // GET: api/Usuarios/by-email/email@example.com
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioByEmail(string email)
        {
            var usuario = await _usuarioService.GetUsuarioByEmailAsync(email);
            if (usuario == null)
            {
                return NotFound(new { message = $"Usuário com email {email} não encontrado." });
            }
            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CreateUsuario(UsuarioDTO usuarioDTO)
        {
            var novoUsuario = await _usuarioService.CreateUsuarioAsync(usuarioDTO);
            return CreatedAtAction(nameof(GetUsuario), new { id = novoUsuario.Id }, novoUsuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(long id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            {
                return BadRequest(new { message = "ID do usuário na URL não corresponde ao ID no corpo da requisição." });
            }

            await _usuarioService.UpdateUsuarioAsync(id, usuarioDTO);
            return NoContent();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }
    }
}
