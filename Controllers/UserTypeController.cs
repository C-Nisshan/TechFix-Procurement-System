using Backend_TechFix.DTOs;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;
        private readonly ILogger<UserTypeController> _logger;

        public UserTypeController(IUserTypeService userTypeService, ILogger<UserTypeController> logger)
        {
            _userTypeService = userTypeService;
            _logger = logger;
        }

        // Get all user types
        [Authorize(Policy = "CanViewUserTypePolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserTypes()
        {
            try
            {
                var userTypes = await _userTypeService.GetUserTypesAsync();
                return Ok(userTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user types.");
                return StatusCode(500, "An error occurred while retrieving user types.");
            }
        }

        // Get user type by ID
        [Authorize(Policy = "CanViewUserTypePolicy")]
        [HttpGet("{userTypeId}")]
        public async Task<IActionResult> GetUserTypeById(int userTypeId)
        {
            try
            {
                var userType = await _userTypeService.GetUserTypeByIdAsync(userTypeId);
                return Ok(userType);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("UserType not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user type with ID {userTypeId}.");
                return StatusCode(500, "An error occurred while retrieving the user type.");
            }
        }

        // Create a new user type
        [Authorize(Policy = "CanCreateUserTypePolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateUserType([FromBody] CreateUserTypeDTO dto)
        {
            try
            {
                var userType = await _userTypeService.CreateUserTypeAsync(dto);
                return CreatedAtAction(nameof(GetUserTypeById), new { userTypeId = userType.UserTypeID }, userType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user type.");
                return StatusCode(500, "An error occurred while creating the user type.");
            }
        }

        // Update an existing user type
        [Authorize(Policy = "CanUpdateUserTypePolicy")]
        [HttpPut("{userTypeId}")]
        public async Task<IActionResult> UpdateUserType(int userTypeId, [FromBody] UpdateUserTypeDTO dto)
        {
            try
            {
                var updatedUserType = await _userTypeService.UpdateUserTypeAsync(userTypeId, dto);
                return Ok(updatedUserType);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("UserType not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user type with ID {userTypeId}.");
                return StatusCode(500, "An error occurred while updating the user type.");
            }
        }

        // Delete a user type
        [Authorize(Policy = "CanDeleteUserTypePolicy")]
        [HttpDelete("{userTypeId}")]
        public async Task<IActionResult> DeleteUserType(int userTypeId)
        {
            try
            {
                var success = await _userTypeService.DeleteUserTypeAsync(userTypeId);
                if (success)
                {
                    return Ok("UserType deleted successfully.");
                }
                else
                {
                    return NotFound("UserType not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user type with ID {userTypeId}.");
                return StatusCode(500, "An error occurred while deleting the user type.");
            }
        }
    }
}
