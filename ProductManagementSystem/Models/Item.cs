using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }  // Nueva propiedad
    }
}
