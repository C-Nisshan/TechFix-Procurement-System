namespace Backend_TechFix.DTOs
{
    public class CreateRFQDTO
    {
        public DateTime RequestedDate { get; set; }
        public int TechFixUserID { get; set; }
        public int SupplierID { get; set; }
        public List<CreateRFQItemDTO> RfqItems { get; set; }
    }
}
