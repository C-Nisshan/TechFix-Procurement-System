namespace Backend_TechFix.Models
{
    public class SupplierProduct
    {
        public int SupplierProductID { get; set; }  // PK
        public int SupplierID { get; set; }  // FK
        public int ProductID { get; set; }  // FK
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
    }
}
