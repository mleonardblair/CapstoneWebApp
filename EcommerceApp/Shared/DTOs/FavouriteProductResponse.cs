using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs
{
    public class FavouriteProductResponse
    {
        // From Favourite Model
        public Guid FavouriteId { get; set; }
        public DateTime DateAddedToFavourite { get; set; }

        // From Product Model
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageURI { get; set; }
        public int ProductStockQuantity { get; set; }
        public string[] ProductImages { get; set; }
    }
}
