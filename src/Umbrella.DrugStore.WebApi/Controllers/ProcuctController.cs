using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
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

        [HttpPut]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
        [Route("updateProductUnit")]
        public async Task<IActionResult> UpdateProductUnitAsync([FromBody] UpdateProductModel model)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(f => f.Active == true && f.Id.Equals(model.Id));
                product.Unit = model.Unit;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Data = product });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message }); ;
            }
        }

        [HttpPut]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductModel model)
        {
            try
            {
                var product = _context.Products.Update(model.ToProduct());
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Data = product.Entity });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message }); ;
            }
        }

        [HttpGet]
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

        [HttpGet]

        //[Authorize(Roles = UserRoles.Restockers)]
        [Route("getProductById")]
        public async Task<IActionResult> GetProductsAsync([FromQuery]Guid id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(w => w.Active.Equals(true) && w.Id.Equals(id));

                return Ok(new ResponseModel { Data = product });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message }); ;
            }
        }
    }
}
