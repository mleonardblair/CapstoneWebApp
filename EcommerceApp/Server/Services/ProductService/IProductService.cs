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
        Task<ServiceResponse<ProductDto>> AddOrUpdateProductAsync(ProductDto productDto);
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId);
        // Overloaded method for pagination.
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int page, int pageSize, Guid productId);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto productDto);
        Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(Guid productId, ProductDto productDto);
        Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId);
        Task<ServiceResponse<List<ProductDto>>> GetProductsByCategoryId(Guid categoryId);
        Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page);
        Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page, bool isAscending);
        Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page, decimal? minPrice, decimal? maxPrice);
        Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page, bool isAscending, decimal? minPrice, decimal? maxPrice);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchQuery);
        Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync(Guid categoryId);
        Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, Guid categoryId);
        Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, bool isAscending, Guid categoryId);
        Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize);
        Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, bool isAscending);
        Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, bool isAscending, decimal? minPrice, decimal? maxPrice, Guid? categoryId);
        Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null);
        Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, Guid tagId);

        Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, bool isAscending, Guid tagId);
        Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, Guid tagId, decimal?minPrice, decimal? maxPrice);
        Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, bool isAscending, Guid tagId, decimal? minPrice, decimal? maxPrice);


        // Add admin methods
        Task<ServiceResponse<List<ProductDto>>> GetAdminProducts();
        // Delete admin method
        Task<ServiceResponse<List<ProductDto>>> DeleteProduct(Guid productId);
        // Update admin method
        Task<ServiceResponse<bool>> UpdateProduct(ProductDto productDto);
        // Add admin method
        Task<ServiceResponse<bool>> AddProduct(ProductDto productDto);

        // Get product by id
        Task<ServiceResponse<ProductDto>> GetAdminProductById(Guid productId);
    }
}
