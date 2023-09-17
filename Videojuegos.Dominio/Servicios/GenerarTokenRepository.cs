using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Videojuegos.Dominio.Contratos;
using Videojuegos.Dominio.DTO.UsuariosToken;
using Videojuegos.Infrastructura.Entities.Videojuegos;

public class GenerarTokenRepository : IGenerarTokenRepository
{
    private readonly IConfiguration _configuration;
    private readonly VideojuegosDbContext _context;
    private readonly ILogger<GenerarTokenRepository> _logger;

    public GenerarTokenRepository(ILogger<GenerarTokenRepository> logger, IConfiguration configuration, VideojuegosDbContext context)
    {
        _configuration = configuration;
        _context = context;
        _logger = logger;
    }

    public string GenerateJwtToken(UsuariosTokenArgs user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Username),
        new Claim(ClaimTypes.Name, user.Username),

    };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public UsuariosTokenArgs FindByUsernameAndPassword(string username, string password)
    {
        var usuario = _context.UsuariosTokens.FirstOrDefault(u => u.Username == username && u.Password == password && u.Rol == "Admin");
        var usuarionorol = _context.UsuariosTokens.FirstOrDefault(u => u.Username == username && u.Password == password && u.Rol != "Admin");

        if (usuario != null)
        {
            var usuarioArgs = new UsuariosTokenArgs
            {
                Username = usuario.Username,
                Email = usuario.Email,
                Password = usuario.Password,
                Rol = usuario.Rol
            };

            return usuarioArgs;
        }
        else if (usuarionorol != null)
        {
            var usuarionorolArgs = new UsuariosTokenArgs
            {
                Username = usuarionorol.Username,
                Email = usuarionorol.Email,
                Password = usuarionorol.Password,
                Rol = usuarionorol.Rol
            };

            return usuarionorolArgs;
        }

        return null;
    }
}