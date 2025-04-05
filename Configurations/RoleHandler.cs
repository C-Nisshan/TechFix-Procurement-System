// Configurations/RoleHandler.cs

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IUserRoleService _userRoleService;

    public RoleHandler(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "User ID not found."));
            return;
        }

        if (await _userRoleService.UserHasRoleAsync(userId, requirement.RoleName))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(new AuthorizationFailureReason(this, $"User does not have the {requirement.RoleName} role."));
        }
    }
}



