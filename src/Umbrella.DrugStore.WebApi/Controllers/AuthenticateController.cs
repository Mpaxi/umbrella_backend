using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;        

        public AuthenticateController(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password) && user.LockoutEnabled.Equals(false))
            {

                var authClaims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var userRole in userRoles)
                    authClaims.Add(new(ClaimTypes.Role, userRole));

                return Ok(new ResponseModel { Data = GetToken(authClaims) });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("deactive")]
        public async Task<IActionResult> Deactive([FromBody] DeactiveModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is not null)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);

                return Ok(new ResponseModel { Data = "Desativado" });

            }

            return BadRequest(new ResponseModel { Data = "Erro" });
        }


        [HttpPost]
        [Route("active")]
        public async Task<IActionResult> Active([FromBody] DeactiveModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is not null)
            {
                await _userManager.SetLockoutEnabledAsync(user, false);

                return Ok(new ResponseModel { Data = "Desativado" });

            }

            return BadRequest(new ResponseModel { Data = "Erro" });
        }

        private TokenModel GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

        }
    }
}
