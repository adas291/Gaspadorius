
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gaspadorius.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
namespace Gaspadorius.Auth;

public class JwtTokenService
{
    private readonly SymmetricSecurityKey _authSignKey;
    private readonly string? _audience;
    private readonly string? _issuer;

    public JwtTokenService(IConfiguration configuration)
    {
        _authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        _issuer = configuration["JWT:ValidIssuer"];
        _audience = configuration["JWT:ValidAudience"];
    }

    public string CreateAccessToken(UserDto user, IEnumerable<string> userRoles)
    {
        var authClaims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Name, user.Name),
            new(JwtRegisteredClaimNames.FamilyName, user.Surname),
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        if(string.IsNullOrEmpty(user.Phone) == false)
        {
            authClaims.Add(new(JwtRegisteredClaimNames.PhoneNumber, user.Phone));
        }

        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var accessSecurityToken = new JwtSecurityToken
        (
            issuer: _issuer,
            audience: _audience,
            expires: DateTime.UtcNow.AddDays(5),
            claims: authClaims,
            signingCredentials: new SigningCredentials(_authSignKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(accessSecurityToken);
    }

}