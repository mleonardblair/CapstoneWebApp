using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.ProductService
{
    /// <summary>
    /// Outlines a contract for interacting with the database, and returning data to the client.
    /// </summary>
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId);
        // Postman Debugging and Testing.
        Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, IFormFile imageFile);
        /*        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto productDto, Stream imageStream);*/
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto productDto);
        Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(Guid productId, ProductDto productDto);
        Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId);
        Task<ServiceResponse<List<ProductDto>>> GetProductsByCategoryId(Guid categoryId);
        Task<ServiceResponse<List<ProductDto>>> SearchProducts(string searchQuery);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchQuery);
    }
}
