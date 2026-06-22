using System.ComponentModel.DataAnnotations;

namespace PetCloudApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        // Aquí definimos el Rol: "Dueño", "Veterinario", "Municipio", "Protectora"
        public string Rol { get; set; } = string.Empty; 
    }
}