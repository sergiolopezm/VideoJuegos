using Videojuegos.Dominio.DTO.UsuariosToken;

namespace Videojuegos.Dominio.Contratos
{
    public interface IGenerarTokenRepository
    {
        string GenerateJwtToken(UsuariosTokenArgs user);
        public UsuariosTokenArgs FindByUsernameAndPassword(string username, string password);
    }

}
