// CreateUserDTO.cs
namespace Backend_TechFix.DTOs
{
    public class CreateUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int UserTypeID { get; set; }
        public int? SupplierID { get; set; }
    }
}
