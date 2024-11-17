using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.UserName) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            try
            {
                var token = await _authService.Authenticate(loginDto);
                if (token == null)
                {
                    return Unauthorized("Invalid username or password.");
                }
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during login attempt for username: {loginDto.UserName}");
                return StatusCode(500, "An error occurred while processing the login request.");
            }
        }

        [Authorize(Policy = "CanUpdateSuperAdminCredentialsPolicy")]
        [HttpPost("update-superadmin-credentials")]
        public async Task<IActionResult> UpdateCredentials(UpdateCredentialsDTO dto)
        {
            try
            {
                var result = await _authService.UpdateSuperAdminCredentials(dto);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
                return Ok("Credentials updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating super admin credentials.");
                return StatusCode(500, "An error occurred while updating credentials.");
            }
        }

        [Authorize(Policy = "CanCreateUserPolicy")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDTO dto)
        {
            try
            {
                var result = await _authService.CreateUserAsync(dto);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [Authorize(Policy = "CanUpdateUserPolicy")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO dto)
        {
            try
            {
                var result = await _authService.UpdateUserAsync(dto);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user.");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        [Authorize(Policy = "CanViewUsersPolicy")]
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
                if (!users.Any())
                {
                    return NotFound("There are no records.");
                }
                var userDtos = users.Select(u => new UserDetailsDTO
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsSuperAdmin = u.IsSuperAdmin,
                    UserTypeID = u.UserTypeID,
                    SupplierID = u.SupplierID
                }).ToList();
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users.");
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        [Authorize(Policy = "CanViewUsersPolicy")]
        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _authService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = new UserDetailsDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsSuperAdmin = user.IsSuperAdmin,
                    UserTypeID = user.UserTypeID,
                    SupplierID = user.SupplierID
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        [Authorize(Policy = "CanViewUsersPolicy")]
        [HttpGet("get-user-by-username/{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            try
            {
                if (int.TryParse(userName, out _))
                {
                    return BadRequest("Username must be a string.");
                }

                var user = await _authService.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = new UserDetailsDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsSuperAdmin = user.IsSuperAdmin,
                    UserTypeID = user.UserTypeID,
                    SupplierID = user.SupplierID
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with username {userName}.");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        [Authorize(Policy = "CanDeleteUserPolicy")]
        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _authService.DeleteUserAsync(userId);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user.");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
