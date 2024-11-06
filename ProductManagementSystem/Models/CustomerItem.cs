using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Models
{
    public class CustomerItem
    {
        [Key]
        public int Id { get; set; }

        // Clave foránea para Customer
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Clave foránea para Item
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
