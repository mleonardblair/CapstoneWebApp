using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.ProductTagService
{
    public interface IProductTagService
    {
            Task<ServiceResponse<ProductTagDto>> CreateProductTagAsync(ProductTagDto tag);
        }
    
}
