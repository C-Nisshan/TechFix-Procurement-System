using Microsoft.AspNetCore.Identity;
using Backend_TechFix.DTOs;
using Backend_TechFix.Database;
using Backend_TechFix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Backend_TechFix.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<RoleService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleService(
            RoleManager<IdentityRole<int>> roleManager,
            UserManager<User> userManager, 
            DatabaseContext dbContext, 
            ILogger<RoleService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        // Retrieve all roles
        public async Task<IEnumerable<IdentityRole<int>>> GetRolesAsync()
        {
            try
            {
                return await _roleManager.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving roles.", ex);
            }
        }

        // Create a new role
        public async Task<IdentityResult> CreateRoleAsync(RoleDTO roleDto)
        {
            try
            {
                var role = new IdentityRole<int> { Name = roleDto.Name };
                return await _roleManager.CreateAsync(role);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the role.", ex);
            }
        }

        // Update an existing role
        public async Task<IdentityResult> UpdateRoleAsync(int roleId, RoleDTO roleDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    throw new KeyNotFoundException("Role not found.");
                }

                role.Name = roleDto.Name;
                return await _roleManager.UpdateAsync(role);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the role.", ex);
            }
        }

        // Delete a role
        public async Task<IdentityResult> DeleteRoleAsync(int roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    throw new KeyNotFoundException("Role not found.");
                }

                return await _roleManager.DeleteAsync(role);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the role.", ex);
            }
        }

        // Check if a user has a specific permission
        public async Task<bool> UserHasPermissionAsync(string permission)
        {
            // Extract the user ID from the claims (sub or NameIdentifier claim)
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                _logger.LogWarning("User ID not found in token.");
                return false;
            }

            int userId = int.Parse(userIdClaim.Value);

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                return false;
            }

            // Get the roles for the user
            var userRoles = await _userManager.GetRolesAsync(user);
            _logger.LogInformation($"User {userId} has roles: {string.Join(", ", userRoles)}");

            // Check if the user has the required permission
            var hasPermission = await _dbContext.RolePermissions
                .Where(rp => userRoles.Contains(rp.Role.Name) && rp.Permission.Name == permission)
                .AnyAsync();

            _logger.LogInformation($"Permission '{permission}' check for User {userId}: {hasPermission}");
            return hasPermission;
        }

        // Assign Role To User
        public async Task<bool> AssignRoleToUserAsync(AssignRoleToUserDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            var role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());

            if (user == null || role == null)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            return result.Succeeded;
        }

        // Remove Role From User
        public async Task<bool> RemoveRoleFromUserAsync(RemoveRoleFromUserDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            var role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());

            if (user == null || role == null)
            {
                return false;
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            return result.Succeeded;
        }


    }
}
