
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        public string Message { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public CategoryDto Category { get; set; }

        // This event will be used to notify subscribers that the categories have changed
        public event Action CategoriesChanged;

        Task GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid productId);

    }
}
