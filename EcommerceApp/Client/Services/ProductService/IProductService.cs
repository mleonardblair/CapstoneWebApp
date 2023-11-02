using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.ProductService
{
    /// <summary>
    /// Responsible for interacting with the user interface, managing client-side state, and making HTTP calls to the server.
    /// </summary>
    public interface IProductService
    {
        event Action ProductsChanged;
        List<ProductDto> Products { get; set; }
        ProductDto Product { get; set; }
        string Message { get; set; }
        Task SearchProducts(string searchQuery);
        Task<List<string>> GetProductSearchSuggestions(string searchQuery);
        Task GetAllProductsAsync(Guid? categoryId = null);
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId);
        Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, Stream imageFile);
        Task<ServiceResponse<ProductDto>> GetProductsByCategoryId(Guid categoryId);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto product);
        Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(ProductDto product);
        Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId);
    }
}
