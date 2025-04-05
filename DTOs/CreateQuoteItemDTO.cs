namespace Backend_TechFix.DTOs
{
    public class CreateQuoteItemDTO
    {
        public int ProductID { get; set; }
        public int RequestedQuantity { get; set; }
        public decimal QuotedPrice { get; set; }
    }
}
