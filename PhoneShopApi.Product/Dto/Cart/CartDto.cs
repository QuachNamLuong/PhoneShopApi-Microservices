using PhoneShopApi.Product.Models;
using PhoneShopApi.Product.Dto.Cart.Item;

namespace PhoneShopApi.Product.Dto.Cart
{
    public class CartDto : CreateCartRequestDto
    {
        public int Id { get; set; }
        public ICollection<CartItemDto> Items { get; set; } = [];
    }
}
