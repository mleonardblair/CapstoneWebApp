using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        public string ImageURI { get; set; }
        public string ImagesJson { get; set; } = JsonConvert.SerializeObject(new string[0]);
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;

        [NotMapped]
        public string[] Images
        {
            get
            {
                if (string.IsNullOrEmpty(ImagesJson))
                {
                    return new string[0];
                }

                var images = JsonConvert.DeserializeObject<string[]>(ImagesJson);
                return images ?? new string[0]; // Return an empty array if deserialization results in null
            }
            set => ImagesJson = value is null ? string.Empty : JsonConvert.SerializeObject(value);
        }



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
