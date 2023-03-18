using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.UserName);

            if (userExists is not null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Usuário já existe!" }
                );

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.UserName,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao criar usuário" }
                );

            await _userManager.SetLockoutEnabledAsync(user, false);

            var role = model.IsAdmin ? UserRoles.Admin : UserRoles.User;

            await AddToRoleAsync(user, role);

            return Ok(new ResponseModel { Message = "Usuário criado com sucesso!" });
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("getUsers")]
        public async Task<IActionResult> CreateUserAsync()
        {
            return Ok(new ResponseModel { Data = await _userManager.Users.Select(s => new { s.UserName, s.Email, s.LockoutEnabled}).ToListAsync() });
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Usuário não já existe!" }
                );

            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao atualizar usuário" }
                );

            return Ok(new ResponseModel { Message = "Usuário atualizado com sucesso!" });



        }

        private async Task AddToRoleAsync(IdentityUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new(role));

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
