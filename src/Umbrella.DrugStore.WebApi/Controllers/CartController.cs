using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Context;
using Umbrella.DrugStore.WebApi.Extenssions;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly BloggingContext _context;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly UserManager<UserEntity> _userManager;

        public CartController(BloggingContext context, AuthenticatedUser authenticatedUser, UserManager<UserEntity> userManager)
        {
            _context = context;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("AddChart")]
        [Authorize(Roles = UserRoles.Client)]
        public async Task<IActionResult> AddChartClientAsync([FromBody]ChartViewModel model)
        {
            try
            {
                var client = await _userManager.FindByEmailAsync(_authenticatedUser.Email);

                var OldChart = _context.Charts.Where(w => w.ClientId.Equals(Guid.Parse(client.Id))).AsNoTracking();

                _context.RemoveRange(OldChart);

                //await _context.SaveChangesAsync();

                var chart = model.ToChart(Guid.Parse(client.Id));

                var data = await _context.Charts.AddAsync(chart);

                await _context.SaveChangesAsync();

                return Ok(data.Entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpGet]
        [Route("GetChart")]
        [Authorize(Roles = UserRoles.Client)]
        public async Task<IActionResult> GetChartClientAsync()
        {
            var client = await _userManager.FindByEmailAsync(_authenticatedUser.Email);

            var data = await _context.Charts.Include(i => i.Products).FirstOrDefaultAsync(w => w.ClientId.Equals(Guid.Parse(client.Id)));

            return Ok(data);
        }

    }
}
