using Backend_TechFix.DTOs;
using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IRoleService roleService, ILogger<RoleController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    // This action requires the "CanViewRoles" permission
    [Authorize(Policy = "CanViewRolesPolicy")]
    [HttpGet("get-all-roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving roles.");
            return StatusCode(500, "An error occurred while retrieving roles.");
        }
    }

    // This action requires the "CanCreateRole" permission
    [Authorize(Policy = "CanCreateRolePolicy")]
    [HttpPost("create-new-role")]
    public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDto)
    {
        try
        {
            var result = await _roleService.CreateRoleAsync(roleDto);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to create role. Please verify input data.");
            }
            return Ok("Role created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating role.");
            return StatusCode(500, "An error occurred while creating the role.");
        }
    }

    // This action requires the "CanUpdateRole" permission
    [Authorize(Policy = "CanUpdateRolePolicy")]
    [HttpPut("{roleId}")]
    public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleDTO roleDto)
    {
        try
        {
            var result = await _roleService.UpdateRoleAsync(roleId, roleDto);
            if (!result.Succeeded)
            {
                return NotFound("Role not found or update failed.");
            }
            return Ok("Role updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating role with ID {roleId}.");
            return StatusCode(500, "An error occurred while updating the role.");
        }
    }

    // This action requires the "CanDeleteRole" permission
    [Authorize(Policy = "CanDeleteRolePolicy")]
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        try
        {
            var result = await _roleService.DeleteRoleAsync(roleId);
            if (!result.Succeeded)
            {
                return NotFound("Role not found or deletion failed.");
            }
            return Ok("Role deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting role with ID {roleId}.");
            return StatusCode(500, "An error occurred while deleting the role.");
        }
    }

    [Authorize(Policy = "CanAssignRoleToUserPolicy")]
    [HttpPost("assign-role-to-user")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserDTO dto)
    {
        try
        {
            var result = await _roleService.AssignRoleToUserAsync(dto);
            if (!result)
            {
                return BadRequest("Failed to assign role to user.");
            }
            return Ok("Role assigned successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning role to user.");
            return StatusCode(500, "An error occurred while assigning the role.");
        }
    }

    [Authorize(Policy = "CanRemoveRoleFromUserPolicy")]
    [HttpDelete("remove-role-from-user")]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleFromUserDTO dto)
    {
        try
        {
            var result = await _roleService.RemoveRoleFromUserAsync(dto);
            if (!result)
            {
                return BadRequest("Failed to remove role from user.");
            }
            return Ok("Role removed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing role from user.");
            return StatusCode(500, "An error occurred while removing the role.");
        }
    }
}
