
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
        private readonly IHttpClientFactory _httpFactory;
        public ProductService(HttpClient http, IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
            _http = http;
        }

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public ProductDto Product { get; set; } = new ProductDto();




        public async Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto product)
        {
            var httpClient = _httpFactory.CreateClient("EcommerceApp.PublicClient");
            ServiceResponse<ProductDto> result = new ServiceResponse<ProductDto>();

            var productResponse = await httpClient.PostAsJsonAsync("api/products/create", product);
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


        public async Task GetAllProductsAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>("api/products");
            if (result != null && result.Data != null)
                Products = result.Data;
        }
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

        public Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, Stream imageFile)
        {
            throw new NotImplementedException();
        }
    }
}
