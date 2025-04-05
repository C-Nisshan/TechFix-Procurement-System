using Microsoft.AspNetCore.Authorization;

namespace Backend_TechFix.Configurations
{
    public class RolePermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public RolePermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
