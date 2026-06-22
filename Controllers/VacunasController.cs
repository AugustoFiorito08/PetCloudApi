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

        // GET: api/Vacunas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacuna>>> GetVacunas()
        {
            return await _context.Vacunas.ToListAsync();
        }

        // GET: api/Vacunas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacuna>> GetVacuna(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);

            if (vacuna == null)
            {
                return NotFound(new { mensaje = "No existe una vacuna con el ID indicado." });
            }

            return vacuna;
        }

        // POST: api/Vacunas
        [HttpPost]
        public async Task<ActionResult<Vacuna>> PostVacuna(Vacuna vacuna)
        {
            _context.Vacunas.Add(vacuna);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacuna), new { id = vacuna.Id }, vacuna);
        }
    }
}