using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorldModel;

namespace AdvancedWeb_Server
{
    public class JwtHandler(UserManager<WorldModelUser> userManager, IConfiguration configuration)
    {
        public async Task<JwtSecurityToken> GenerateTokenAsync(WorldModelUser user)
        {
            return new JwtSecurityToken(  // This is the constructor of JWT security Token (all argeuments are required)
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpiryInMinutes"])),
                signingCredentials: GetSiningCredenials(), 
                claims: await GetClaimsAsync(user)
             );
        }
        private SigningCredentials GetSiningCredenials()
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!);
            var signingkey = new SymmetricSecurityKey(key);
            return new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256);
        }
        public async Task<List<Claim>> GetClaimsAsync(WorldModelUser user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName)];
            //claims.AddRange(await userManager.GetClaimsAsync(user)).Select(role => new Claim(ClaimTypes.Role, role);
            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
