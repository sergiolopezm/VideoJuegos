using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; 
using System.Text;
using Videojuegos.Dominio.Contratos;
using Videojuegos.Dominio.Servicios;
using Videojuegos.Infrastructura.Entities.Videojuegos;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Se Agrega Cadena de Conexion a base de Datos 
builder.Services.AddDbContext<VideojuegosDbContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
});

// Configura el middleware de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

// Agrega Identity en el método ConfigureServices
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<VideojuegosDbContext>();

// Agregar servicios personalizados
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGenerarTokenRepository, GenerarTokenRepository>();

// Configurar las políticas de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7214") // Reemplaza con tus dominios permitidos
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configurar servicios de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PoliticaCreacion", policy =>
    {
        policy.RequireRole("Admin");
    });
});

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
    // Configura aquí cualquier otra información que desees mostrar en la documentación Swagger
});

var app = builder.Build();

// Configure el HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
        
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();  
app.UseAuthorization();   

app.MapControllers();

app.Run();