using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

     
       [Required]
        public bool IsActive { get; set; } = true;  // Por defecto activo

        // Nueva propiedad para el número de teléfono
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string? PhoneNumber { get; set; }

        public ICollection<CustomerItem> CustomerItems { get; set; } = new List<CustomerItem>();
    }
}
