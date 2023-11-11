using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs
{
    public class ProductPaginationResponse
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        // is the total number of pages available, which is calculated by dividing the TotalCount by the number of items per page and rounding up.

        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
