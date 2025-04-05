namespace Backend_TechFix.Models
{
    public class Product
    {
        public int ProductID { get; set; }  // PK
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }  // FK
        public int BrandID { get; set; }  // FK
        public string ModelNumber { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public ICollection<SupplierProduct> SupplierProducts { get; set; }
        public ICollection<QuoteItem> QuoteItems { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<RfqItem> RfqItems { get; set; }
    }
}
