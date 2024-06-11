using PhoneShopApi.Models;
using PhoneShopApi.Dto.Cart.Item;

namespace PhoneShopApi.Dto.Cart
{
    public class CartDto : CreateCartRequestDto
    {
        public int Id { get; set; }
        public ICollection<CartItemDto> Items { get; set; } = [];
    }
}
