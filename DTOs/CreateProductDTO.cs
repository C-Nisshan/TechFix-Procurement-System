namespace Backend_TechFix.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public string ModelNumber { get; set; }
        public List<CreateSupplierProductDTO> SupplierProducts { get; set; }
    }
}
