using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs
{
    public class CartProductResponse
    {
        public Guid Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Tags { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
