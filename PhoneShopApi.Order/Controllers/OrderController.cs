using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Ordering.Models;
using PhoneShopApi.Ordering.Data;
using PhoneShopApi.Ordering.Dto.Order;
using PhoneShopApi.Ordering.Dto.Order.Item;
using PhoneShopApi.Ordering.Mappers;

namespace PhoneShopApi.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(PhoneShopDbContext context) : Controller
    {
        private readonly PhoneShopDbContext _context = context;

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<ICollection<OrderDto>>> GetAllOrders()
        {
            var orders = await _context.Orders
                                .Include(o => o.OrderItems)
                                .ThenInclude(i => i.PhoneOption)
                                .ThenInclude(po => po.BuiltInStorage)
                                .Include(o => o.OrderItems)
                                .ThenInclude(i => i.PhoneOption)
                                .ThenInclude(po => po.PhoneColor)
                                .Include(po => po.OrderItems)
                                .ThenInclude(i => i.PhoneOption)
                                .ThenInclude(po => po.Phone)
                                .ToListAsync();
            var ordersDto = orders.Select(o => o.ToOrderDto());

            return Ok(ordersDto);
        }

        [HttpGet("GetAllOrder/user/{userId}")]
        public async Task<ActionResult<ICollection<OrderDto>>> GetAllUserOrders(string userId)
        {
            var orders = await _context.Orders
                                .Where(o => o.UserId == userId && o.IsUserDeleted == false)
                                .Include(o => o.OrderItems)
                                .ThenInclude(i => i.PhoneOption)
                                .ThenInclude(po => po.BuiltInStorage)
                                .Include(o => o.OrderItems)
                                .ThenInclude(i => i.PhoneOption)
                                .ThenInclude(po => po.PhoneColor)
                                .Include(po => po.OrderItems)
                                .ThenInclude(i => i.PhoneOption)
                                .ThenInclude(po => po.Phone)
                                .ToListAsync();

            var ordersDto = orders.Select(o => o.ToOrderDto());

            return Ok(ordersDto);
        }

        [HttpGet]
        [Route("order/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.PhoneOption)
                .ThenInclude(po => po.BuiltInStorage)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.PhoneOption)
                .ThenInclude(po => po.PhoneColor)
                .Include(po => po.OrderItems)
                .ThenInclude(i => i.PhoneOption)
                .ThenInclude(po => po.Phone)
                .FirstOrDefaultAsync();
            if (order is null) return NotFound($"Order is not found.");

            return Ok(order.ToOrderDto());
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderRequestDto createOrderRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest("Cannot create order now.");

            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == createOrderRequestDto.UserId);
            if (user is null) return NotFound("user not found.");

            var payment = await _context.Payments.FindAsync(createOrderRequestDto.PaymentId);
            if (payment == null) return NotFound("Payment not found.");

            var newOrder = new Order
            {
                UserId = createOrderRequestDto.UserId,
                PaymentId = createOrderRequestDto.PaymentId,
                ShippingAddress = createOrderRequestDto.ShippingAddress,
                CreateAt = DateTime.Now
            };

            user.Orders.Add(newOrder);

            await _context.SaveChangesAsync();
            return Ok(newOrder.ToOrderDto());

        }

        [HttpPost]
        [Route("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(
            [FromBody] CreateOrderItemRequestDto createOrderItemRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest("Cannot create order item now.");

            var phoneOption = await _context.PhoneOptions
                .Include(po => po.PhoneColor)
                .Include(po => po.Phone)
                .Include(po => po.BuiltInStorage)
                .FirstOrDefaultAsync(po => po.Id == createOrderItemRequestDto.PhoneOptionId);
            if (phoneOption == null) return NotFound("Phone Option not found.");

            var order = await _context.Orders
                .Where(o => o.Id == createOrderItemRequestDto.OrderId)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync();
            if (order == null) return NotFound("Order not found.");

            order.TotalPrice += createOrderItemRequestDto.Quantity * phoneOption.Price;
            var phoneQuantity = phoneOption.Quantity;
            phoneQuantity -= createOrderItemRequestDto.Quantity;
            if (phoneQuantity < 0) return BadRequest("This phone is sell out.");

            phoneOption.Quantity = phoneQuantity;

            var orderItem = await _context.OrderItems
                        .Where(i => i.PhoneOptionId == createOrderItemRequestDto.PhoneOptionId)
                        .FirstOrDefaultAsync();
            if (orderItem != null)
            {
                orderItem.Quantity += createOrderItemRequestDto.Quantity;
                orderItem.Price += phoneOption.Price * createOrderItemRequestDto.Quantity;
            }
            else
            {
                var newOrderItem = createOrderItemRequestDto.ToOrderItemFromCreateOrderItemRequestDto();
                newOrderItem.Price = phoneOption.Price * createOrderItemRequestDto.Quantity;

                order.OrderItems.Add(newOrderItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Success");
        }

    }
}
