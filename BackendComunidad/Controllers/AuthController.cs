using BackendCom.Contexts;
using BackendCom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login == null)
                return BadRequest("Datos inválidos");

            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u =>
                    u.Email == login.Email &&
                    u.Estado == "Activo"
                );

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            // Verificar contraseña con BCrypt
            bool passwordValida = BCrypt.Net.BCrypt.Verify(
                login.PassHash,      // contraseña que escribe el usuario
                usuario.PassHash     // hash guardado en la BD
            );

            if (!passwordValida)
                return Unauthorized("Credenciales incorrectas");

            var token = GenerateToken(usuario);

            return Ok(new { token });
        }


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
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(jwt["ExpireMinutes"]!)
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [HttpPost("crear")]
        public async Task<IActionResult> PostUsuarioCrear([FromBody] Usuario usuario)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(usuario.Cedula)) return BadRequest("La cédula es obligatoria");
            if (string.IsNullOrWhiteSpace(usuario.Nombres)) return BadRequest("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(usuario.Apellidos)) return BadRequest("El apellido es obligatorio");
            if (string.IsNullOrWhiteSpace(usuario.Email)) return BadRequest("El correo es obligatorio");
            if (string.IsNullOrWhiteSpace(usuario.PassHash)) return BadRequest("La contraseña es obligatoria");

            if (await _context.Usuarios.AnyAsync(u => u.Email.ToLower() == usuario.Email.ToLower()))
                return BadRequest("El correo ya está registrado");

            if (await _context.Usuarios.AnyAsync(u => u.Cedula == usuario.Cedula))
                return BadRequest("La cédula ya está registrada");

            // Hash
            usuario.PassHash = BCrypt.Net.BCrypt.HashPassword(usuario.PassHash);

            // Resto de campos
            usuario.RolId = 3;
            usuario.Estado = "Activo";
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaModificacion = null;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario creado correctamente" });
        }


    }
}
