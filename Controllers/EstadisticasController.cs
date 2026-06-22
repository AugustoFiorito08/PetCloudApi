using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;

namespace PetCloudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Estadisticas/Resumen
        // Este endpoint es ideal para la pantalla principal del Municipio
        [HttpGet("Resumen")]
        public async Task<IActionResult> GetResumenGeneral()
        {
            var totalMascotas = await _context.Mascotas.CountAsync();
            var totalVacunas = await _context.Vacunas.CountAsync();
            
            // Contamos los usuarios según su rol
            var totalDuenos = await _context.Usuarios.CountAsync(u => u.Rol == "Dueño");
            var totalVeterinarios = await _context.Usuarios.CountAsync(u => u.Rol == "Veterinario");

            return Ok(new {
                TotalMascotasRegistradas = totalMascotas,
                TotalVacunasAplicadas = totalVacunas,
                DuenosActivos = totalDuenos,
                VeterinariosAdheridos = totalVeterinarios
            });
        }

        // GET: api/Estadisticas/MascotasPorEspecie
        // Este endpoint agrupa las mascotas para hacer gráficos circulares (ej: 80% perros, 20% gatos)
        [HttpGet("MascotasPorEspecie")]
        public async Task<IActionResult> GetMascotasPorEspecie()
        {
            var estadisticas = await _context.Mascotas
                .GroupBy(m => m.Especie)
                .Select(grupo => new {
                    Especie = grupo.Key,
                    Cantidad = grupo.Count()
                })
                .ToListAsync();

            return Ok(estadisticas);
        }

        // GET: api/Estadisticas/VacunasDelMes
        // Mide el éxito de las campañas de vacunación en el mes actual
        [HttpGet("VacunasDelMes")]
        public async Task<IActionResult> GetVacunasDelMes()
        {
            var hoy = DateTime.Now;

            // 1. Filtramos las vacunas aplicadas exactamente en el mes y año en curso
            var vacunasEsteMes = await _context.Vacunas
                .Where(v => v.FechaAplicacion.Month == hoy.Month && v.FechaAplicacion.Year == hoy.Year)
                .ToListAsync();

            // 2. Agrupamos para saber qué tipo de vacunas se dieron más
            var detallePorVacuna = vacunasEsteMes
                .GroupBy(v => v.NombreVacuna)
                .Select(grupo => new {
                    Vacuna = grupo.Key,
                    Cantidad = grupo.Count()
                });

            // 3. Devolvemos el reporte armado
            return Ok(new {
                Periodo = hoy.ToString("MMMM yyyy"), // Ejemplo: "junio 2026"
                TotalAplicadas = vacunasEsteMes.Count,
                Detalle = detallePorVacuna
            });
        }
    }
}