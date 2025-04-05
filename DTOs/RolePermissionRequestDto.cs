namespace Backend_TechFix.DTOs
{
    public class RolePermissionRequestDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
