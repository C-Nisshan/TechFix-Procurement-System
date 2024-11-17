using Backend_TechFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend_TechFix.Configurations
{
    public class RolePermissionHandler : AuthorizationHandler<RolePermissionRequirement>
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RolePermissionHandler> _logger;

        // Inject IRoleService and ILogger directly
        public RolePermissionHandler(IRoleService roleService, ILogger<RolePermissionHandler> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePermissionRequirement requirement)
        {
            // Get the user from the context
            var userIdClaim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                _logger.LogWarning("User ID not found in claims.");
                context.Fail();
                return;
            }

            int userId = int.Parse(userIdClaim.Value);

            _logger.LogInformation($"Checking permission '{requirement.Permission}' for User ID {userId}.");

            // Check if the user has the required permission
            bool hasPermission = await _roleService.UserHasPermissionAsync(requirement.Permission);

            if (hasPermission)
            {
                _logger.LogInformation("Permission granted.");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("Permission denied.");
                context.Fail();
            }
        }
    }
}


