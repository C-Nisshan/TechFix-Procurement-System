namespace Backend_TechFix.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }  // PK
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }

        // Navigation Properties
        public ICollection<SupplierProduct> SupplierProducts { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Rfq> Rfqs { get; set; }
    }
}
 