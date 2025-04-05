// Configurations/RoleRequirement.cs

using Microsoft.AspNetCore.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public string RoleName { get; }
    public RoleRequirement(string roleName)
    {
        RoleName = roleName;
    }
}
