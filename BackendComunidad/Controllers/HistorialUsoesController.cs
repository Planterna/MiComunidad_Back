using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendCom.Contexts;
using BackendCom.Models;

namespace BackendComunidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialUsosController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public HistorialUsosController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/HistorialUsoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialUso>>> GetHistorialUsos()
        {
            return await _context.HistorialUsos.Include(h=> h.Usuario)
                .Include(h => h.Recurso).ToListAsync();
        }

        // GET: api/HistorialUsoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialUso>> GetHistorialUso(int id)
        {
            var historialUso = await _context.HistorialUsos.FindAsync(id);

            if (historialUso == null)
            {
                return NotFound();
            }

            return historialUso;
        }

        // PUT: api/HistorialUsoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialUso(int id, HistorialUsoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id no coincide");

            var entity = await _context.HistorialUsos.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.UsuarioId = dto.UsuarioId;
            entity.RecursoId = dto.RecursoId;
            entity.FechaUso = DateOnly.Parse(dto.FechaUso);
            entity.HoraInicio = TimeOnly.Parse(dto.HoraInicio);
            entity.HoraFin = TimeOnly.Parse(dto.HoraFin);
            entity.Estado = dto.Estado;
            entity.Notas = dto.Notas;
            entity.Activo = dto.Activo;
            entity.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // POST: api/HistorialUsoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialUso>> PostHistorialUso(HistorialUsoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new HistorialUso
            {
                UsuarioId = dto.UsuarioId,
                RecursoId = dto.RecursoId,
                FechaUso = DateOnly.ParseExact(dto.FechaUso, "yyyy-MM-dd"),
                HoraInicio = TimeOnly.ParseExact(dto.HoraInicio, "HH:mm"),
                HoraFin = TimeOnly.ParseExact(dto.HoraFin, "HH:mm"),
                Estado = dto.Estado,
                Notas = dto.Notas,
                Activo = dto.Activo,
                FechaCreacion = DateTime.Now
            };

            _context.HistorialUsos.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorialUso), new { id = entity.Id }, entity);
        }


        // DELETE: api/HistorialUsoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialUso(int id)
        {
            var historialUso = await _context.HistorialUsos.FindAsync(id);
            if (historialUso == null)
            {
                return NotFound();
            }

            _context.HistorialUsos.Remove(historialUso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialUsoExists(int id)
        {
            return _context.HistorialUsos.Any(e => e.Id == id);
        }
    }
}
