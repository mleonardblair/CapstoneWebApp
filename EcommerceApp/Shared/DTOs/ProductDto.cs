using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Shared.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "A name is required."), MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "A description is required."),]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Selecting a price is required."), Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "An image is required.")]
        public string ImageURI { get; set; } = string.Empty;
        public string[] Images { get; set; } = new string[0];
        public bool isFavourite { get; set; } = false;
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [Required(ErrorMessage = "Selecting a category is required.")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "An item quantity is required.")]
        public int StockQuantity { get; set; }
        public ICollection<ProductTagDto> ProductTags { get; set; } = new List<ProductTagDto>();
        public List<string> TagNames { get; set; } = new List<string>();
    }
}
