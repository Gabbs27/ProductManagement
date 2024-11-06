using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Propiedad de navegación para CustomerItems
        public ICollection<CustomerItem> CustomerItems { get; set; } = new List<CustomerItem>();
    }
}
