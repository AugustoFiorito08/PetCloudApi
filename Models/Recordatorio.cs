using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCloudApi.Models
{
    public class Recordatorio
    {
        [Key]
        public int Id { get; set; }

        // Vinculamos el recordatorio a una mascota específica
        [Required]
        public int MascotaId { get; set; }
        [ForeignKey("MascotaId")]
        public Mascota? Mascota { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }
    }
}