using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Context;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    public class ProcuctController : ControllerBase
    {
        private readonly BloggingContext _context;
        public ProcuctController(BloggingContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("addProduct")]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductModel model)
        {
            try
            {
                var product = await _context.Products.AddAsync(model.toProduct());
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Data = product.Entity });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message }); ;
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        //[Authorize(Roles = UserRoles.Restockers)]
        [Route("getProduct")]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var products = _context.Products.Where(w => w.Active.Equals(true)).ToList();
                
                return Ok(new ResponseModel { Data = products.OrderByDescending(o => o.Created) });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message }); ;
            }
        }
    }
}
