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
    public class NoticiasController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public NoticiasController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/Noticias
        [HttpGet]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticias()
        {
            return await _context.Noticias.ToListAsync();
        }

        // GET: api/Noticias/5
        [HttpGet("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Noticia>> GetNoticia(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);

            if (noticia == null)
            {
                return NotFound();
            }

            return noticia;
        }

        // PUT: api/Noticias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> PutNoticia(int id, Noticia noticia)
        {
            if (id != noticia.Id)
            {
                return BadRequest();
            }

            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // POST: api/Noticias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Noticia>> PostNoticia(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoticia", new { id = noticia.Id }, noticia);
        }

        // DELETE: api/Noticias/5
        [HttpDelete("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }
    }
}
