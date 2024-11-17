using Backend_TechFix.Configurations;
using Backend_TechFix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Database
{
    public static class PermissionsSeeder
    {
        public static async Task SeedPermissionsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            // Create permissions if they do not exist
            foreach (var permissionName in Permissions.AllPermissions)
            {
                bool permissionExists = await dbContext.Permissions.AnyAsync(p => p.Name == permissionName);
                if (!permissionExists)
                {
                    dbContext.Permissions.Add(new Permission { Name = permissionName });
                }
            }

            await dbContext.SaveChangesAsync();

            // Retrieve Super Admin role, assuming it's already created in a separate seeder
            var superAdminRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Super Admin");
            if (superAdminRole == null)
            {
                Console.WriteLine("Super Admin role does not exist. Ensure Super Admin seeding is completed first.");
                return;
            }

            // Assign all permissions to Super Admin role if not already assigned
            var superAdminRoleId = superAdminRole.Id;
            var allPermissionIds = await dbContext.Permissions.Select(p => p.Id).ToListAsync();
            var existingRolePermissions = (await dbContext.RolePermissions
                .Where(rp => rp.RoleId == superAdminRoleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync()).ToHashSet();

            var newRolePermissions = allPermissionIds
                .Where(permissionId => !existingRolePermissions.Contains(permissionId))
                .Select(permissionId => new RolePermission
                {
                    RoleId = superAdminRoleId,
                    PermissionId = permissionId
                });

            dbContext.RolePermissions.AddRange(newRolePermissions);
            await dbContext.SaveChangesAsync();
        }
    }
}
