using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;

namespace PetCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Alertas/Dueno/2
        [HttpGet("Dueno/{duenoId}")]
        public async Task<IActionResult> GetAlertas(int duenoId)
        {
            // Calculamos la fecha límite (hoy + 30 días)
            var fechaLimite = DateTime.Now.AddDays(30);

            var vacunasPorVencer = await _context.Vacunas
                .Include(v => v.Mascota)
                .Where(v => v.Mascota.DuenoId == duenoId 
                         && v.FechaProximaDosis != null 
                         && v.FechaProximaDosis <= fechaLimite)
                .Select(v => new {
                    Mascota = v.Mascota.Nombre,
                    Vacuna = v.NombreVacuna,
                    FechaAplicacion = v.FechaAplicacion,
                    ProximaDosis = v.FechaProximaDosis,
                    // Calculamos cuántos días faltan
                    DiasRestantes = (v.FechaProximaDosis.Value - DateTime.Now).Days
                })
                .ToListAsync();

            if (!vacunasPorVencer.Any())
            {
                return Ok(new { Mensaje = "¡Genial! No hay vacunas próximas a vencer." });
            }

            return Ok(vacunasPorVencer);
        }
    }
}