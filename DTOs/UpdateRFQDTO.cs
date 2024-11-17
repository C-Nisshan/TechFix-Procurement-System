namespace Backend_TechFix.DTOs
{
    public class UpdateRFQDTO
    {
        public DateTime RequestedDate { get; set; }
        public int? SupplierID { get; set; }
        public List<UpdateRFQItemDTO> RfqItems { get; set; }
    }
}
