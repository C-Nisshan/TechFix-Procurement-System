namespace Backend_TechFix.Models
{
    public class QuoteItem
    {
        public int QuoteItemID { get; set; }  // PK
        public int QuoteID { get; set; }  // FK
        public int ProductID { get; set; }  // FK
        public int RequestedQuantity { get; set; }
        public decimal QuotedPrice { get; set; }

        // Navigation Properties
        public Quote Quote { get; set; }
        public Product Product { get; set; }
    }
}
