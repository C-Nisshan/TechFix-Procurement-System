using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IRFQService
    {
        Task<IEnumerable<RFQDTO>> GetAllRFQsAsync();
        Task<bool> CreateRFQAsync(CreateRFQDTO dto);
        Task<bool> UpdateRFQAsync(int id, UpdateRFQDTO dto);
        Task<bool> DeleteRFQAsync(int id);
        Task<RFQDetailsDTO> GetRFQByIdAsync(int id);
    }
}
