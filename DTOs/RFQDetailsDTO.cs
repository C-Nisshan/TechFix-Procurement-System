namespace Backend_TechFix.DTOs
{
    public class RFQDetailsDTO
    {
        public int RfqID { get; set; }
        public DateTime RequestedDate { get; set; }
        public int TechFixUserID { get; set; }
        public int SupplierID { get; set; }
        public List<RFQItemDTO> RfqItems { get; set; }
    }
}
