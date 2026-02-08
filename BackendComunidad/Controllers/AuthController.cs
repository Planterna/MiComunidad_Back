using BackendCom.Contexts;
using BackendCom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendComunidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ComunidadContext _context;

        public AuthController(IConfiguration configuration, ComunidadContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // ======================
        // DTOs
        // ======================
        public class ResetPassDto
        {
            public string Email { get; set; }
            public string NewPassword { get; set; }
        }

        // ======================
        // LOGIN
        // ======================
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login == null)
                return BadRequest("Datos inválidos");

            var email = (login.Email ?? "").Trim().ToLower();
            var pass = (login.PassHash ?? "").Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
                return BadRequest("Email y contraseña son obligatorios");

            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u =>
                    u.Email.ToLower() == email &&
                    u.Estado == "Activo"
                );

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            // Si el PassHash no es BCrypt válido, NO intentes Verify (evita Invalid salt version)
            if (string.IsNullOrWhiteSpace(usuario.PassHash) || !usuario.PassHash.StartsWith("$2"))
                return Unauthorized("Credenciales incorrectas");

            bool passwordValida;
            try
            {
                passwordValida = BCrypt.Net.BCrypt.Verify(pass, usuario.PassHash);
            }
            catch
            {
                return Unauthorized("Credenciales incorrectas");
            }

            if (!passwordValida)
                return Unauthorized("Credenciales incorrectas");

            var token = GenerateToken(usuario);
            return Ok(new { token });
        }

        // ======================
        // GENERATE TOKEN
        // ======================
        private string GenerateToken(Usuario usuario)
        {
            var jwt = _configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("nombre", usuario.Nombres),
                new Claim("apellido", usuario.Apellidos),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "User")
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpireMinutes"]!)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

     

       
    }
}
