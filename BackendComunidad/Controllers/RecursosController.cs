using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendCom.Contexts;
using BackendCom.Models;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> Agregar archivos de proyecto.

namespace BackendComunidad.Controllers
{
    [Route("api/[controller]")]
<<<<<<< HEAD
=======
    [Authorize]
>>>>>>> Agregar archivos de proyecto.
    [ApiController]
    public class RecursosController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public RecursosController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/Recursoes
        [HttpGet]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<IEnumerable<Recurso>>> GetRecursos()
        {
            return await _context.Recursos.ToListAsync();
        }

        // GET: api/Recursoes/5
        [HttpGet("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Recurso>> GetRecurso(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);

            if (recurso == null)
            {
                return NotFound();
            }

            return recurso;
        }

        // PUT: api/Recursoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> PutRecurso(int id, Recurso recurso)
        {
            if (id != recurso.Id)
            {
                return BadRequest();
            }

            _context.Entry(recurso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursoExists(id))
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

        // POST: api/Recursoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Recurso>> PostRecurso(Recurso recurso)
        {
            _context.Recursos.Add(recurso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecurso", new { id = recurso.Id }, recurso);
        }

        // DELETE: api/Recursoes/5
        [HttpDelete("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> DeleteRecurso(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }

            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecursoExists(int id)
        {
            return _context.Recursos.Any(e => e.Id == id);
        }
    }
}
