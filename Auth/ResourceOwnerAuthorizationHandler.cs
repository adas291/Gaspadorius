namespace Gaspadorius.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, IUserOwnedResource>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerRequirement requirement, IUserOwnedResource resource)
    {
        // foreach (var claim in context.User.Claims)
        // {
        //     Console.WriteLine($"{claim.Type}: {claim.Value}");
        // }
        // System.Console.WriteLine(resource.UserId);

        System.Console.WriteLine(context.User.FindFirstValue(JwtRegisteredClaimNames.Sub));

        if (context.User.IsInRole(Roles.Admin) ||
            context.User.FindFirstValue(JwtRegisteredClaimNames.Sub) == resource.UserId.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record ResourceOwnerRequirement : IAuthorizationRequirement;