
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action CategoriesChanged;
        List<CategoryDto> Categories { get; set; }
        Task GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid productId);

    }
}
