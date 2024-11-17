using Backend_TechFix.DTOs;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(IPermissionService permissionService, ILogger<PermissionController> logger)
        {
            _permissionService = permissionService;
            _logger = logger;
        }

        [Authorize(Policy = "CanViewPermissionsPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            try
            {
                var permissions = await _permissionService.GetAllPermissionsAsync();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving permissions.");
                return StatusCode(500, "An error occurred while retrieving permissions.");
            }
        }

        [Authorize(Policy = "CanViewPermissionsPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            try
            {
                var permission = await _permissionService.GetPermissionByIdAsync(id);
                if (permission == null)
                {
                    return NotFound("Permission not found.");
                }
                return Ok(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving permission with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the permission.");
            }
        }

        [Authorize(Policy = "CanViewPermissionsPolicy")]
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetPermissionsForRole(int roleId)
        {
            try
            {
                var permissions = await _permissionService.GetPermissionsForRoleAsync(roleId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving permissions for role ID {roleId}.");
                return StatusCode(500, "An error occurred while retrieving permissions.");
            }
        }

        [Authorize(Policy = "CanAssignPermissionsToRolePolicy")]
        [HttpPost("role/{roleId}/add")]
        public async Task<IActionResult> AddPermissionsToRole(int roleId, [FromBody] RolePermissionRequestDto request)
        {
            try
            {
                if (roleId != request.RoleId)
                {
                    return BadRequest("Role ID in URL does not match Role ID in body.");
                }

                var result = await _permissionService.AddPermissionsToRoleAsync(roleId, request.PermissionIds);
                if (!result)
                {
                    return BadRequest("Failed to add permissions.");
                }

                return Ok("Permissions added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding permissions to role ID {roleId}.");
                return StatusCode(500, "An error occurred while adding permissions.");
            }
        }

        [Authorize(Policy = "CanRemovePermissionsFromRolePolicy")]
        [HttpDelete("role/{roleId}/remove")]
        public async Task<IActionResult> RemovePermissionsFromRole(int roleId, [FromBody] RolePermissionRequestDto request)
        {
            try
            {
                if (roleId != request.RoleId)
                {
                    return BadRequest("Role ID in URL does not match Role ID in body.");
                }

                var result = await _permissionService.RemovePermissionsFromRoleAsync(roleId, request.PermissionIds);
                if (!result)
                {
                    return BadRequest("Failed to remove permissions.");
                }

                return Ok("Permissions removed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing permissions for role ID {roleId}.");
                return StatusCode(500, "An error occurred while removing permissions.");
            }
        }
    }
}
