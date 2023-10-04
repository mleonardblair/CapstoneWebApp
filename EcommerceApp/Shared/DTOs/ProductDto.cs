using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Shared.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public List<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
