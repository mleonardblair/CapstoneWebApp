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
        Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, IFormFile imageFile);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto productDto);
        Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(Guid productId, ProductDto productDto);
        Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId);
    }
}
