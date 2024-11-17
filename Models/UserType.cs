namespace Backend_TechFix.Models
{
    public class UserType
    {
        public int UserTypeID { get; set; }  // PK
        public string TypeName { get; set; }  // 'TechFix', 'Supplier'

        // Navigation Properties
        public ICollection<User> Users { get; set; }

    }
}