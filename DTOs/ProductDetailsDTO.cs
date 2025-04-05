using Backend_TechFix.DTOs;

namespace Backend_TechFix.DTOs
{
    public class ProductDetailsDTO : ProductDTO
    {
        public List<CreateSupplierProductDTO> SupplierProducts { get; set; }
    }
}
