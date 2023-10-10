using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid Id);
        Task<ServiceResponse<CategoryDto>> PostCategoryByIdAsync(CategoryDto categoryDto);
        Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CategoryDto categoryDto);
        Task<ServiceResponse<CategoryDto>> UpdateCategoryByIdAsync(Guid categoryId, CategoryDto categoryDto);
        Task<ServiceResponse<bool>> DeleteCategoryByIdAsync(Guid categoryId);

    }
}
