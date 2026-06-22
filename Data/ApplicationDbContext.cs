using Microsoft.EntityFrameworkCore;
using PetCloudApi.Models;

namespace PetCloudApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Aquí definimos cuáles modelos se convertirán en tablas en la BD
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }

        //tabla de Vacunas
        public DbSet<Vacuna> Vacunas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vacuna>()
                .HasOne(v => v.Veterinario)
                .WithMany()
                .HasForeignKey(v => v.VeterinarioId)
                .OnDelete(DeleteBehavior.Restrict); // Esto apaga el borrado en cascada para el veterinario
        }
    }
}