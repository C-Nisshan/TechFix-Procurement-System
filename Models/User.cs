using Microsoft.AspNetCore.Identity;

namespace Backend_TechFix.Models
{
    public class User : IdentityUser<int>
    {
        
        public bool IsSuperAdmin { get; set; }

        // Foreign Key for UserType
        public int UserTypeID { get; set; } 
        public UserType UserType { get; set; }

        // Foreign Key for Supplier
        public int? SupplierID { get; set; }
        public Supplier Supplier { get; set; }

    }
}
