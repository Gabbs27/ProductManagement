using ProductManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class CustomerItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required(ErrorMessage = "Please select an item.")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }


 
}