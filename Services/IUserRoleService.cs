// Services/IUserRoleService.cs
public interface IUserRoleService
{
    Task<bool> UserHasRoleAsync(string userId, string roleName);
}
