
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        public string Message { get; set; }
        public List<CategoryDto> Categories { get; set; }
        List<CategoryDto> AdminCategories { get; set; }
        public CategoryDto Category { get; set; }

        // This event will be used to notify subscribers that the categories have changed
        public event Action OnChange;

        Task GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid productId);
        Task GetAdminCategories();
        Task GetCategoryById(Guid categoryId);
        Task AddCategory(CategoryDto categoryDto);
        Task UpdateCategory(CategoryDto categoryDto);
        Task DeleteCategory(Guid categoryId);
        CategoryDto CreateNewCategory();





    }
}
