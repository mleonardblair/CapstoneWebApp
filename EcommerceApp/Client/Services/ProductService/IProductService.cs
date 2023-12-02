using EcommerceApp.Client.Shared;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.ProductService
{
    /// <summary>
    /// Responsible for interacting with the user interface, managing client-side state, and making HTTP calls to the server. Defines the ProductService contract.
    /// </summary>
    public interface IProductService
    {
        event Action ProductsChanged;
        List<ProductDto> Products { get; set; }
        ProductDto Product { get; set; }
        string Message { get; set; }
        string LastSearchQuery { get; set; }
        string SelectedCategoryName { get; set; }
        string SelectedTagName { get; set; }
        int PageCount { get; set; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        decimal? MaxPrice { get; set; }
        decimal? MinPrice { get; set; }
        bool IsAscending { get; set; }
        List<ProductDto> AdminProducts { get; set; }
        string SnackMessage { get; set; }
        ReusableResultSnackbar Snackbar { get; set; }
        Severity Severity { get; set; }
        Task<string> GetDynamicHeading();
        Task SetSelectedCategory(Guid? categoryId);
        Task SetSelectedTag(Guid? tagId);
        Task<ProductDto> AddOrUpdateProductAsync(ProductDto product);
        // handle filter
        Task<List<string>> GetProductSearchSuggestions(string searchQuery);
        Task GetAllProductsAsync(int page, int pageSize);
        Task GetAllProductsAsync(Guid? categoryId = null);
        Task GetAllProductsAsync(int page, int pageSize, Guid? categoryId = null );
        // New methods with price range parameters
        Task GetAllProductsAsync(int page, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null);
        Task GetAllProductsAsync(int page, int pageSize, bool isAscending, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null);
        Task GetProductsByTag(int page, int pageSize, Guid? tagId =null);
        Task SearchProducts(string searchQuery, int page);
        Task SearchProducts(string searchQuery, int page, decimal? minPrice = null, decimal? maxPrice = null);
        Task SearchProducts(string searchQuery, int page, bool isAscending, decimal? minPrice = null, decimal? maxPrice = null);
        Task SearchProducts(string searchQuery, int page, bool isAscending);
        Task GetProductsByTag(int page, int pageSize, bool isAscending, Guid? tagId = null);
        Task GetProductsByTag(int page, int pageSize, Guid? tagId, decimal? minPrice = null, decimal? maxPrice = null);
        Task GetProductsByTag(int page, int pageSize, bool isAscending, Guid? tagId, decimal? minPrice = null, decimal? maxPrice = null);
        Task GetAllProductsAsync(int page, int pageSize, bool isAscending, Guid? categoryId = null);

        Task GetAllProductsAsync(int page, int pageSize, bool isAscending);

        
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId);
        /*        Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, Stream imageFile);*/
        Task<ServiceResponse<List<ProductDto>>> GetProductsByCategoryId(Guid? categoryId);
        Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto product);
        Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(ProductDto product);
        Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId);

        // Get product by id
        Task<ServiceResponse<ProductDto>> GetAdminProductById(Guid productId);
        Task<ServiceResponse<bool>> AddProduct(ProductDto productDto);
        Task<ServiceResponse<bool>> UpdateProduct(ProductDto productDto);
        Task<ServiceResponse<List<ProductDto>>> DeleteProduct(Guid productId);
        ProductDto CreateNewProduct();
        void ResetSnackbarMessage();
        Task GetAdminProducts();
    }
}
