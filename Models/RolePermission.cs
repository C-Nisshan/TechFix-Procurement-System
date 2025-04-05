using Microsoft.AspNetCore.Identity;

namespace Backend_TechFix.Models
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public IdentityRole<int> Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
