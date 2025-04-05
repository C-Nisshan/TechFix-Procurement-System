// Services/UserRoleService.cs

using System.Threading.Tasks;
using Backend_TechFix.Models;
using Microsoft.AspNetCore.Identity;


public class UserRoleService : IUserRoleService
{
    private readonly UserManager<User> _userManager;

    public UserRoleService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> UserHasRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return await _userManager.IsInRoleAsync(user, roleName);
    }
}
