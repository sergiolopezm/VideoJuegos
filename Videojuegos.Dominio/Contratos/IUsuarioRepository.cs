using Videojuegos.Dominio.DTO;
using Videojuegos.Infrastructura.Entities.Videojuegos;

namespace Videojuegos.Dominio.Contratos
{
    public interface IUsuarioRepository
    {
        Task<Usuario> FindByUsernameAsync(string username);
        Task<Usuario> FindByEmailAsync(string email);
        Task<Respuesta> CreateAsync(Usuario usuario);
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}
