using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Auth.Dto.Cart.Item
{
    public class CreateCartItemRequestDto
    {
        public int CartId { get; set; }
        public int PhoneOptionId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
