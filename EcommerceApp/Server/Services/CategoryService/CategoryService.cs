using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using AutoMapper;
using EcommerceApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Client.Services.CategoryService;
using EcommerceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryByIdAsync(Guid categoryId)
        {
            var response = new ServiceResponse<bool>();
            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found";
                return response;
            }

            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                response.Data = true;
                response.Message = "Category deleted successfully";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = $"An error occurred: {e.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var response = new ServiceResponse<List<CategoryDto>>();
            var categories = await _context.Categories.ToListAsync();

            if (categories == null || !categories.Any())
            {
                response.Success = false;
                response.Message = "Categories are not found.";
            }
            else
            {
                response.Data = _mapper.Map<List<CategoryDto>>(categories);
                response.Message = "All went well.";
            }
            return response;
        }

        /// <summary>
        /// When called, this method will return the first category associated with the category id.
        /// </summary>
        /// <param name="Id">The category id that represents the category.</param>
        /// <returns>The category requested.</returns>
        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid Id)
        {
            var response = new ServiceResponse<CategoryDto>();
            if (_context.Categories != null)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(e=>e.Id==Id);
                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Category not found.";
                }
                else
                {
                    response.Data = _mapper.Map<CategoryDto>(category);

                }
            }
            return response;
        }

        public async Task<ServiceResponse<CategoryDto>> PostCategoryByIdAsync(CategoryDto categoryDto)
        {
            // Map the category to the DTO
            var category = _mapper.Map<Category>(categoryDto);
            var response = new ServiceResponse<CategoryDto>();

            await _context.AddAsync(category);

            // Save the category
            var result = await _context.SaveChangesAsync();

            if (result <= 0)
            {
                response.Success = false;
                response.Message = "Failed to add category.";
            }
            else
            {
                // if successful Map the Category back to a CategoryDTO
                var categoryDtoOut = _mapper.Map<CategoryDto>(category);
                response.Data = categoryDtoOut;
            }
            return response;
        }

        public async Task<ServiceResponse<CategoryDto>> UpdateCategoryByIdAsync(Guid categoryId, [FromBody] CategoryDto categoryToUpdate)
        {
            var _categoryToUpdate = _mapper.Map<Category>(categoryToUpdate);
            var response = new ServiceResponse<CategoryDto>();

            try
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(e => e.Id == _categoryToUpdate.Id);
                if (existingCategory == null)
                {
                    response.Success = false;
                    response.Message = "No category found to update.";
                    response.StatusCode = 404; // Not Found
                    return response;
                }

                existingCategory.Name = categoryToUpdate.Name;
                existingCategory.Description = categoryToUpdate.Description;

                _context.Categories.Update(existingCategory);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    response.Success = false;
                    response.Message = "Failed to update category.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }
                else
                {
                    var categoryDtoOut = _mapper.Map<CategoryDto>(existingCategory);
                    response.Data = categoryDtoOut;
                    response.StatusCode = 200; // OK
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = 500; // Internal Server Error
                return response;
            }
        }
        public async Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CategoryDto categoryDto)
        {
            // Map the category to the DTO
            var category = _mapper.Map<Category>(categoryDto);
            var response = new ServiceResponse<CategoryDto>();

            await _context.AddAsync(category);

            // Save the category
            var result = await _context.SaveChangesAsync();

            if (result <= 0)
            {
                response.Success = false;
                response.Message = "Failed to add category.";
            }
            else
            {
                // if successful Map the Category back to a CategoryDTO
                var categoryDtoOut = _mapper.Map<CategoryDto>(category);
                response.Data = categoryDtoOut;
            }
            return response;
        }

    }
}
