using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        // Número de ítem, único para cada ítem
        [Required(ErrorMessage = "Item Number is required")]
        [StringLength(20, ErrorMessage = "Item Number cannot exceed 20 characters")]
        public string ItemNumber { get; set; }

        // Nombre del ítem
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        // Descripción del ítem
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        // Precio por defecto del ítem
        [Required(ErrorMessage = "Default Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Default Price must be greater than zero")]
        public decimal DefaultPrice { get; set; }

        // Categoría del ítem (puede ser un enum o una cadena)
        [Required(ErrorMessage = "Item Category is required")]
        public string ItemCategory { get; set; }

        // Estado del ítem (activo o inactivo)
        [Required]
        public bool Status { get; set; } = true;  // Por defecto activo
    }
}
