using PhoneShopApi.Dto.Order;
using PhoneShopApi.Models;
using PhoneShopApi.Dto.Order.Item;

namespace PhoneShopApi.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                Id = orderModel.Id,
                PaymentId = orderModel.PaymentId,
                ShippingAddress = orderModel.ShippingAddress,
                OrderStatus = orderModel.OrderStatus,
                TotalPrice = orderModel.TotalPrice,
                UserId = orderModel.UserId,
                CreateAt = orderModel.CreateAt,
                Items = orderModel.OrderItems.Select(i => i.ToOrderItemDto()).ToList()
            };
        }

        public static Order ToOrderFromCreateOrderRequestDto(
            this CreateOrderRequestDto createOrderRequestDto)
        {
            return new Order
            {
                PaymentId = createOrderRequestDto.PaymentId,
                ShippingAddress = createOrderRequestDto.ShippingAddress,
                UserId = createOrderRequestDto.UserId
            };
        }

        public static OrderItemDto ToOrderItemDto(this OrderItem orderItemModel)
        {
            var phone = orderItemModel.PhoneOption.Phone;
            var phoneColor = orderItemModel.PhoneOption.PhoneColor;
            var storage = orderItemModel.PhoneOption.BuiltInStorage;
            return new OrderItemDto
            {
                PhoneOptionId = orderItemModel.PhoneOptionId,
                PhoneName = phone.Name,
                PhoneColor = phoneColor.Name,
                ImageUrl = phoneColor.ImageUrl,
                Stogare = $"{storage.Capacity} {storage.Unit}",
                Price = orderItemModel.Price,
                Quantity = orderItemModel.Quantity
            };
        }

        public static OrderItem ToOrderItemFromCreateOrderItemRequestDto(
            this CreateOrderItemRequestDto createOrderItemRequestDto)
        {
            return new OrderItem
            {
                OrderId = createOrderItemRequestDto.OrderId,
                PhoneOptionId = createOrderItemRequestDto.PhoneOptionId,
                Quantity = createOrderItemRequestDto.Quantity
            };
        }
    }
}
