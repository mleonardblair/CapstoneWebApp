using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs
{
    public class OrderDetailsProductResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ImageURI { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
