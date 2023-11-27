using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Server.Models;
using EcommerceApp.Server.Services.CategoryService;
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
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAllCategoriesAsync()
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
        [HttpGet("admin/{categoryId}"), Authorize(Roles ="Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> GetAdminCategoriesById()
        {
            var result = await _categoryService.GetAdminCategories();
            return Ok(result);
        }

        [HttpDelete("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> DeleteCategory(Guid id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return Ok(result);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> AddCategory(CategoryDto category)
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
