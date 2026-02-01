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
    public class RolesController : ControllerBase
    {
        private readonly ComunidadContext _context;

        public RolesController(ComunidadContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> Agregar archivos de proyecto.
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
