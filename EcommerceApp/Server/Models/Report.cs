using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceApp.Shared.DTOs;

namespace EcommerceApp.Server.Models
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        public DateTime TimePeriodStart { get; set; }
        public DateTime TimePeriodEnd { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalRevenue { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalTax { get; set; }
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalDiscount { get; set; }
    }
}
