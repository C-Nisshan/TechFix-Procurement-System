using Backend_TechFix.Enums;

namespace Backend_TechFix.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }  // PK
        public DateTime RequestedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SupplierID { get; set; }  // FK
        public int RfqID { get; set; }  // FK to RFQ
        public QuoteStatusEnum QuoteStatus { get; set; }  // Enum for 'Pending', 'Accepted', 'Rejected'

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public Rfq Rfq { get; set; }
        public ICollection<QuoteItem> QuoteItems { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}



