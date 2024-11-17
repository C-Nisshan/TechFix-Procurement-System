namespace Backend_TechFix.DTOs
{
    public class UpdateUserDTO
    {
        public int? UserId { get; set; }
        public string? NewUserName { get; set; }
        public string? NewEmail { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public int? UserTypeID { get; set; }
        public int? SupplierID { get; set; }
        public string? NewPassword { get; set; } 
    }
}
