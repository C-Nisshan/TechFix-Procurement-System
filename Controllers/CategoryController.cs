using Backend_TechFix.DTOs;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewCategoriesPolicy")]
        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories.");
                return StatusCode(500, "An error occurred while retrieving categories.");
            }
        }

        [Authorize(Policy = "CanCreateCategoryPolicy")]
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(dto);
                if (!result)
                {
                    return BadRequest("Failed to create category.");
                }
                return Ok("Category created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category.");
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }

        [Authorize(Policy = "CanUpdateCategoryPolicy")]
        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO dto)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(id, dto);
                if (!result)
                {
                    return NotFound("Category not found or update failed.");
                }
                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID {id}.");
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }

        [Authorize(Policy = "CanDeleteCategoryPolicy")]
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (!result)
                {
                    return NotFound("Category not found or deletion failed.");
                }
                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }
    }
}
