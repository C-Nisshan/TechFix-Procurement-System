using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
        Task<PermissionDto> GetPermissionByIdAsync(int id);
        Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(int roleId);
        Task<bool> AddPermissionsToRoleAsync(int roleId, List<int> permissionIds);
        Task<bool> RemovePermissionsFromRoleAsync(int roleId, List<int> permissionIds);
    }
}
