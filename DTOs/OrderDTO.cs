namespace Backend_TechFix.DTOs
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int SupplierID { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public int? QuoteID { get; set; }
    }
}
