using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceApp.Shared.DTOs;

namespace EcommerceApp.Server.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid? ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
    }
}
