using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Extenssions;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticatedUser _authenticatedUser;
        public ClientController(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, AuthenticatedUser authenticatedUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticatedUser = authenticatedUser;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateClientAsync([FromBody] CreateUserClientModel model)
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
                Address = model.Address.Select(s => new Address
                {
                    Rua = s.Rua,
                    Numero = s.Numero,
                    Complemento = s.Complemento,
                    CEP = s.CEP,
                    UF = s.UF,
                    Bairro = s.Bairro,
                    Cidade = s.Cidade,
                    Principal = s.Principal

                }).ToList(),
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao criar usuário" }
                );

            await _userManager.SetLockoutEnabledAsync(user, false);

            var role = UserRoles.Client;

            await AddToRoleAsync(user, role);

            return Ok(new ResponseModel { Message = "Usuário criado com sucesso!" });
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = UserRoles.Client)]
        public async Task<IActionResult> UpdateClientAsync([FromBody] UpdateUserClientModel model)
        {
            var user = await _userManager.FindByEmailAsync(_authenticatedUser.Email);
            user.Nome = model.Nome;
            user.Address = model.Address.Select(s => new Address
            {
                Id = s.Id,
                Rua = s.Rua,
                Numero = s.Numero,
                Complemento = s.Complemento,
                CEP = s.CEP,
                UF = s.UF,
                Bairro = s.Bairro,
                Cidade = s.Cidade,
                Principal = s.Principal

            }).ToList();


            var role = UserRoles.Client;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao atualizar usuário" }
                );

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
