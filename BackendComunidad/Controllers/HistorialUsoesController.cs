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
    [Authorize]
    [ApiController]
    public class HistorialUsoesController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public HistorialUsoesController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/HistorialUsoes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HistorialUso>>> GetHistorialUsos()
        {
            return await _context.HistorialUsos.ToListAsync();
        }

        // GET: api/HistorialUsoes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> PutHistorialUso(int id, HistorialUso historialUso)
        {
            if (id != historialUso.Id)
            {
                return BadRequest();
            }

            _context.Entry(historialUso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialUsoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HistorialUsoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<HistorialUso>> PostHistorialUso(HistorialUso historialUso)
        {
            _context.HistorialUsos.Add(historialUso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialUso", new { id = historialUso.Id }, historialUso);
        }

        // DELETE: api/HistorialUsoes/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
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
