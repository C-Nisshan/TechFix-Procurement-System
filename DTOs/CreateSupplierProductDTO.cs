namespace Backend_TechFix.DTOs
{
    public class CreateSupplierProductDTO
    {
        public int SupplierID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
