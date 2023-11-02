using System;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Shared.DTOs
{
    public class ProductTagDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; } 
    }
}
