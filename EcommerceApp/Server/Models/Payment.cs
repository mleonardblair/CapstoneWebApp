using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Server.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required, MaxLength(25)]
        public string TransactionId { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public PaymentStatus Status { get; set; }


        [Required, MaxLength(25)]
        public string PaymentMethod { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}
