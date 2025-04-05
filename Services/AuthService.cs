using Backend_TechFix.DTOs;
using Backend_TechFix.Models;
using Backend_TechFix.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Backend_TechFix.Database;

namespace Backend_TechFix.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthService> _logger;
        private readonly DatabaseContext _context;

        public AuthService(UserManager<User> userManager, JwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger, DatabaseContext context)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _context = context;
        }

        // Method to validate SupplierId
        private async Task<bool> IsSupplierIdValid(int? supplierId)
        {
            if (supplierId == null)
                return true;  // If SupplierId is null, it's valid because it can be nullable.

            var supplier = await _context.Suppliers.FindAsync(supplierId);
            return supplier != null;
        }

        public async Task<string> Authenticate(LoginDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                _logger.LogWarning($"Failed login attempt for username: {loginDto.UserName}");
                return null;
            }

            // Generate token
            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);
            return token;
        }

        public async Task<IdentityResult> UpdateSuperAdminCredentials(UpdateCredentialsDTO dto)
        {
            var user = await _userManager.FindByNameAsync("superadmin");
            if (user == null)
            {
                _logger.LogWarning("Super admin user not found.");
                return IdentityResult.Failed(new IdentityError { Description = "Super admin not found." });
            }

            // Update username and password
            user.UserName = dto.NewUsername;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.NewPassword);

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserDTO dto)
        {
            // Check if username or email already exists
            if (await _userManager.FindByNameAsync(dto.UserName) != null)
            {
                _logger.LogWarning($"Username {dto.UserName} is already taken.");
                return IdentityResult.Failed(new IdentityError { Description = "Username is already taken." });
            }
            if (!string.IsNullOrEmpty(dto.Email) && _userManager.Users.Any(u => u.Email == dto.Email))
            {
                _logger.LogWarning($"Email {dto.Email} is already in use.");
                return IdentityResult.Failed(new IdentityError { Description = "Email is already in use." });
            }

            // Validate SupplierID
            if (!await IsSupplierIdValid(dto.SupplierID))
            {
                _logger.LogWarning($"Invalid SupplierID {dto.SupplierID} provided.");
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The SupplierID you provided does not exist. " +
                    "Please enter valid SupplierId or It can be null."
                });
            }

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                IsSuperAdmin = dto.IsSuperAdmin,
                UserTypeID = dto.UserTypeID,
                SupplierID = dto.SupplierID
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {user.UserName} created successfully.");
            }
            else
            {
                _logger.LogWarning($"Error creating user {dto.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
            {
                _logger.LogWarning($"User with ID {dto.UserId} not found.");
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            // Check if the new email is already taken
            if (!string.IsNullOrEmpty(dto.NewEmail) && _userManager.Users.Any(u => u.Email == dto.NewEmail && u.Id != user.Id))
            {
                _logger.LogWarning($"Email {dto.NewEmail} is already in use.");
                return IdentityResult.Failed(new IdentityError { Description = "Email is already in use." });
            }

            // Validate SupplierID
            if (!await IsSupplierIdValid(dto.SupplierID))
            {
                _logger.LogWarning($"Invalid SupplierID {dto.SupplierID} provided.");
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The SupplierID you provided does not exist. " +
                    "Please enter valid SupplierId or It can be null."
                });
            }

            user.UserName = dto.NewUserName ?? user.UserName;
            user.Email = dto.NewEmail ?? user.Email;
            user.IsSuperAdmin = dto.IsSuperAdmin ?? user.IsSuperAdmin;
            user.UserTypeID = dto.UserTypeID ?? user.UserTypeID;
            user.SupplierID = dto.SupplierID ?? user.SupplierID;

            // If a new password is provided, validate it first
            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                // Validate new password
                var passwordValidator = new PasswordValidator<User>();
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, user, dto.NewPassword);

                if (!passwordValidationResult.Succeeded)
                {
                    // If password does not meet requirements, return an error
                    _logger.LogWarning($"Password validation failed: {string.Join(", ", passwordValidationResult.Errors.Select(e => e.Description))}");
                    return IdentityResult.Failed(passwordValidationResult.Errors.ToArray());
                }

                // If password is valid, hash the new password and update the user
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.NewPassword);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {user.UserName} updated successfully.");
            }
            else
            {
                _logger.LogWarning($"Error updating user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return result;
        }

        public async Task<IQueryable<User>> GetAllUsersAsync()
        {
            var users = _userManager.Users;
            if (!users.Any())
            {
                _logger.LogInformation("No users found.");
            }
            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
            }
            return user;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                _logger.LogWarning($"User with username {userName} not found.");
            }
            return user;
        }

        public async Task<IdentityResult> DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {user.UserName} deleted successfully.");
            }
            else
            {
                _logger.LogWarning($"Error deleting user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return result;
        }
    }
}
