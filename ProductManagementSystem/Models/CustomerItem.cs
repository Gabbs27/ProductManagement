using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Models
{
    public class CustomerItem
    {
        [Key]
        public int Id { get; set; }  // Clave primaria para el modelo

        // Clave foránea para el cliente
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  // Propiedad de navegación para el cliente

        // Clave foránea para el ítem
        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }  // Propiedad de navegación para el ítem
    }
}
