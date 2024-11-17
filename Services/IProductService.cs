using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<bool> CreateProductAsync(CreateProductDTO dto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDTO dto);
        Task<bool> DeleteProductAsync(int id);
        Task<ProductDetailsDTO> GetProductByIdAsync(int id);
    }
}
