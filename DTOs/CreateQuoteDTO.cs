namespace Backend_TechFix.DTOs
{
    public class CreateQuoteDTO
    {
        public DateTime RequestedDate { get; set; }
        public int SupplierID { get; set; }
        public int RfqID { get; set; }
        public List<CreateQuoteItemDTO> QuoteItems { get; set; }
    }
}
