using Microsoft.EntityFrameworkCore;
using PetCloudApi.Models;

namespace PetCloudApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
        public DbSet<RegistroSanitario> RegistrosSanitarios { get; set; } // ¡Nueva tabla!
        // ¡Nueva tabla de Recordatorios!
        public DbSet<Recordatorio> Recordatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Ya no necesitamos la regla compleja de borrado porque simplificamos
            // el veterinario a un simple texto (string) en el registro sanitario
            // según las especificaciones de tu PDF.
        }
    }
}