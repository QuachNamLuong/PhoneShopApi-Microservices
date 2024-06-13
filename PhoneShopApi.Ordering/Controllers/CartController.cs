using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Ordering.Mappers;
using PhoneShopApi.Ordering.Dto;
using PhoneShopApi.Ordering.Models;
using System.Reflection.Metadata.Ecma335;
using PhoneShopApi.Ordering.Data;

namespace PhoneShopApi.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(PhoneShopDbContext context) : Controller
    {
        private readonly PhoneShopDbContext _context = context;


        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetUserCart(string userId)
        {
            if (!ModelState.IsValid) return BadRequest("Cannot create cart for user, please try again.");
            var cart = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.CartItems)
                .ThenInclude(i => i.PhoneOption)
                .ThenInclude(o => o.Phone)
                .Include(c => c.CartItems)
                .ThenInclude(i => i.PhoneOption)
                .ThenInclude(o => o.PhoneColor)
                .Include(c => c.CartItems)
                .ThenInclude(i => i.PhoneOption)
                .ThenInclude(o => o.BuiltInStorage)
                .FirstOrDefaultAsync();
            if (cart != null) return Ok(cart.ToCartDto());

            cart = new Cart
            {
                UserId = userId
            };
            
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();  
            return Ok(cart.ToCartDto());
        }

        [HttpPost]
        [Route("user/{userId}/phoneOption/{phoneOptionId:int}")]
        public async Task<IActionResult> AddItemToCart(string userId, int phoneOptionId)
        {
            var phoneOption = await _context.PhoneOptions.FindAsync(phoneOptionId);
            if (phoneOption == null) return NotFound("phone option not found.");
            var cart = await _context.Carts
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };
                if (!ModelState.IsValid) return BadRequest("Cannot create cart for user, please try again.");
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = await _context.CartItems
                .Where(c => c.CartId == cart.Id && c.PhoneOptionId == phoneOptionId)
                .FirstOrDefaultAsync();
            if (cartItem != null) return BadRequest("Item exist in your cart");

            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                PhoneOptionId = phoneOptionId,
                Quantity = 1
            };

            await _context.CartItems.AddAsync(newCartItem);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                cartId = newCartItem.CartId,
                phoneOptionId = newCartItem.PhoneOptionId,
                quantity = newCartItem.Quantity
            });
        }

        public record AddItemToCartRequest(string UserId, int PhoneOptionId);

        [HttpPost]
        [Route("AddItemToCart")]
        public async Task<IActionResult> AddItemToCartJson([FromBody] AddItemToCartRequest addItemToCartRequest)
        {
            var phoneOption = await _context.PhoneOptions.FindAsync(addItemToCartRequest.PhoneOptionId);
            if (phoneOption == null) return NotFound("phone option not found.");
            var cart = await _context.Carts
                .Where(c => c.UserId == addItemToCartRequest.UserId)
                .FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = addItemToCartRequest.UserId
                };
                if (!ModelState.IsValid) return BadRequest("Cannot create cart for user, please try again.");
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = await _context.CartItems
                .Where(c => c.CartId == cart.Id && c.PhoneOptionId == addItemToCartRequest.PhoneOptionId)
                .FirstOrDefaultAsync();
            if (cartItem != null) return BadRequest("Item exist in your cart");

            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                PhoneOptionId = addItemToCartRequest.PhoneOptionId,
                Quantity = 1
            };

            await _context.CartItems.AddAsync(newCartItem);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                cartId = newCartItem.CartId,
                phoneOptionId = newCartItem.PhoneOptionId,
                quantity = newCartItem.Quantity
            });
        }

        [HttpPut]
        [Route("cartId/{cartId:int}/phoneOption/{phoneOptionId:int}/quantity/{quantity:int}")]
        public async Task<IActionResult> ChangeQuantityInCartItem(
            int cartId, int phoneOptionId, int quantity)
        {
            if (!ModelState.IsValid) return BadRequest();

            var cartItem = await _context.CartItems
                .Where(i => i.CartId == cartId && i.PhoneOptionId == phoneOptionId)
                .FirstOrDefaultAsync();

            if (cartItem is null) return NotFound("Cart not found.");

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return Ok("Item updated.");
        }

        [HttpDelete]
        [Route("cart/{cartId:int}/phoneOption/{phoneOptionId:int}")]
        public async Task<IActionResult> DeleteCartItem(int cartId, int phoneOptionId)
        {
            var cartItem = await _context.CartItems
                .Where(i => i.CartId == cartId && i.PhoneOptionId == phoneOptionId)
                .FirstOrDefaultAsync();

            if (cartItem is null) return NotFound();

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok("Item deleted.");
        }
    }
}
