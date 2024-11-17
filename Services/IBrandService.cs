using Backend_TechFix.DTOs;

namespace Backend_TechFix.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<bool> CreateBrandAsync(CreateBrandDTO dto);
        Task<bool> UpdateBrandAsync(int id, UpdateBrandDTO dto);
        Task<bool> DeleteBrandAsync(int id);
    }
}
