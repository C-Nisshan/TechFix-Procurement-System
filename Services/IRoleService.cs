using Microsoft.AspNetCore.Identity;
using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<IdentityRole<int>>> GetRolesAsync();
        Task<IdentityResult> CreateRoleAsync(RoleDTO roleDto);
        Task<IdentityResult> UpdateRoleAsync(int roleId, RoleDTO roleDto);
        Task<IdentityResult> DeleteRoleAsync(int roleId);
        Task<bool> UserHasPermissionAsync(string permission);
        Task<bool> AssignRoleToUserAsync(AssignRoleToUserDTO dto);
        Task<bool> RemoveRoleFromUserAsync(RemoveRoleFromUserDTO dto);
    }
}
