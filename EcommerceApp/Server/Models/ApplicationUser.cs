using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using EcommerceApp.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Server.Models
{
    public class ApplicationUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public string Role { get; set; } = "Customer"; // default role

        public ShoppingCart ShoppingCart { get; set; }

        // Navigation properties for relationships
        public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
