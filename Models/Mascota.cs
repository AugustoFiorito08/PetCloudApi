using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCloudApi.Models
{
    public class Mascota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Especie { get; set; } = string.Empty; // Perro, Gato, Ave, etc.

        [StringLength(50)]
        public string Raza { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        // Relación con el Dueño (Clave Foránea)
        [Required]
        public int DuenoId { get; set; }
        
        [ForeignKey("DuenoId")]
        public Usuario? Dueno { get; set; }
    }
}