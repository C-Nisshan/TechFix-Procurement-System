using Backend_TechFix.Database;
using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(DatabaseContext dbContext, ILogger<CategoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _dbContext.Categories.Add(category);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDTO dto)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return false;

            category.CategoryName = dto.CategoryName ?? category.CategoryName;

            _dbContext.Categories.Update(category);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return false;

            _dbContext.Categories.Remove(category);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
