using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCloudApi.Models
{
    public class RegistroSanitario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MascotaId { get; set; }
        [ForeignKey("MascotaId")]
        public Mascota? Mascota { get; set; }

        [Required]
        public int VacunaId { get; set; }
        [ForeignKey("VacunaId")]
        public Vacuna? Vacuna { get; set; }

        [Required]
        public DateTime FechaAplicacion { get; set; }

        [Required]
        [StringLength(100)]
        public string Veterinario { get; set; } = string.Empty;

        public string Observaciones { get; set; } = string.Empty;
    }
}