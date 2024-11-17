namespace Backend_TechFix.Models
{
    public class Category
    {
        public int CategoryID { get; set; }  // PK
        public string CategoryName { get; set; }

        // Navigation Properties
        public ICollection<Product> Products { get; set; }
    }
}
