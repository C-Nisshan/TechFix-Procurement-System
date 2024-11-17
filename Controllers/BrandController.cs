using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_TechFix.DTOs;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewBrandsPolicy")]
        [HttpGet("get-all-brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var brands = await _brandService.GetAllBrandsAsync();
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving brands.");
                return StatusCode(500, "An error occurred while retrieving brands.");
            }
        }

        [Authorize(Policy = "CanCreateBrandPolicy")]
        [HttpPost("create-brand")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDTO dto)
        {
            try
            {
                var result = await _brandService.CreateBrandAsync(dto);
                if (!result)
                {
                    return BadRequest("Failed to create brand.");
                }
                return Ok("Brand created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating brand.");
                return StatusCode(500, "An error occurred while creating the brand.");
            }
        }

        [Authorize(Policy = "CanUpdateBrandPolicy")]
        [HttpPut("update-brand/{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] UpdateBrandDTO dto)
        {
            try
            {
                var result = await _brandService.UpdateBrandAsync(id, dto);
                if (!result)
                {
                    return NotFound("Brand not found or update failed.");
                }
                return Ok("Brand updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating brand with ID {id}.");
                return StatusCode(500, "An error occurred while updating the brand.");
            }
        }

        [Authorize(Policy = "CanDeleteBrandPolicy")]
        [HttpDelete("delete-brand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                var result = await _brandService.DeleteBrandAsync(id);
                if (!result)
                {
                    return NotFound("Brand not found or deletion failed.");
                }
                return Ok("Brand deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting brand with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the brand.");
            }
        }
    }
}
