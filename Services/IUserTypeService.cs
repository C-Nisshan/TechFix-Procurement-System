using Backend_TechFix.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_TechFix.Services
{
    public interface IUserTypeService
    {
        Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync();
        Task<UserTypeDTO> GetUserTypeByIdAsync(int userTypeId);
        Task<UserTypeDTO> CreateUserTypeAsync(CreateUserTypeDTO dto);
        Task<UserTypeDTO> UpdateUserTypeAsync(int userTypeId, UpdateUserTypeDTO dto);
        Task<bool> DeleteUserTypeAsync(int userTypeId);
    }
}
