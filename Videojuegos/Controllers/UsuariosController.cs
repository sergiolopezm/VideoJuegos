using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videojuegos.Dominio.Contratos;
using Videojuegos.Dominio.DTO;
using Videojuegos.Dominio.DTO.Usuarios;
using Videojuegos.Dominio.Servicios;
using Videojuegos.Infrastructura.Entities.Videojuegos;

namespace Videojuegos.Controllers
{
    [Authorize(Policy = "PoliticaCreacion")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuariosController(ILogger<UsuarioRepository> logger, IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
                
        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioArgs usuarioArgs)
        {
            if (usuarioArgs == null)
            {
                return BadRequest(new Respuesta
                {
                    Exito = false,
                    Mensaje = "Datos de registro no válidos"
                });
            }

            var usuario = await _usuarioRepository.FindByUsernameAsync(usuarioArgs.Username);

            if (usuario != null)
            {
                return BadRequest(new Respuesta
                {
                    Exito = false,
                    Mensaje = "El nombre de usuario ya está en uso"
                });
            }

            usuario = await _usuarioRepository.FindByEmailAsync(usuarioArgs.Email);

            if (usuario != null)
            {
                return BadRequest(new Respuesta
                {
                    Exito = false,
                    Mensaje = "La dirección de correo electrónico ya está en uso"
                });
            }

            var nuevoUsuario = new Usuario
            {
                Username = usuarioArgs.Username,
                Email = usuarioArgs.Email,
                Password = usuarioArgs.Password 
            };

            var resultadoCreacion = await _usuarioRepository.CreateAsync(nuevoUsuario);

            if (resultadoCreacion.Exito)
            {
                return Ok(new Respuesta
                {
                    Exito = true,
                    Mensaje = "Usuario registrado exitosamente.",
                    Salida_respuesta = null
                });
            }
            else
            {
                
                return BadRequest(new Respuesta
                {
                    Exito = false,
                    Mensaje = "No se pudo crear el usuario",
                    Salida_respuesta = resultadoCreacion.Salida_respuesta 
                });
            }
        }


    }
}
