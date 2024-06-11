using PhoneShopApi.Auth.Models;
using PhoneShopApi.Auth.Dto.Cart.Item;

namespace PhoneShopApi.Auth.Dto.Cart
{
    public class CartDto : CreateCartRequestDto
    {
        public int Id { get; set; }
        public ICollection<CartItemDto> Items { get; set; } = [];
    }
}
