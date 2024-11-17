using Backend_TechFix.Enums;

namespace Backend_TechFix.Models
{
    public class Order
    {
        public int OrderID { get; set; }  // PK
        public DateTime OrderDate { get; set; }
        public int SupplierID { get; set; }  // FK
        public decimal TotalAmount { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }  // Pending, Confirmed, Shipped, Delivered, Cancelled
        public int? QuoteID { get; set; }

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public Quote Quote { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}


