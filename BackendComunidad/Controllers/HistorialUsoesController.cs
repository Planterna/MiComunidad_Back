using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendCom.Contexts;
using BackendCom.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackendComunidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistorialUsosController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public HistorialUsosController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/HistorialUsos
        [HttpGet]
        [Authorize(Roles = "Administrador,Encargado,Vecino")]
        public async Task<IActionResult> GetHistorialUsos()
        {
            var data = await _context.HistorialUsos
                .Include(h => h.Usuario)
                .Include(h => h.Recurso)
                .Select(h => new
                {
                    id = h.Id,
                    recursoId = h.RecursoId,
                    usuarioId = h.UsuarioId,
                    fechaUso = h.FechaUso.ToString("yyyy-MM-dd"),
                    horaInicio = h.HoraInicio.ToString("HH:mm"),
                    horaFin = h.HoraFin.ToString("HH:mm"),
                    estado = h.Estado,
                    notas = h.Notas,
                    activo = h.Activo ?? true,
                    fechaCreacion = h.FechaCreacion,
                    fechaModificacion = h.FechaModificacion,

                    // IMPORTANTE: usuario y recurso vienen con camelCase gracias a Program.cs
                    usuario = h.Usuario,
                    recurso = h.Recurso
                })
                .ToListAsync();

            return Ok(data);
        }

        // GET: api/HistorialUsos/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Encargado,Vecino")]
        public async Task<IActionResult> GetHistorialUso(int id)
        {
            var h = await _context.HistorialUsos
                .Include(x => x.Usuario)
                .Include(x => x.Recurso)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (h == null) return NotFound("No encontrado");

            return Ok(new
            {
                id = h.Id,
                recursoId = h.RecursoId,
                usuarioId = h.UsuarioId,
                fechaUso = h.FechaUso.ToString("yyyy-MM-dd"),
                horaInicio = h.HoraInicio.ToString("HH:mm"),
                horaFin = h.HoraFin.ToString("HH:mm"),
                estado = h.Estado,
                notas = h.Notas,
                activo = h.Activo ?? true,
                fechaCreacion = h.FechaCreacion,
                fechaModificacion = h.FechaModificacion,
                usuario = h.Usuario,
                recurso = h.Recurso
            });
        }

        // POST: api/HistorialUsos
        [HttpPost]
        [Authorize(Roles = "Administrador,Encargado,Vecino")]
        public async Task<IActionResult> PostHistorialUso([FromBody] HistorialUsoDto dto)
        {
            if (dto == null) return BadRequest("Body inválido");

            if (dto.UsuarioId <= 0) return BadRequest("UsuarioId inválido");
            if (dto.RecursoId <= 0) return BadRequest("RecursoId inválido");
            if (string.IsNullOrWhiteSpace(dto.Estado)) return BadRequest("Estado es obligatorio");

            var fechaTxt = (dto.FechaUso ?? "").Trim();
            if (fechaTxt.Contains("T")) fechaTxt = fechaTxt.Split('T')[0];

            if (!DateOnly.TryParseExact(fechaTxt, "yyyy-MM-dd", out var fecha))
                return BadRequest("FechaUso inválida, formato yyyy-MM-dd");

            var hi = (dto.HoraInicio ?? "").Trim();
            var hf = (dto.HoraFin ?? "").Trim();
            if (hi.Length >= 5) hi = hi.Substring(0, 5);
            if (hf.Length >= 5) hf = hf.Substring(0, 5);

            if (!TimeOnly.TryParseExact(hi, "HH:mm", out var horaInicio))
                return BadRequest("HoraInicio inválida, formato HH:mm");

            if (!TimeOnly.TryParseExact(hf, "HH:mm", out var horaFin))
                return BadRequest("HoraFin inválida, formato HH:mm");

            var entity = new HistorialUso
            {
                UsuarioId = dto.UsuarioId,
                RecursoId = dto.RecursoId,
                FechaUso = fecha,
                HoraInicio = horaInicio,
                HoraFin = horaFin,
                Estado = dto.Estado,
                Notas = dto.Notas,
                Activo = dto.Activo,
                FechaCreacion = DateTime.Now,
                FechaModificacion = null
            };

            _context.HistorialUsos.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Historial creado", id = entity.Id });
        }

        // PUT: api/HistorialUsos/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Encargado,Vecino")]
        public async Task<IActionResult> PutHistorialUso(int id, [FromBody] HistorialUsoDto dto)
        {
            if (dto == null) return BadRequest("Body inválido");
            if (id != dto.Id) return BadRequest("El id no coincide");

            var entity = await _context.HistorialUsos.FindAsync(id);
            if (entity == null) return NotFound("No encontrado");

            var fechaTxt = (dto.FechaUso ?? "").Trim();
            if (fechaTxt.Contains("T")) fechaTxt = fechaTxt.Split('T')[0];

            if (!DateOnly.TryParseExact(fechaTxt, "yyyy-MM-dd", out var fecha))
                return BadRequest("FechaUso inválida, formato yyyy-MM-dd");

            var hi = (dto.HoraInicio ?? "").Trim();
            var hf = (dto.HoraFin ?? "").Trim();
            if (hi.Length >= 5) hi = hi.Substring(0, 5);
            if (hf.Length >= 5) hf = hf.Substring(0, 5);

            if (!TimeOnly.TryParseExact(hi, "HH:mm", out var horaInicio))
                return BadRequest("HoraInicio inválida, formato HH:mm");

            if (!TimeOnly.TryParseExact(hf, "HH:mm", out var horaFin))
                return BadRequest("HoraFin inválida, formato HH:mm");

            entity.UsuarioId = dto.UsuarioId;
            entity.RecursoId = dto.RecursoId;
            entity.FechaUso = fecha;
            entity.HoraInicio = horaInicio;
            entity.HoraFin = horaFin;
            entity.Estado = dto.Estado;
            entity.Notas = dto.Notas;
            entity.Activo = dto.Activo;
            entity.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }



        // DELETE: api/HistorialUsoes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHistorialUso(int id)
        {
            var h = await _context.HistorialUsos.FindAsync(id);
            if (h == null) return NotFound();

            h.Activo = false;
            h.FechaModificacion = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialUsoExists(int id)
        {
            return _context.HistorialUsos.Any(e => e.Id == id);
        }
    }
}
