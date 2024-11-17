namespace Backend_TechFix.Models
{
    public class Brand
    {
        public int BrandID { get; set; }  // PK
        public string BrandName { get; set; }

        // Navigation Properties
        public ICollection<Product> Products { get; set; }
    }
}
