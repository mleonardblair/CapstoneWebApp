using System.Net.Http.Json;
using EcommerceApp.Client.Pages.Admin;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;
        public string SnackMessage { get; set; } = "";
        public Severity Severity { get; set; } = Severity.Error;
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
            OnChange?.Invoke();
        }

        public async Task GetAllCategoriesShopAsync()
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
                SnackMessage= $"An error occurred: {ex.Message}";
            }
            OnChange?.Invoke();
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

        public Task GetCategoryById(Guid categoryId)
        {
            throw new NotImplementedException();
        }
        public async Task GetAdminCategories()
        {
           var response = await _http.GetFromJsonAsync<ServiceResponse<List<CategoryDto>>>("api/categories/admin");
            if (response != null && response?.Data != null)
            {
                AdminCategories = response.Data;
                Categories = response.Data;

            }
            OnChange?.Invoke();
        }

 

        public async Task<ServiceResponse<bool>> AddCategory(CategoryDto categoryDto)
        {
            var response = await _http.PostAsJsonAsync("api/categories/admin", categoryDto);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if(result != null && result.Success == false)
            {
                // If we hit a server validation, warn basically for invalid input.
                  ServiceResponse<bool> categories = new()
                  {
                    Data = false,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Warning;
                SnackMessage = result.Message;
                await GetAdminCategories();
                return categories;
            }
            if (result != null && result.Success == true)
            {
                ServiceResponse<bool> categories = new()
                {
                    Data = false,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Success;
                SnackMessage = result.Message;
                await GetAdminCategories();

                return categories;
            }
            else
            {
                ServiceResponse<bool> categories = new() { Data = false, Success = false, Message = result.Message, StatusCode = result.StatusCode };
                Severity = Severity.Error;
                SnackMessage = result.Message;
                await GetAdminCategories();
                return categories;
            }
        }

        /// <summary>
        ///  When called this method will update the category on the server. It will also update the local categories. 
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<CategoryDto>>> UpdateCategory(CategoryDto categoryDto)
        {
            var response = await _http.PutAsJsonAsync("api/categories/admin", categoryDto);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CategoryDto>>>();
            if (result != null && result.Success == true)
            {
                ServiceResponse<List<CategoryDto>> categories = new()
                {
                    Data = null,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Success;
                SnackMessage = result.Message;
                await GetAdminCategories();
               
                return categories;
            }
            else
            {
                ServiceResponse<List<CategoryDto>> categories = new()
                {
                    Data = null,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Error;
                SnackMessage = result.Message;
                await GetAdminCategories();
                return categories;
            }

 
        }

        public async Task<ServiceResponse<List<CategoryDto>>> DeleteCategory(Guid categoryId)
        {
            var response = await _http.DeleteAsync($"api/categories/admin/{categoryId}");
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CategoryDto>>>();
            // Check what the error message.
            if (result != null && result.Success == true)
            {
                ServiceResponse<List<CategoryDto>>  categories = new()
                {
                    Data = result.Data,
                    Success = result.Success,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Success;
                SnackMessage = result.Message;
                await GetAdminCategories();

                return categories;
            }
            else // the operation failed.
            {
                ServiceResponse<List<CategoryDto>> categories = new()
                {
                    Data = result.Data,
                    Success = result.Success,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Warning;
                SnackMessage = result.Message;
                await GetAdminCategories();

                return categories;
            }

          
        }


        public CategoryDto CreateNewCategory() { 
           var newCategory = new CategoryDto
           {
                IsNew = true,
                Editing = true
            };
            AdminCategories.Add(newCategory);
            OnChange?.Invoke();
            return newCategory;
        }

        public void ResetSnackbarMessage()
        {
            SnackMessage = "";
            Severity = Severity.Success; // Reset to a default severity
        }

    }

}

