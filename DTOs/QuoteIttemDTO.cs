namespace Backend_TechFix.DTOs
{
    public class QuoteItemDTO
    {
        public int QuoteItemID { get; set; }
        public int ProductID { get; set; }
        public int RequestedQuantity { get; set; }
        public decimal QuotedPrice { get; set; }
    }
}
