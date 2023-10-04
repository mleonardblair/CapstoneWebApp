using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceApp.Shared.DTOs;

namespace EcommerceApp.Server.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }  = Guid.NewGuid();

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public decimal Discount { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment Payment { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

}
