
namespace Videojuegos.Dominio.DTO.UsuariosToken
{
    public class UsuariosTokenArgs
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
