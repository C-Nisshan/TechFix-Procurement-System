using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using Backend_TechFix.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Backend_TechFix.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<UserTypeService> _logger;

        public UserTypeService(DatabaseContext dbContext, ILogger<UserTypeService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // Get all user types
        public async Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync()
        {
            try
            {
                var userTypes = await _dbContext.UserTypes.ToListAsync();
                return userTypes.Select(ut => new UserTypeDTO
                {
                    UserTypeID = ut.UserTypeID,
                    TypeName = ut.TypeName
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user types.");
                throw new Exception("An error occurred while retrieving user types.", ex);
            }
        }

        // Get user type by ID
        public async Task<UserTypeDTO> GetUserTypeByIdAsync(int userTypeId)
        {
            try
            {
                var userType = await _dbContext.UserTypes.FindAsync(userTypeId);
                if (userType == null)
                {
                    throw new KeyNotFoundException("UserType not found.");
                }

                return new UserTypeDTO
                {
                    UserTypeID = userType.UserTypeID,
                    TypeName = userType.TypeName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user type with ID {userTypeId}.");
                throw new Exception("An error occurred while retrieving the user type.", ex);
            }
        }

        // Create a new user type
        public async Task<UserTypeDTO> CreateUserTypeAsync(CreateUserTypeDTO dto)
        {
            try
            {
                var userType = new UserType { TypeName = dto.TypeName };
                _dbContext.UserTypes.Add(userType);
                await _dbContext.SaveChangesAsync();

                return new UserTypeDTO
                {
                    UserTypeID = userType.UserTypeID,
                    TypeName = userType.TypeName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user type.");
                throw new Exception("An error occurred while creating the user type.", ex);
            }
        }

        // Update an existing user type
        public async Task<UserTypeDTO> UpdateUserTypeAsync(int userTypeId, UpdateUserTypeDTO dto)
        {
            try
            {
                var userType = await _dbContext.UserTypes.FindAsync(userTypeId);
                if (userType == null)
                {
                    throw new KeyNotFoundException("UserType not found.");
                }

                userType.TypeName = dto.TypeName;
                await _dbContext.SaveChangesAsync();

                return new UserTypeDTO
                {
                    UserTypeID = userType.UserTypeID,
                    TypeName = userType.TypeName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user type with ID {userTypeId}.");
                throw new Exception("An error occurred while updating the user type.", ex);
            }
        }

        // Delete a user type
        public async Task<bool> DeleteUserTypeAsync(int userTypeId)
        {
            try
            {
                var userType = await _dbContext.UserTypes.FindAsync(userTypeId);
                if (userType == null)
                {
                    throw new KeyNotFoundException("UserType not found.");
                }

                _dbContext.UserTypes.Remove(userType);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user type with ID {userTypeId}.");
                throw new Exception("An error occurred while deleting the user type.", ex);
            }
        }
    }
}
