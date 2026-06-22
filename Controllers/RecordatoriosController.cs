using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;
using PetCloudApi.Models;

namespace PetCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordatoriosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecordatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recordatorios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recordatorio>>> GetRecordatorios()
        {
            return await _context.Recordatorios.ToListAsync();
        }

        // GET: api/Recordatorios/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Recordatorio>> GetRecordatorio(int id)
        {
            var recordatorio = await _context.Recordatorios.FindAsync(id);

            if (recordatorio == null)
            {
                return NotFound(new { mensaje = "No existe un recordatorio con el ID indicado." });
            }

            return Ok(recordatorio);
        }

        // POST: api/Recordatorios
        [HttpPost]
        public async Task<ActionResult<Recordatorio>> PostRecordatorio(Recordatorio recordatorio)
        {
            // Verificamos que la mascota exista antes de crearle un recordatorio
            var mascotaExiste = await _context.Mascotas.AnyAsync(m => m.Id == recordatorio.MascotaId);
            if (!mascotaExiste)
            {
                return BadRequest(new { mensaje = "No se puede crear el recordatorio porque la mascota no existe." });
            }

            _context.Recordatorios.Add(recordatorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecordatorio), new { id = recordatorio.Id }, recordatorio);
        }

        // PUT: api/Recordatorios/1 (Para actualizar un recordatorio)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecordatorio(int id, Recordatorio recordatorio)
        {
            if (id != recordatorio.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del cuerpo de la petición." });
            }

            _context.Entry(recordatorio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _context.Recordatorios.AnyAsync(e => e.Id == id);
                if (!existe) return NotFound(new { mensaje = "No existe un recordatorio con el ID indicado." });
                else throw;
            }

            return Ok(recordatorio); // Devuelve el recordatorio actualizado
        }

        // DELETE: api/Recordatorios/1 (Para borrar un recordatorio)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordatorio(int id)
        {
            var recordatorio = await _context.Recordatorios.FindAsync(id);
            if (recordatorio == null)
            {
                return NotFound(new { mensaje = "No existe un recordatorio con el ID indicado." });
            }

            _context.Recordatorios.Remove(recordatorio);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 (operación exitosa, pero sin datos que mostrar)
        }
    }
}