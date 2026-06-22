using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCloudApi.Models
{
    public class Vacuna
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreVacuna { get; set; } = string.Empty;

        public DateTime FechaAplicacion { get; set; } = DateTime.Now;

        // NUEVO: Fecha del próximo refuerzo (opcional)
        public DateTime? FechaProximaDosis { get; set; }

        // Relación con la mascota que recibe la vacuna
        [Required]
        public int MascotaId { get; set; }
        [ForeignKey("MascotaId")]
        public Mascota? Mascota { get; set; }

        // Relación con el veterinario que aplicó la vacuna
        [Required]
        public int VeterinarioId { get; set; }
        [ForeignKey("VeterinarioId")]
        public Usuario? Veterinario { get; set; }
    }
}