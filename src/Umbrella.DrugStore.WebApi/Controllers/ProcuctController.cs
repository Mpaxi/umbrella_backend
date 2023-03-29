using Microsoft.AspNetCore.Mvc;
using Umbrella.DrugStore.WebApi.Context;
using Umbrella.DrugStore.WebApi.Entities;
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
    }
}
