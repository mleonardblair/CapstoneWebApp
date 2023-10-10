using System.Net.Http;
using System.Net.Http.Json;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using static System.Net.WebRequestMethods;

namespace EcommerceApp.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public List<CategoryDto> Categories {  get; set; } = new List<CategoryDto>();
        public CategoryDto Category { get; set; } = new CategoryDto();

        private readonly HttpClient _http;
        private readonly IHttpClientFactory _httpFactory;
        public CategoryService(HttpClient http, IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
            _http = http;
        }

        public async Task GetAllCategoriesAsync()
        {
            try
            {
                var httpClient = _httpFactory.CreateClient("EcommerceApp.PublicClient");

                var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<CategoryDto>>>("api/categories");

                if (response != null && response.Data != null)
                {
                    Categories = response.Data;
                    Console.WriteLine($"Server Message: {response.Message}");
                    Console.WriteLine("Successfully retrieved categories.");
                }
                else
                {
                    Console.WriteLine("Received null or empty response from the server.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            var httpClient = _httpFactory.CreateClient("EcommerceApp.PublicClient"); // Replace with whatever name you used to configure the client

            var result = await httpClient.GetFromJsonAsync<ServiceResponse<CategoryDto>>($"api/categories/{categoryId}");

            if (result == null)
            {
                // Return that the category data couldn't be retrieved.
                return new ServiceResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Could not retrieve category data."
                };
            }

            return result;
        }

    }
}

