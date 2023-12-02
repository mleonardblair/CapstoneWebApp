
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        public string SnackMessage { get; set; }
        public Severity Severity { get; set; }
        public string Message { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<CategoryDto> AdminCategories { get; set; }
        public CategoryDto Category { get; set; }

        // This event will be used to notify subscribers that the categories have changed
        public event Action OnChange;
        Task<ServiceResponse<Dictionary<Guid, string>>> GetCategoryNamesAsync();
        Task<ServiceResponse<string>> GetCategoryName(Guid id);
        Task GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid productId);
        Task GetAdminCategories();
        Task GetCategoryById(Guid categoryId);
        Task<ServiceResponse<bool>> AddCategory(CategoryDto categoryDto);
        Task<ServiceResponse<List<CategoryDto>>> UpdateCategory(CategoryDto categoryDto);
        Task<ServiceResponse<List<CategoryDto>>> DeleteCategory(Guid categoryId);
        CategoryDto CreateNewCategory();
        void ResetSnackbarMessage();




    }
}
