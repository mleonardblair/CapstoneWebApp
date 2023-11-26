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
        public List<CategoryDto> AdminCategories { get; set; } = new List<CategoryDto>();
        public CategoryDto Category { get; set; } = new CategoryDto();

        // This event will be used to notify subscribers that the categories have changed
        public event Action OnChange;
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
                    Categories = result.Data;
                    // Notify subscribers that the products have changed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            OnChange.Invoke();
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


        public async Task GetAdminCategories()
        {
           var response = await _http.GetFromJsonAsync<ServiceResponse<List<CategoryDto>>>("api/categories/admin");
            if (response != null && response?.Data != null)
                Categories = response.Data;
           
        }

        public Task GetCategoryById(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task AddCategory(CategoryDto categoryDto)
        {
            var response = await _http.PostAsJsonAsync("api/categories/admin", categoryDto);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<CategoryDto>>>()).Data;
            await GetAllCategoriesAsync();
            OnChange.Invoke();
        }

        public async Task UpdateCategory(CategoryDto categoryDto)
        {
            var response = await _http.PutAsJsonAsync("api/categories/admin", categoryDto);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<CategoryDto>>>()).Data;
            await GetAllCategoriesAsync();
            OnChange.Invoke();
        }

        public async Task DeleteCategory(Guid categoryId)
        {
             var response = await _http.DeleteAsync($"api/categories/admin/{categoryId}");
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<CategoryDto>>>()).Data;
            await GetAllCategoriesAsync();
            OnChange.Invoke();
        }

        public CategoryDto CreateNewCategory() { 
           var newCategory = new CategoryDto
           {
                IsNew = true,
                Editing = true
            };
            AdminCategories.Add(newCategory);
            OnChange.Invoke();
            return newCategory;
        }
    }
}

