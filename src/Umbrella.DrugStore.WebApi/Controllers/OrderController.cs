using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Umbrella.DrugStore.WebApi.Auth;
using Umbrella.DrugStore.WebApi.Context;
using Umbrella.DrugStore.WebApi.Extenssions;
using Umbrella.DrugStore.WebApi.Models;

namespace Umbrella.DrugStore.WebApi.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly BloggingContext _context;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly UserManager<UserEntity> _userManager;
        public OrderController(BloggingContext context, AuthenticatedUser authenticatedUser, UserManager<UserEntity> userManager)
        {
            _context = context;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Client}")]
        [Route("Checkout")]
        public async Task<IActionResult> OrderCheckoutAsync([FromBody] OrderCheckoutViewModel orderCheckout)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(_authenticatedUser.Email);

                var entity = _context.Orders.Add(orderCheckout.ToOrder(Guid.Parse(user.Id)));

                var products = await _context.Products.Where(w => orderCheckout.OrderProducts.Select(s => s.ProductId).Contains(w.Id)).ToListAsync();

                foreach (var product in products)
                {
                    var orderProduct = orderCheckout.OrderProducts.FirstOrDefault(f => f.ProductId == product.Id);

                    if (product.Unit <= 0)
                    {
                        return BadRequest(new ResponseModel { Data = $"Product {product.Name} is out of stock" });
                    }

                    product.Unit -= orderProduct.Quantity;

                    _context.Products.Update(product);
                }

                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Data = entity.Entity });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Client}")]
        [Route("GetUserOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(_authenticatedUser.Email);

                var orders = await _context.Orders.Include(i => i.OrderProducts).ThenInclude(ti => ti.Product).Where(x => x.UserId == Guid.Parse(user.Id)).ToListAsync();

                return Ok(new ResponseModel { Data = orders });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Client}")]
        [Route("GetOrderById")]
        public async Task<IActionResult> GetOrderById([FromQuery] Guid orderId)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(_authenticatedUser.Email);

                var order = await _context.Orders.Include(i => i.OrderProducts).ThenInclude(ti => ti.Product).FirstOrDefaultAsync(f => f.Id == orderId && f.UserId == Guid.Parse(user.Id));

                return Ok(new ResponseModel { Data = order });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
        [Route("GetAdminOrderById")]
        public async Task<IActionResult> GetAdminOrderById([FromQuery] Guid orderId)
        {
            try
            {

                var order = await _context.Orders.Include(i => i.OrderProducts).ThenInclude(ti => ti.Product).FirstOrDefaultAsync(f => f.Id == orderId);

                return Ok(new ResponseModel { Data = order });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
        [Route("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders.Include(i => i.OrderProducts).ThenInclude(ti => ti.Product).ToListAsync();

                return Ok(new ResponseModel { Data = orders });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Restockers}")]
        [Route("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusViewModel updateOrderStatus)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(f => f.Id == updateOrderStatus.OrderId);

                if (order == null)
                {
                    return BadRequest(new ResponseModel { Data = "Order not found" });
                }

                order.Status = updateOrderStatus.Status;

                _context.Orders.Update(order);

                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Data = order });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel { Data = ex.Message });
            }
        }
    }
}
