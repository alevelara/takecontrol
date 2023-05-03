using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Takecontrol.Shared.Application.Constants;

namespace Takecontrol.API.Tests.Helpers;

public static class AuthTestHelper
{
    public static string GenerateToken(string userName, string email, string id, string role)
    {
        var roleClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Role, role)
        };

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(CustomClaimsTypes.Uid, id)
        }
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f0f4d530-3680-455e-9d4c-666135e02003"));
        var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: "TakeControlTest",
            audience: "TakeControlUserTest",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: signInCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
