using System.Net.Http.Json;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;
        public string Message { get; set; } = "Loading categories...";
        public List<CategoryDto> Categories { get ; set; } = new List<CategoryDto>();
        public CategoryDto Category { get; set; } = new CategoryDto();

        // This event will be used to notify subscribers that the categories have changed
        public event Action CategoriesChanged;
        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetAllCategoriesAsync()
        {
            try
            {

                var result = await _http.GetFromJsonAsync<ServiceResponse<List<CategoryDto>>>("api/categories");
                if (result != null && result.Data != null)
                {
                    Categories = result.Data;
                    Console.WriteLine($"Server Message: {result.Message}");
                    Console.WriteLine("Successfully retrieved categories.");
                    // Notify subscribers that the products have changed
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
            CategoriesChanged?.Invoke();
        }



        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<CategoryDto>>($"api/categories/{categoryId}");

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

