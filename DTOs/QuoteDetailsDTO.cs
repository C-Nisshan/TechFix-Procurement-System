namespace Backend_TechFix.DTOs
{
    public class QuoteDetailsDTO
    {
        public int QuoteID { get; set; }
        public DateTime RequestedDate { get; set; }
        public int SupplierID { get; set; }
        public string QuoteStatus { get; set; }
        public int RfqID { get; set; }
        public List<QuoteItemDTO> QuoteItems { get; set; }
    }
}
