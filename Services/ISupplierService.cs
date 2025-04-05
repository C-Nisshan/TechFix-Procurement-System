using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync();
        Task<bool> CreateSupplierAsync(CreateSupplierDTO dto);
        Task<bool> UpdateSupplierAsync(int id, UpdateSupplierDTO dto);
        Task<bool> DeleteSupplierAsync(int id);
    }
}





