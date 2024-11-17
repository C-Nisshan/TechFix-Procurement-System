using Backend_TechFix.DTOs;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierService supplierService, ILogger<SupplierController> logger)
        {
            _supplierService = supplierService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewSuppliersPolicy")]
        [HttpGet("get-all-suppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                var suppliers = await _supplierService.GetAllSuppliersAsync();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving suppliers.");
                return StatusCode(500, "An error occurred while retrieving suppliers.");
            }
        }

        [Authorize(Policy = "CanCreateSupplierPolicy")]
        [HttpPost("create-supplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierDTO dto)
        {
            try
            {
                var result = await _supplierService.CreateSupplierAsync(dto);
                if (!result)
                {
                    return BadRequest("Failed to create supplier.");
                }
                return Ok("Supplier created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supplier.");
                return StatusCode(500, "An error occurred while creating the supplier.");
            }
        }

        [Authorize(Policy = "CanUpdateSupplierPolicy")]
        [HttpPut("update-supplier/{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierDTO dto)
        {
            try
            {
                var result = await _supplierService.UpdateSupplierAsync(id, dto);
                if (!result)
                {
                    return NotFound("Supplier not found or update failed.");
                }
                return Ok("Supplier updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating supplier with ID {id}.");
                return StatusCode(500, "An error occurred while updating the supplier.");
            }
        }

        [Authorize(Policy = "CanDeleteSupplierPolicy")]
        [HttpDelete("delete-supplier/{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var result = await _supplierService.DeleteSupplierAsync(id);
                if (!result)
                {
                    return NotFound("Supplier not found or deletion failed.");
                }
                return Ok("Supplier deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting supplier with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the supplier.");
            }
        }
    }
}
