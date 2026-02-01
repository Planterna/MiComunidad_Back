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
    public class TipoRecursoesController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public TipoRecursoesController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/TipoRecursoes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TipoRecurso>>> GetTipoRecursos()
        {
            return await _context.TipoRecursos.ToListAsync();
        }

        // GET: api/TipoRecursoes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TipoRecurso>> GetTipoRecurso(int id)
        {
            var tipoRecurso = await _context.TipoRecursos.FindAsync(id);

            if (tipoRecurso == null)
            {
                return NotFound();
            }

            return tipoRecurso;
        }

        // PUT: api/TipoRecursoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutTipoRecurso(int id, TipoRecurso tipoRecurso)
        {
            if (id != tipoRecurso.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoRecurso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoRecursoExists(id))
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

        // POST: api/TipoRecursoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<TipoRecurso>> PostTipoRecurso(TipoRecurso tipoRecurso)
        {
            _context.TipoRecursos.Add(tipoRecurso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoRecurso", new { id = tipoRecurso.Id }, tipoRecurso);
        }

        // DELETE: api/TipoRecursoes/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTipoRecurso(int id)
        {
            var tipoRecurso = await _context.TipoRecursos.FindAsync(id);
            if (tipoRecurso == null)
            {
                return NotFound();
            }

            _context.TipoRecursos.Remove(tipoRecurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoRecursoExists(int id)
        {
            return _context.TipoRecursos.Any(e => e.Id == id);
        }
    }
}
