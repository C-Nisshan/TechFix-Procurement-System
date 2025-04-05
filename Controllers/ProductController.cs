using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_TechFix.DTOs;
using Backend_TechFix.Services;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewProductsPolicy")]
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products.");
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }

        [Authorize(Policy = "CanCreateProductPolicy")]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            try
            {
                var result = await _productService.CreateProductAsync(dto);
                if (!result)
                {
                    return BadRequest("Failed to create product.");
                }
                return Ok("Product created successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product.");
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }

        [Authorize(Policy = "CanUpdateProductPolicy")]
        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO dto)
        {
            try
            {
                var result = await _productService.UpdateProductAsync(id, dto);
                if (!result)
                {
                    return NotFound("Product not found or update failed.");
                }
                return Ok("Product updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {id}.");
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [Authorize(Policy = "CanDeleteProductPolicy")]
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (!result)
                {
                    return NotFound("Product not found or deletion failed.");
                }
                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

        [Authorize(Policy = "CanViewProductsPolicy")]
        [HttpGet("get-product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }
    }
}
