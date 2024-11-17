using Backend_TechFix.DTOs;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RFQController : ControllerBase
{
    private readonly IRFQService _rfqService;
    private readonly ILogger<RFQController> _logger;

    public RFQController(IRFQService rfqService, ILogger<RFQController> logger)
    {
        _rfqService = rfqService;
        _logger = logger;
    }

    [Authorize(Policy = "CanViewRFQsPolicy")]
    [HttpGet("get-all-rfqs")]
    public async Task<IActionResult> GetAllRFQs()
    {
        try
        {
            var rfqs = await _rfqService.GetAllRFQsAsync();
            return Ok(rfqs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RFQs.");
            return StatusCode(500, "An error occurred while retrieving RFQs.");
        }
    }

    [Authorize(Policy = "CanViewRFQsPolicy")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRFQById(int id)
    {
        try
        {
            var rfq = await _rfqService.GetRFQByIdAsync(id);
            return Ok(rfq);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, $"RFQ with ID {id} not found.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving RFQ with ID {id}.");
            return StatusCode(500, "An error occurred while retrieving the RFQ.");
        }
    }

    [Authorize(Policy = "CanCreateRFQPolicy")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRFQ([FromBody] CreateRFQDTO rfqDto)
    {
        try
        {
            var result = await _rfqService.CreateRFQAsync(rfqDto);
            if (!result)
            {
                return BadRequest("Failed to create RFQ.");
            }
            return Ok("RFQ created successfully.");
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid data provided for RFQ creation.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating RFQ.");
            return StatusCode(500, "An error occurred while creating the RFQ.");
        }
    }

    [Authorize(Policy = "CanUpdateRFQPolicy")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRFQ(int id, [FromBody] UpdateRFQDTO rfqDto)
    {
        try
        {
            var result = await _rfqService.UpdateRFQAsync(id, rfqDto);
            if (!result)
            {
                return NotFound("RFQ not found or update failed.");
            }
            return Ok("RFQ updated successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, $"RFQ with ID {id} not found.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating RFQ with ID {id}.");
            return StatusCode(500, "An error occurred while updating the RFQ.");
        }
    }

    [Authorize(Policy = "CanDeleteRFQPolicy")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRFQ(int id)
    {
        try
        {
            var result = await _rfqService.DeleteRFQAsync(id);
            if (!result)
            {
                return NotFound("RFQ not found or deletion failed.");
            }
            return Ok("RFQ deleted successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, $"RFQ with ID {id} not found.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting RFQ with ID {id}.");
            return StatusCode(500, "An error occurred while deleting the RFQ.");
        }
    }
}
