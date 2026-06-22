using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;
using PetCloudApi.Models;

namespace PetCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacunasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VacunasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vacunas (Obtener todas las vacunas con datos extra)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacuna>>> GetVacunas()
        {
            // Usamos .Include() para traer los datos reales de la mascota y del veterinario, no solo el número de ID
            return await _context.Vacunas
                                 .Include(v => v.Mascota)
                                 .Include(v => v.Veterinario)
                                 .ToListAsync();
        }

        // POST: api/Vacunas (Simula el escaneo del QR y la carga de la vacuna)
        [HttpPost]
        public async Task<ActionResult<Vacuna>> PostVacuna(Vacuna vacuna)
        {
            _context.Vacunas.Add(vacuna);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacunas), new { id = vacuna.Id }, vacuna);
        }
    }
}