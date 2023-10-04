using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.ProductService
{
    /// <summary>
    /// Responsible for interacting with the user interface, managing client-side state, and making HTTP calls to the server.
    /// </summary>
    public interface IProductService
    {
        List<ProductDto> Products { get; set; }
        ProductDto Product { get; set; }
        Task GetAllProductsAsync();
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId);
        Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, Stream imageFile);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto product);
        Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(ProductDto product);
        Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId);
    }
}
