using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;
using PetCloudApi.Models;

namespace PetCloudApi.Controllers
{
    // Esta será la URL de la API: localhost:puerto/api/Mascotas
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MascotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mascotas (Obtener todas las mascotas)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotas()
        {
            return await _context.Mascotas.ToListAsync();
        }

        // POST: api/Mascotas (Crear una nueva mascota)
        [HttpPost]
        public async Task<ActionResult<Mascota>> PostMascota(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            // Devuelve un código 201 (Creado) y los datos de la mascota
            return CreatedAtAction(nameof(GetMascotas), new { id = mascota.Id }, mascota);
        }

        // GET: api/Mascotas/1/Libreta
        [HttpGet("{id}/Libreta")]
        public async Task<IActionResult> GetLibretaSanitaria(int id)
        {
            // 1. Buscamos a la mascota y traemos los datos de su dueño
            var mascota = await _context.Mascotas
                .Include(m => m.Dueno)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mascota == null)
            {
                return NotFound("No se encontró la mascota en el sistema.");
            }

            // 2. Buscamos todas las vacunas de esta mascota específica
            var vacunas = await _context.Vacunas
                .Include(v => v.Veterinario)
                .Where(v => v.MascotaId == id)
                .Select(v => new {
                    v.NombreVacuna,
                    v.FechaAplicacion,
                    VeterinarioQueAplico = v.Veterinario.Nombre // Extraemos solo el nombre del veterinario
                })
                .ToListAsync();

            // 3. Devolvemos la Libreta Digital unificada
            return Ok(new {
                Mascota = mascota.Nombre,
                Especie = mascota.Especie,
                Raza = mascota.Raza,
                Dueno = mascota.Dueno.Nombre,
                HistorialDeVacunas = vacunas
            });
        }
    }
}