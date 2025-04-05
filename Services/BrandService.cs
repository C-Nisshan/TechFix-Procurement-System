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
    public class BrandService : IBrandService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<BrandService> _logger;

        public BrandService(DatabaseContext dbContext, ILogger<BrandService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            return await _dbContext.Brands
                .Select(b => new BrandDTO
                {
                    BrandID = b.BrandID,
                    BrandName = b.BrandName
                })
                .ToListAsync();
        }

        public async Task<bool> CreateBrandAsync(CreateBrandDTO dto)
        {
            var brand = new Brand
            {
                BrandName = dto.BrandName
            };

            _dbContext.Brands.Add(brand);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateBrandAsync(int id, UpdateBrandDTO dto)
        {
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null) return false;

            brand.BrandName = dto.BrandName ?? brand.BrandName;

            _dbContext.Brands.Update(brand);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null) return false;

            _dbContext.Brands.Remove(brand);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
