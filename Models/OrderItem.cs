namespace Backend_TechFix.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }  // PK
        public int OrderID { get; set; }  // FK
        public int ProductID { get; set; }  // FK
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
