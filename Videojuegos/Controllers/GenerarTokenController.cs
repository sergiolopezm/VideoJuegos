using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videojuegos.Dominio.Contratos;
using Videojuegos.Dominio.DTO.UsuariosToken;
using Videojuegos.Infrastructura.Entities.Videojuegos;

namespace Videojuegos.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GenerarTokenController : ControllerBase
    {
        private readonly IGenerarTokenRepository _generarTokenRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GenerarTokenController(IGenerarTokenRepository generarTokenRepository, IUsuarioRepository usuarioRepository)
        {
            _generarTokenRepository = generarTokenRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("generar")]
        public IActionResult GenerarToken([FromBody] UsuariosTokenArgs usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Los datos de usuario son inválidos.");
            }

            var usuarioExistente = _generarTokenRepository.FindByUsernameAndPassword(usuario.Username, usuario.Password);
            
            if (usuarioExistente.Rol != "Admin")
            {
                return Unauthorized("No tienes permiso para acceder a esta función.");
            }

            if (usuarioExistente == null)
            {
                return Unauthorized("Credenciales inválidas. La autenticación falló.");
            }

            string jwtToken = _generarTokenRepository.GenerateJwtToken(new UsuariosTokenArgs
            {
                Username = usuario.Username,
                Email = usuarioExistente.Email,
                Password = usuario.Password,
                Rol = usuarioExistente.Rol
            });

            return Ok(new { Token = jwtToken });
        }
    }
}