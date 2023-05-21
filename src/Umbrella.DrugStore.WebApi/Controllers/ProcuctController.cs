using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Context;
using Umbrella.DrugStore.WebApi.Entities;
using Umbrella.DrugStore.WebApi.Models;
using Umbrella.DrugStore.WebApi.Services;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    public class ProcuctController : ControllerBase
    {
        private readonly BloggingContext _context;
        private readonly IAzureStorage _storage;
        public ProcuctController(BloggingContext context, IAzureStorage storage)
        {
            _context = context;
            _storage = storage;
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
        [Route("addProduct")]
        public async Task<IActionResult> CreateProductAsync(IFormFile cover, List<IFormFile> photos, [FromForm]CreateProductModel model)
        {
            try
            {
                var addProduct = model.toProduct();

                BlobResponseDto? response = await _storage.UploadAsync(cover);
                if (!response.Error)
                {
                    addProduct.Images.Add(new Image
                    {
                        Source = response.Blob.Uri,
                        Type = EnumImageType.Cover
                    });
                }

                foreach (var photo in photos)
                {
                    response = await _storage.UploadAsync(photo);
                    if (!response.Error)
                    {
                        addProduct.Images.Add(new Image
                        {
                            Source = response.Blob.Uri,
                            Type = EnumImageType.Galery
                        });
                    }
                }

                var product = await _context.Products.AddAsync(addProduct);
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
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
        [Route("getProduct")]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var products = _context.Products.ToList();
                
                return Ok(new ResponseModel { Data = products.OrderByDescending(o => o.Created) });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message }); ;
            }
        }

        [HttpGet]
        [Route("getProductsUser")]
        public async Task<IActionResult> GetProductsUserAsync()
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
