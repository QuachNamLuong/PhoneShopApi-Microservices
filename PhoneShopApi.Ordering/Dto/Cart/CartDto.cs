using PhoneShopApi.Ordering.Models;
using PhoneShopApi.Ordering.Dto.Cart.Item;

namespace PhoneShopApi.Ordering.Dto.Cart
{
    public class CartDto : CreateCartRequestDto
    {
        public int Id { get; set; }
        public ICollection<CartItemDto> Items { get; set; } = [];
    }
}
