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
    public class SupplierService : ISupplierService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(DatabaseContext dbContext, ILogger<SupplierService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync()
        {
            return await _dbContext.Suppliers
                .Select(s => new SupplierDTO
                {
                    SupplierID = s.SupplierID,
                    Name = s.Name,
                    ContactEmail = s.ContactEmail,
                    ContactPhone = s.ContactPhone,
                    Address = s.Address
                })
                .ToListAsync();
        }

        public async Task<bool> CreateSupplierAsync(CreateSupplierDTO dto)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                Address = dto.Address
            };

            _dbContext.Suppliers.Add(supplier);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateSupplierAsync(int id, UpdateSupplierDTO dto)
        {
            var supplier = await _dbContext.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            supplier.Name = dto.Name ?? supplier.Name;
            supplier.ContactEmail = dto.ContactEmail ?? supplier.ContactEmail;
            supplier.ContactPhone = dto.ContactPhone ?? supplier.ContactPhone;
            supplier.Address = dto.Address ?? supplier.Address;

            _dbContext.Suppliers.Update(supplier);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await _dbContext.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _dbContext.Suppliers.Remove(supplier);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
