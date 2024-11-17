using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_TechFix.Services;
using Backend_TechFix.DTOs;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(IQuoteService quoteService, ILogger<QuoteController> logger)
        {
            _quoteService = quoteService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewQuotesPolicy")]
        [HttpGet("get-all-quotes")]
        public async Task<IActionResult> GetAllQuotes()
        {
            try
            {
                var quotes = await _quoteService.GetAllQuotesAsync();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quotes.");
                return StatusCode(500, "An error occurred while retrieving quotes.");
            }
        }

        [Authorize(Policy = "CanViewQuotesPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteById(int id)
        {
            try
            {
                var quote = await _quoteService.GetQuoteByIdAsync(id);
                return Ok(quote);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Quote with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving quote with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the quote.");
            }
        }

        [Authorize(Policy = "CanCreateQuotePolicy")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteDTO quoteDto)
        {
            try
            {
                var result = await _quoteService.CreateQuoteAsync(quoteDto);
                if (!result)
                {
                    return BadRequest("Failed to create quote.");
                }
                return Ok("Quote created successfully.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid data provided for quote creation.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quote.");
                return StatusCode(500, "An error occurred while creating the quote.");
            }
        }

        [Authorize(Policy = "CanUpdateQuotePolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, [FromBody] UpdateQuoteDTO quoteDto)
        {
            try
            {
                var result = await _quoteService.UpdateQuoteAsync(id, quoteDto);
                if (!result)
                {
                    return NotFound("Quote not found or update failed.");
                }
                return Ok("Quote updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Quote with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating quote with ID {id}.");
                return StatusCode(500, "An error occurred while updating the quote.");
            }
        }

        [Authorize(Policy = "CanUpdateQuoteStatusPolicy")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateQuoteStatus(int id, [FromBody] QuoteStatusUpdateDTO statusDto)
        {
            try
            {
                var result = await _quoteService.UpdateQuoteStatusAsync(id, statusDto);
                if (!result)
                {
                    return NotFound("Quote not found or status update failed.");
                }
                return Ok("Quote status updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Quote with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for quote with ID {id}.");
                return StatusCode(500, "An error occurred while updating the quote status.");
            }
        }

        [Authorize(Policy = "CanDeleteQuotePolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            try
            {
                var result = await _quoteService.DeleteQuoteAsync(id);
                if (!result)
                {
                    return NotFound("Quote not found or deletion failed.");
                }
                return Ok("Quote deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Quote with ID {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting quote with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the quote.");
            }
        }
    }
}
