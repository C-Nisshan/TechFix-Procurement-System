namespace Backend_TechFix.Models
{
    public class InventoryItem
    {
        public int InventoryItemID { get; set; }  // PK
        public int SupplierID { get; set; }  // FK
        public int ProductID { get; set; }  // FK
        public int StockLevel { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
    }
}


