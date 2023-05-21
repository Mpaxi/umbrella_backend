using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Extenssions;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticatedUser _authenticatedUser;
        public UserController(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, AuthenticatedUser authenticatedUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticatedUser = authenticatedUser;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModel model)
        {
            if (!model.Password.Equals(model.ConfirmPassword))
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao criar usuário" }
                );

            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists is not null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Usuário já existe!" }
                );

            UserEntity user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                Nome = model.Nome,
                CPF = model.CPF,
                Masculino = model.Masculino,
                DataNascimento = model.DataNascimento,
                UserName = model.Email,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao criar usuário" }
                );

            await _userManager.SetLockoutEnabledAsync(user, false);

            var role = model.IsAdmin ? UserRoles.Admin : UserRoles.Restockers;

            await AddToRoleAsync(user, role);

            return Ok(new ResponseModel { Message = "Usuário criado com sucesso!" });
        }

        [HttpGet]
        //[Authorize(Roles = UserRoles.Admin)]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var data = _userManager.Users.ToList();
            
            var retorno = data.Select(s => 
            new { 
                s.Id, 
                s.UserName, 
                s.Nome, 
                s.CPF,
                s.Masculino,
                s.DataNascimento,
                s.Email, 
                s.LockoutEnabled,
                s.Rua,
                s.Numero,
                s.Complemento,
                s.Bairro,
                s.Cidade,
                s.CEP,
                Roles = _userManager.GetRolesAsync(s).Result
            }
            ).ToList();

            return Ok(new ResponseModel { Data = retorno });

            //return Ok(new ResponseModel { Data = _userManager.Users.Select(s => new { s.Id, s.UserName, s.Nome, s.CPF, s.Email, s.LockoutEnabled }).ToList() });
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("getUser")]
        public async Task<IActionResult> GetUserAsync([FromQuery] Guid Id)
        {
            return Ok(new ResponseModel { Data = _userManager.Users.Select(s => new { s.Id, s.UserName, s.Nome, s.CPF,
                s.Masculino,
                s.DataNascimento,
                s.Email, s.LockoutEnabled, s.Rua, s.Numero, s.Complemento, s.Bairro, s.Cidade, s.CEP }).FirstOrDefault(w => w.Id.Equals(Id.ToString())) });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserModel model)
        {
            UserEntity user = await _userManager.FindByIdAsync(model.Id.ToString());
            var role = model.IsAdmin ? UserRoles.Admin : UserRoles.Restockers;

            if (user.UserName.Equals(_authenticatedUser.Email))
            {

                if (!model.Password.Equals(model.ConfirmPassword))
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new ResponseModel { Success = false, Message = "Erro ao criar usuário" }
                    );

                if (user is null)
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new ResponseModel { Success = false, Message = "Usuário não já existe!" }
                    );

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);

                if (!result.Succeeded)
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new ResponseModel { Success = false, Message = "Erro ao atualizar usuário" }
                    );

                user.Nome = model.Nome;
                user.CPF = model.CPF;

                result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new ResponseModel { Success = false, Message = "Erro ao atualizar usuário" }
                    );

                await AddToRoleAsync(user, role);

                return Ok(new ResponseModel { Message = "Usuário atualizado com sucesso!" });
            }

            await AddToRoleAsync(user, role);

            return Ok(new ResponseModel { Message = "Usuário atualizado com sucesso!" });
        }

        private async Task AddToRoleAsync(UserEntity user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new(role));

            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
