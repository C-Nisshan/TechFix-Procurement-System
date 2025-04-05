namespace Backend_TechFix.DTOs
{
    public class UpdateSupplierProductDTO
    {
        public int? SupplierID { get; set; }
        public int? ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
