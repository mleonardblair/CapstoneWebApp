using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Server.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public byte[]? Image { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();//default empty list

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
