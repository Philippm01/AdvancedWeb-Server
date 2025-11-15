using AdvancedWeb_Server.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using WorldModel;

namespace AdvancedWeb_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<WorldModelUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            WorldModelUser? worldUser = await userManager.FindByNameAsync(loginRequest.Username);
            if (worldUser is null)
            {
                return Unauthorized("Invalid Username");
            }
            bool loginStatus = await userManager.CheckPasswordAsync(worldUser, loginRequest.Password);
            if (!loginStatus)
            {
                return Unauthorized("Invalid Password");
            }
            JwtSecurityToken JwtToken = await jwtHandler.GenerateTokenAsync(worldUser);
            string stringToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Mom loves us",
                Token = stringToken

            });

        }
    }
}