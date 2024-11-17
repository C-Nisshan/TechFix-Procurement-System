using Backend_TechFix.DTOs;

namespace Backend_TechFix.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<bool> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDTO dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
