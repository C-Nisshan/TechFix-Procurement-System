using Backend_TechFix.Database;
using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly DatabaseContext _dbContext;

        public PermissionService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            return await _dbContext.Permissions
                .Select(p => new PermissionDto { Id = p.Id, Name = p.Name })
                .ToListAsync();
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int id)
        {
            var permission = await _dbContext.Permissions.FindAsync(id);
            return permission == null ? null : new PermissionDto { Id = permission.Id, Name = permission.Name };
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(int roleId)
        {
            return await _dbContext.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .Select(rp => new PermissionDto { Id = rp.Permission.Id, Name = rp.Permission.Name })
                .ToListAsync();
        }

        public async Task<bool> AddPermissionsToRoleAsync(int roleId, List<int> permissionIds)
        {
            var existingPermissionIds = (await _dbContext.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync()) 
                .ToHashSet();

            var newPermissions = permissionIds
                .Where(pid => !existingPermissionIds.Contains(pid))
                .Select(pid => new RolePermission { RoleId = roleId, PermissionId = pid });

            await _dbContext.RolePermissions.AddRangeAsync(newPermissions);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemovePermissionsFromRoleAsync(int roleId, List<int> permissionIds)
        {
            var permissionsToRemove = await _dbContext.RolePermissions
                .Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
                .ToListAsync();

            _dbContext.RolePermissions.RemoveRange(permissionsToRemove);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
