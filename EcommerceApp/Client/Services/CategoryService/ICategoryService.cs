
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        List<CategoryDto> Categories { get; set; }
        Task GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid productId);
    }
}
