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
    public class ReservasController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public ReservasController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        //GET: api/Reservas/Full
        [HttpGet("Full")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<IEnumerable<ReservasDTO>>> GetReservasFull()
        {
            return await _context.ReservasDTO
            .FromSqlRaw("EXEC sp_Reservas_Data_Full").ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
