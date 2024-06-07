using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.API.Domain.Security;
using Shop.API.Models;
using Shop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(UserManager<User> userManager, IOptions<JwtConfig> config) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var isValidPassword = await userManager.CheckPasswordAsync(user, model.Password);

            if(user == null || !isValidPassword) 
            { 
                return Unauthorized();
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateToken(authClaims);

            var response = new AccessTokenResponse()
            {
                AccessToken  = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = token.ValidTo.Second,
                RefreshToken = "",
            };
            return Ok(response);
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Secret));

            var token = new JwtSecurityToken
            (
                issuer: config.Value.ValidIssuer,
                audience: config.Value.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
