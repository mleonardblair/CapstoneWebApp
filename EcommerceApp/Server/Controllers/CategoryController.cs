using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // get category name by id
        [HttpGet("name")]
        public async Task<ActionResult<ServiceResponse<string>>> GetCategoryNameByIdAsync(Guid id)
        {
            var response = await _categoryService.GetCategoryNameByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("names")]
        public async Task<ActionResult<ServiceResponse<Dictionary<Guid, string>>>> GetCategoryNames()
        {
            return await _categoryService.GetCategoryNamesAsync();
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCategoryByIdAsync(Guid categoryId)
        {
            var response = await _categoryService.DeleteCategoryByIdAsync(categoryId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Category>>> PostCategoriesAsync([FromBody] CategoryDto categoryDto)
        {
            var response = await _categoryService.PostCategoryByIdAsync(categoryDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult<ServiceResponse<Category>>> UpdateCategoryByIdAsync(Guid categoryId, CategoryDto categoryToUpdate)
        {
            var response = await _categoryService.UpdateCategoryByIdAsync(categoryId, categoryToUpdate);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> GetAllCategoriesAsync()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategoryByIdAsync(Guid categoryId)
        {
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(result);
        }
        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> GetAdminCategories()
        {
            var result = await _categoryService.GetAdminCategories();
            return Ok(result);
        }
        [HttpGet("admin/{categoryId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> GetAdminCategoriesById()
        {
            var result = await _categoryService.GetAdminCategories();
            return Ok(result);
        }

        [HttpDelete("admin/{categoryId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> DeleteCategory(Guid categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            return Ok(result);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddCategory(CategoryDto category)
        {
            var result = await _categoryService.AddCategory(category);
            return Ok(result);
        }

        [HttpPut("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> UpdateCategory(CategoryDto category)
        {
            var result = await _categoryService.UpdateCategory(category);
            return Ok(result);
        }
    }
}
