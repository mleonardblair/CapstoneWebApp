using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using AutoMapper;
using EcommerceApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        public async Task<ServiceResponse<List<CategoryDto>>> UpdateCategory(CategoryDto category)
        {
            // Validate the category name (only letters, no digits or special characters but does take french characters)
       if (!Regex.IsMatch(category.Name, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$"))
            {
                return new ServiceResponse<List<CategoryDto>>
                {
                    Success = false,
                    Message = "Invalid category name. Only letters are allowed.",
                    Data = null,
                    StatusCode = 400 // Bad Request
                };
            }

            // Check for name uniqueness only on non-deleted categories as if deleted they wont be visible so we dont want confusion with the user names being taken they cant see.
            var isNameUsed = await _context.Categories
                            .AnyAsync(c => c.Name == category.Name && c.Id != category.Id && !c.Deleted);

            if (isNameUsed)
            {
                return new ServiceResponse<List<CategoryDto>>
                {
                    Success = false,
                    Message = "Category name already exists.",
                    Data = null,
                    StatusCode = 400 // Bad Request
                };
            }

            // Check if the category already exists
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<List<CategoryDto>>
                {
                    Success = false,
                    Message = "Category not found.",
                    Data = null,
                    StatusCode = 404 // Not Found
                };
            }

            // Update category properties
            dbCategory.Name = category.Name;
            dbCategory.Description = category.Description;
            dbCategory.Visible = category.Visible;
            dbCategory.Editing = category.Editing;
            dbCategory.IsNew = false;

            try
            {
                await _context.SaveChangesAsync();
                var updatedCategories = await GetAdminCategories();
                return new ServiceResponse<List<CategoryDto>>
                {
                    Success = true,
                    Message = "Category updated successfully.",
                    Data = updatedCategories.Data,
                    StatusCode = 200 // OK
                };
            }
            catch (Exception)
            {
                // Log the exception details here

                return new ServiceResponse<List<CategoryDto>>
                {
                    Success = false,
                    Message = "An error occurred while updating the category.",
                    Data = null,
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public async Task<ServiceResponse<bool>> AddCategory(CategoryDto category)
        { // Validate the category name (only letters, no digits or special characters but does take french characters)
            if (!Regex.IsMatch(category.Name, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$"))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Invalid category name. Only letters are allowed.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }

            //  // get the id of the user who has an email that is the same as the email passed in appUser
            // Check for name uniqueness


            var isNameUsed = await _context.Categories
                            .AnyAsync(c => c.Name == category.Name && c.Id != category.Id && !c.Deleted);

            if (isNameUsed)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Category name already exists.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }
            var response = new ServiceResponse<bool>
            {
                Data = new bool()
            };
            Category c = new()
            {
                Editing = category.IsNew = false
            };
            try
            {
                c = _mapper.Map<Category>(category);
                _context.Categories.Add(c);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    response.Data = false;
                    response.Success = false;
                    response.Message = "Failed to add category.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }
                else
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Category added successfully.";
                    response.StatusCode = 200; // OK
                    return response;
                }
            }
            catch (Exception)
            {
                // Log the exception details here

                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "An unexpected server error occurred while adding the category.",
                    Data = false,
                    StatusCode = 500 // Internal Server Error
                };
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<CategoryDto>>> DeleteCategory(Guid categoryId)
        {
            var response = new ServiceResponse<List<CategoryDto>>()
            {
                Data = new List<CategoryDto>() // Ensure Data is always present
            };
            try
            {
                Category category = await GetCategoryById(categoryId);
                if (category == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Category not found.";
                    response.StatusCode = 404; // Not Found
                    return response;
                }

                if (category.Deleted)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Category already deleted.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }

                category.Deleted = true;
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Failed to delete category.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }

                response = await GetAdminCategories();
                response.Success = true;
                response.Message = "Category deleted successfully.";
                response.StatusCode = 200; // OK
                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Server error occurred.";
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
        public async Task<ServiceResponse<List<CategoryDto>>> GetAdminCategories()
        {
            var response = new ServiceResponse<List<CategoryDto>>();
            var categories = await _context.Categories.Where(c => !c.Deleted).ToListAsync();

            if (categories == null || !categories.Any())
            {
                response.Success = false;
                response.Message = "Categories are not found.";
            }
            else
            {
                var convertedCat = _mapper.Map<List<CategoryDto>>(categories);
                response.Data = convertedCat;
                response.Message = "All went well.";
            }
            return response;
        }

        public async Task<Category> GetCategoryById(Guid Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(e => e.Id == Id);
            return category == null ? throw new KeyNotFoundException($"Category with Id {Id} not found.") : category;
        }
        /// <summary>
        /// When called returns the categories as a list of CategoryDTOs, that are not deleted and are visible.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var response = new ServiceResponse<List<CategoryDto>>();
            // Deleted categories are only accessible via Administrator role directly interacting with database, so consequently they are ignored for gets.
            var categories = await _context.Categories.Where(c=>!c.Deleted && c.Visible).ToListAsync();

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
        public async Task<ServiceResponse<Dictionary<Guid, string>>> GetCategoryNamesAsync()
        {
            var response = new ServiceResponse<Dictionary<Guid, string>>();
            try
            {
                var categories = await _context.Categories.Where(p => !p.Deleted).ToDictionaryAsync(c => c.Id, c => c.Name);
                response.Data = categories;
                response.Success = true;
                response.Message = "Categories retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
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
                    response.Message = "Category updated successfully.";
                    response.StatusCode = 200; // OK
                    return response;
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Server not available.";
                response.StatusCode = 500; // Internal Server Error
                return response;
            }
        }

        /// <summary>
        /// The method will return the category name associated with the category id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResponse<string>> GetCategoryNameByIdAsync(Guid id)
        {
            var response = new ServiceResponse<string>();

            // Await the asynchronous operation to get the category
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            // Check if the category is null
            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found.";
                response.StatusCode = 404; // Not Found
            }
            else
            {
                response.Success = true;
                response.Message = "Category found.";
                response.StatusCode = 200; // OK
                response.Data = category.Name; // Directly access the Name property
            }

            return response;
        }

    }
}
