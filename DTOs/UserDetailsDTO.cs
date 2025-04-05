// UserDetailsDTO.cs
namespace Backend_TechFix.DTOs
{
    public class UserDetailsDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int UserTypeID { get; set; }
        public int? SupplierID { get; set; }
    }
}
