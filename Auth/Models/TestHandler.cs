
namespace Gaspadorius.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


public class HasPhoneNumberHandler : AuthorizationHandler<PhoneRequirement, string>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PhoneRequirement requirement, string resource)
    {
        System.Console.WriteLine("in handler");

        // foreach (var claim in context.User.Claims)
        // {
        //     Console.WriteLine($"{claim.Type}: {claim.Value}");
        // }

        if (!string.IsNullOrEmpty(resource))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

// public record PhoneRequirement : IAuthorizationRequirement;


public class PhoneRequirement : IAuthorizationRequirement
{
    // public PhoneRequirement(string? phone) => phone = Phone;
    // public string? Phone { get; set; }
}