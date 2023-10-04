using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Shared.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
