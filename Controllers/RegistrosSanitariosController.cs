using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;
using PetCloudApi.Models;

namespace PetCloudApi.Controllers
{
    [ApiController]
    public class RegistrosSanitariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistrosSanitariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/mascotas/{id}/registros-sanitarios
        [HttpGet("api/mascotas/{id}/registros-sanitarios")]
        public async Task<ActionResult<IEnumerable<object>>> GetHistorialMascota(int id)
        {
            var existeMascota = await _context.Mascotas.AnyAsync(m => m.Id == id);
            if (!existeMascota) return NotFound(new { mensaje = "No existe una mascota con el ID indicado." });

            var registros = await _context.RegistrosSanitarios
                .Where(r => r.MascotaId == id)
                .Select(r => new {
                    id = r.Id,
                    mascotaId = r.MascotaId,
                    vacunaId = r.VacunaId,
                    fechaAplicacion = r.FechaAplicacion.ToString("yyyy-MM-dd"),
                    veterinario = r.Veterinario,
                    observaciones = r.Observaciones
                })
                .ToListAsync();

            return Ok(registros);
        }

        // GET: /api/registros-sanitarios/{id}
        [HttpGet("api/registros-sanitarios/{id}")]
        public async Task<ActionResult<object>> GetRegistroSanitario(int id)
        {
            var registro = await _context.RegistrosSanitarios.FindAsync(id);

            if (registro == null) return NotFound(new { mensaje = "No existe un registro con el ID indicado." });

            return Ok(new {
                id = registro.Id,
                mascotaId = registro.MascotaId,
                vacunaId = registro.VacunaId,
                fechaAplicacion = registro.FechaAplicacion.ToString("yyyy-MM-dd"),
                veterinario = registro.Veterinario,
                observaciones = registro.Observaciones
            });
        }

        // POST: /api/mascotas/{id}/registros-sanitarios
        [HttpPost("api/mascotas/{id}/registros-sanitarios")]
        public async Task<ActionResult<object>> PostRegistroSanitario(int id, RegistroSanitario registro)
        {
            var existeMascota = await _context.Mascotas.AnyAsync(m => m.Id == id);
            var existeVacuna = await _context.Vacunas.AnyAsync(v => v.Id == registro.VacunaId);

            if (!existeMascota || !existeVacuna) 
            {
                return NotFound(new { mensaje = "No existe la mascota o la vacuna con el ID especificado." });
            }

            registro.MascotaId = id; // Tomamos el ID de la mascota desde la URL
            
            _context.RegistrosSanitarios.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistroSanitario), new { id = registro.Id }, new {
                id = registro.Id,
                mascotaId = registro.MascotaId,
                vacunaId = registro.VacunaId,
                fechaAplicacion = registro.FechaAplicacion.ToString("yyyy-MM-dd"),
                veterinario = registro.Veterinario,
                observaciones = registro.Observaciones
            });
        }

        // DELETE: /api/registros-sanitarios/{id}
        [HttpDelete("api/registros-sanitarios/{id}")]
        public async Task<IActionResult> DeleteRegistroSanitario(int id)
        {
            var registro = await _context.RegistrosSanitarios.FindAsync(id);
            if (registro == null) return NotFound(new { mensaje = "No existe un registro con el ID indicado." });

            _context.RegistrosSanitarios.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent(); // Código 204
        }
    }
}