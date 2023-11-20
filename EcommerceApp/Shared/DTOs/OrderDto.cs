using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Shared.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Discount { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
}
