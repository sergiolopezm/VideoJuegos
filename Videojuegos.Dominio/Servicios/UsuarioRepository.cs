
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Videojuegos.Dominio.Contratos;
using Videojuegos.Dominio.DTO;
using Videojuegos.Infrastructura.Entities.Videojuegos;

namespace Videojuegos.Dominio.Servicios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly VideojuegosDbContext _context;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(ILogger<UsuarioRepository> logger,  VideojuegosDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Usuario> FindByUsernameAsync(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Usuario> FindByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return false; // El usuario no existe
            }

            return user.Password == password;
        }

        public async Task<Respuesta> CreateAsync(Usuario usuario)
        {
            try
            {
                // Agregar el usuario al contexto
                _context.Usuarios.Add(usuario);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return new Respuesta { Exito = true, Mensaje = "Éxito en la creación del usuario", Salida_respuesta = null };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creación de Usuario");
                return new Respuesta
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Salida_respuesta = null
                };
            }
        }
    }
}