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

        
    }
}