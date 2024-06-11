using PhoneShopApi.Product.Models;
using PhoneShopApi.Product.Dto.Order.Item;

namespace PhoneShopApi.Product.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int PaymentId { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
    }
}
