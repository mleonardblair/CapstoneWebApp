using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace EcommerceApp.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public string SelectedCategoryName { get; set; }
        public string SelectedTagName { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public ProductDto Product { get; set; } = new ProductDto();
        public string Message { get; set; } = "Loading products...";

        public string LastSearchQuery { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; } = 0;
        public decimal? MaxPrice { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public bool IsAscending { get; set; } = true;

        // This event will be used to notify subscribers that the products have changed
        public event Action ProductsChanged;
        public async Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto product)
        {
            ServiceResponse<ProductDto> result = new();

            var productResponse = await _http.PostAsJsonAsync("api/products/create", product);
            if (productResponse.IsSuccessStatusCode)
            {
                var newProduct = await productResponse.Content.ReadFromJsonAsync<ProductDto>();
                result.Success = true;
                result.Message = "Product successfully added.";
                result.Data = newProduct;
            }
            else
            {
                result.Success = false;
                result.Message = "Failed to add product.";
            }

            return result;
        }

        public async Task GetAllProductsAsync(Guid? categoryId)
        {
            var result = (categoryId == null || categoryId == Guid.Empty)
             ? await _http.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>("api/products")
             : await _http.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>($"api/products?categoryId={categoryId}");



            if (result != null && result.Data != null)
            {
                Products = result.Data;
            }
            CurrentPage = 1;
            PageCount = 0;

            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }

        /// <summary>
        /// Retrieves a paginated list of all products asynchronously.
        /// </summary>
        /// <param name="page">The page number of products to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="categoryId">The optional category ID to filter products by.</param>
        /// <returns>A task that represents the asynchronous operation, returning a paginated list of products.</returns>
        public async Task GetAllProductsAsync(int page, int pageSize, Guid? categoryId)
        {
            string url = categoryId.HasValue && categoryId != Guid.Empty
                 ? $"api/products?page={page}&pageSize={pageSize}&categoryId={categoryId}"
                 : $"api/products?page={page}&pageSize={pageSize}";

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);


            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }

        /// <summary>
        /// Retrieves a product by its ID asynchronously.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, returning a service response containing the product information.</returns>

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId)
        {
            try
            {
                // Using server api http client - mediated 
                var result = await _http.GetFromJsonAsync<ServiceResponse<ProductDto>>($"api/products/{productId}");
                if (result != null && result.Data != null)
                {
                    return new ServiceResponse<ProductDto>
                    {
                        Data = result.Data,
                        Message = result.Message,
                        Success = result.Success
                    };
                }
                else
                {
                    return new ServiceResponse<ProductDto>
                    {
                        Success = false,
                        Message = "Failed to locate product."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Deletes a product by its ID asynchronously.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>A task that represents the asynchronous operation, returning a service response indicating the success of the operation.</returns>

        public async Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId)
        {

            try
            {
                var response = await _http.DeleteAsync($"api/products/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    // If you're sending a ServiceResponse from the server when deleting
                    var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
                    return result ?? new ServiceResponse<bool> { Success = false, Message = "Unexpected error." };
                }
                else
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Failed to delete product."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        /// <summary>
        /// Updates a product asynchronously by sending a PUT request to the server.
        /// </summary>
        /// <param name="product">The product object containing the updated information.</param>
        /// <returns>A task that represents the asynchronous operation, returning a service response indicating the success of the update.</returns>

        public async Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(ProductDto product)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/products", product);
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize directly into ServiceResponse<bool>
                    var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ProductDto>>();
                    return result ?? new ServiceResponse<ProductDto> { Success = false, Message = "Unexpected error." };
                }
                else
                {
                    return new ServiceResponse<ProductDto> { Success = false, Message = "Failed to update product." };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductDto> { Success = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retrieves a list of products by their category ID asynchronously.
        /// </summary>
        /// <param name="categoryId">The ID of the category to filter products by.</param>
        /// <returns>A task that represents the asynchronous operation, returning a service response containing the list of products in the specified category.</returns>
        public async Task<ServiceResponse<List<ProductDto>>> GetProductsByCategoryId(Guid? categoryId)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>($"api/products/category/{categoryId}");
                if (response != null && response.Data != null)
                {
                    Products = response.Data;
                    ProductsChanged?.Invoke(); // Make sure to invoke the change event
                    return response;
                }
                else
                {
                    return new ServiceResponse<List<ProductDto>>
                    {
                        Success = false,
                        Message = "No products found in this category."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<ProductDto>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Searches for products based on the provided query asynchronously.
        /// </summary>
        /// <param name="searchQuery">The search query used to find products.</param>
        /// <returns>A task that represents the asynchronous operation of searching for products and updating the result.</returns>
        public async Task SearchProducts(string searchQuery)
        {
            LastSearchQuery = searchQuery;
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products/search?searchQuery={searchQuery}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;// These are <ProductDto> 's  =)
            }

            if (Products.Count == 0)
            {
                Message = "No products found.";
            }
            ProductsChanged?.Invoke();
        }

        /// <summary>
        /// Searches for products based on the provided query and page number asynchronously.
        /// </summary>
        /// <param name="searchQuery">The search query used to find products.</param>
        /// <param name="page">The page number of search results to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of searching for products, updating the result, and paginating the results.</returns>

        public async Task SearchProducts(string searchQuery, int page)
        {
            LastSearchQuery = searchQuery;
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products/search?searchQuery={searchQuery}&page={page}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;// These are <ProductDto> 's  =)
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "No products found.";
            }
            ProductsChanged?.Invoke();
        }

        /// <summary>
        /// Searches for products based on the provided query, page number, and sorting order asynchronously.
        /// </summary>
        /// <param name="searchQuery">The search query used to find products.</param>
        /// <param name="page">The page number of search results to retrieve.</param>
        /// <param name="isAscending">A boolean flag to specify whether the sorting order is ascending or not.</param>
        /// <returns>
        /// A task that represents the asynchronous operation of searching for products, updating the result, and handling sorting.
        /// </returns>
        public async Task SearchProducts(string searchQuery, int page, bool isAscending)
        {
            LastSearchQuery = searchQuery;
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products/search?searchQuery={searchQuery}&page={page}&isAscending={isAscending}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;// These are <ProductDto> 's  =)
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "No products found.";
            }
            ProductsChanged?.Invoke();
        }
        public async Task SearchProducts(string searchQuery, int page, bool isAscending, decimal? minPrice = null, decimal? maxPrice = null)
        {
            LastSearchQuery = searchQuery;
            string url = $"api/products/search?searchQuery={Uri.EscapeDataString(searchQuery)}&page={page}&isAscending={isAscending}";

            if (minPrice.HasValue)
            {
                url += $"&minPrice={minPrice}";
            }
            if (maxPrice.HasValue)
            {
                url += $"&maxPrice={maxPrice}";
            }

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products; // These are <ProductDto>'s
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "No products found.";
            }
            ProductsChanged?.Invoke();
        }

        public async Task SearchProducts(string searchQuery, int page, decimal? minPrice = null, decimal? maxPrice = null)
        {

            LastSearchQuery = searchQuery;
            string url = $"api/products/search?searchQuery={searchQuery}&page={page}&minPrice={minPrice}&maxPrice={maxPrice}";

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;// These are <ProductDto> 's  =)
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "No products found.";
            }
            ProductsChanged?.Invoke();
        }
        /// <summary>
        /// Searches for products based on the provided query, page number, and page size asynchronously.
        /// </summary>
        /// <param name="searchQuery">The search query used to find products.</param>
        /// <param name="page">The page number of search results to retrieve.</param>
        /// <param name="pagesize">The number of products per page.</param>
        /// <returns>A task that represents the asynchronous operation of searching for products, updating the result, and paginating the results.</returns>

        public async Task SearchProducts(string searchQuery, int page, int pagesize)
        {
            LastSearchQuery = searchQuery;
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products/search?searchQuery={searchQuery}&page={page}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;// These are <ProductDto> 's  =)
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "No products found.";
            }
            ProductsChanged?.Invoke();
        }

        /// <summary>
        /// Retrieves a list of search suggestions for product search based on the provided search query asynchronously.
        /// </summary>
        /// <param name="searchQuery">The search query used to generate search suggestions.</param>
        /// <returns>
        /// A task that represents the asynchronous operation of retrieving search suggestions.
        /// The task returns a list of strings containing search suggestions or an empty list if no suggestions are available.
        /// </returns>
        public async Task<List<string>> GetProductSearchSuggestions(string searchQuery)
        {
            var response = await _http.GetAsync($"api/products/suggestions/{searchQuery}");

            // Check if the response is successful and is of JSON content type
            if (response.IsSuccessStatusCode && response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<string>>>();
                return result?.Data ?? new List<string>();
            }
            else
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<string>();
            }
        }


        /// <summary>
        /// Retrieves all products asynchronously with optional sorting and category filtering.
        /// </summary>
        /// <param name="page">The page number of products to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="isAscending">A boolean flag to specify whether the sorting order is ascending or not.</param>
        /// <param name="categoryId">The optional category ID to filter products by.</param>
        /// <returns>
        /// A task that represents the asynchronous operation of retrieving products,
        /// optionally sorting them, filtering by category, and updating the result.
        /// </returns>
        public async Task GetAllProductsAsync(int page, int pageSize, bool isAscending, Guid? categoryId)
        {
            // conditionally show the products by category or not
            var result = (categoryId == null || categoryId == Guid.Empty) ?
                 await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?page={page}&pageSize={pageSize}&isAscending={isAscending}")
               : await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?categoryId={categoryId}&page={page}&pageSize={pageSize}&isAscending={isAscending}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged.Invoke();
        }

        /// <summary>
        /// Retrieves all products asynchronously with optional sorting.
        /// </summary>
        /// <param name="page">The page number of products to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="isAscending">A boolean flag to specify whether the sorting order is ascending or not.</param>
        public async Task GetAllProductsAsync(int page, int pageSize, bool isAscending)
        {
            // conditionally show the products by category or not
            var result = (isAscending == false) ?
                 await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?page={page}&pageSize={pageSize}")
               : await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?page={page}&pageSize={pageSize}&isAscending={isAscending}");



            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }  
        
        public async Task GetAllProductsAsync(int page, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null)
        {
            string url = (categoryId == null || categoryId == Guid.Empty)
       ? $"api/products?page={page}&pageSize={pageSize}&minPrice={minPrice}&maxPrice={maxPrice}"
       : $"api/products?categoryId={categoryId}&page={page}&pageSize={pageSize}&minPrice={minPrice}&maxPrice={maxPrice}";

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }
        public async Task GetAllProductsAsync(int page, int pageSize)
        {

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?page={page}&pageSize={pageSize}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }

            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }

        public async Task GetAllProductsAsync(int page, int pageSize, bool isAscending, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null)
        {
            string url = categoryId == null || categoryId == Guid.Empty
       ? $"api/products?page={page}&pageSize={pageSize}&isAscending={isAscending}&minPrice={minPrice}&maxPrice={maxPrice}"
       : $"api/products?categoryId={categoryId}&page={page}&pageSize={pageSize}&isAscending={isAscending}&minPrice={minPrice}&maxPrice={maxPrice}";

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();

        }

        /// <summary>
        /// Retrieves products based on the provided tag ID, page number, and page size asynchronously.
        /// </summary>
        /// <param name="page">The page number of products to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="tagId">The optional tag ID to filter products by.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving products and updating the result based on the provided tag ID.</returns>
        public async Task GetProductsByTag(int page, int pageSize, Guid? tagId = null)
        {
            // conditionally show the products by category or not
            var result = (tagId == null || tagId == Guid.Empty)
             ? await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?page={page}&pageSize={pageSize}")
             : await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products/tag?tagId={tagId}&page={page}&pageSize={pageSize}");


            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }

        public async Task GetProductsByTag(int page, int pageSize, bool isAscending, Guid? tagId = null)
        {
            // conditionally show the products by category or not
            var result = (tagId == null || tagId == Guid.Empty) ?
                 await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products?page={page}&pageSize={pageSize}&isAscending={isAscending}")
               : await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>($"api/products/tag?tagId={tagId}&page={page}&pageSize={pageSize}&isAscending={isAscending}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }
        public async Task GetProductsByTag(int page, int pageSize, Guid? tagId, decimal? minPrice = null, decimal? maxPrice = null)
        {
            string url = (tagId == null || tagId == Guid.Empty)
       ? $"api/products?page={page}&pageSize={pageSize}&minPrice={minPrice}&maxPrice={maxPrice}"
       : $"api/products/tag?tagId={tagId}&page={page}&pageSize={pageSize}&minPrice={minPrice}&maxPrice={maxPrice}";

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();
        }

        public async Task GetProductsByTag(int page, int pageSize, bool isAscending, Guid? tagId, decimal? minPrice = null, decimal? maxPrice = null)
        {
            string url = $"api/products/tag?tagId={tagId}&page={page}&pageSize={pageSize}&isAscending={isAscending}&minPrice={minPrice}&maxPrice={maxPrice}";
            if (tagId == null || tagId == Guid.Empty)
            {
                url = $"api/products?page={page}&pageSize={pageSize}&isAscending={isAscending}&minPrice={minPrice}&maxPrice={maxPrice}";
            }

            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductPaginationResponse>>(url);

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            else
            {
                // Handle the case where result or result.Data is null
                // You can set a default value or handle the error accordingly.
                Products = new List<ProductDto>(); // Example: Initialize Products with an empty list
                CurrentPage = 0; // Example: Set CurrentPage to 0
                PageCount = 0; // Example: Set PageCount to 0
                Message = "No products found."; // Example: Set a default error message
            }
            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Products.Count == 0)
                Message = "No products found.";

            // Notify subscribers that the products have changed
            ProductsChanged?.Invoke();

        }
        // way to get the category name
        public async Task SetSelectedCategory(Guid? categoryId)
        {
            if (categoryId.HasValue && categoryId.Value != Guid.Empty)
            {
                // Fetch the category name based on the ID
                var categoryResponse = await _http.GetFromJsonAsync<ServiceResponse<CategoryDto>>($"api/categories/{categoryId}");
                if (categoryResponse != null && categoryResponse.Data != null)
                {
                    SelectedCategoryName = categoryResponse.Data.Name;
                }
            }
            else
            {
                SelectedCategoryName = null;
            }
        }

        // way to get the tag name
        public async Task SetSelectedTag(Guid? tagId)
        {
            if (tagId.HasValue && tagId.Value != Guid.Empty)
            {
                // Fetch the category name based on the ID
                var tagResponse = await _http.GetFromJsonAsync<ServiceResponse<TagDto>>($"api/tags/{tagId}");
                if (tagResponse != null && tagResponse.Data != null)
                {
                    SelectedTagName = tagResponse.Data.Name;
                }
            }
            else
            {
                SelectedTagName = null;
            }
        }
        public async Task<string> GetDynamicHeading()
        {
            var heading = new StringBuilder("Catalog");

            if (!string.IsNullOrWhiteSpace(LastSearchQuery))
            {
                heading.Clear().Append($"Search Results for '{LastSearchQuery}'");
            }
            else if (!string.IsNullOrWhiteSpace(SelectedCategoryName))
            {
                heading.Clear().Append($"Category: {SelectedCategoryName}");
            }
            else if (!string.IsNullOrWhiteSpace(SelectedTagName))
            {
                heading.Clear().Append($"Tag: {SelectedTagName}");
            }

            if (MinPrice.HasValue || MaxPrice.HasValue)
            {
                heading.Append($" (Price: {MinPrice?.ToString("C") ?? "Any"} - {MaxPrice?.ToString("C") ?? "Any"})");
            }

            heading.Append(IsAscending ? " - Ascending" : " - Descending");

            return heading.ToString();
        }
    }
}
