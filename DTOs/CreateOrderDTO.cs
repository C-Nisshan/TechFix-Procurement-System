namespace Backend_TechFix.DTOs
{
    public class CreateOrderDTO
    {
        public DateTime OrderDate { get; set; }
        public int SupplierID { get; set; }
        public decimal TotalAmount { get; set; }
        public int QuoteID { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
