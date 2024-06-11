using PhoneShopApi.Ordering.Dto.Cart;
using PhoneShopApi.Ordering.Dto.Cart.Item;
using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this Cart cartModel)
        {
            return new CartDto
            {
                Id = cartModel.Id,
                UserId = cartModel.UserId,
                Items = cartModel.CartItems.Select(i => i.ToCartItemDto()).ToList()
            };
        }

        public static Cart ToCartFromCreateCartRequestDto(this CreateCartRequestDto createCartRequestDto)
        {
            return new Cart
            {
                UserId = createCartRequestDto.UserId
            };
        }

        public static CartItemDto ToCartItemDto(this CartItem cartItemModel)
        {
            var phone = cartItemModel.PhoneOption.Phone;
            var phoneColor = cartItemModel.PhoneOption.PhoneColor;
            var phoneOption = cartItemModel.PhoneOption;
            var storage = cartItemModel.PhoneOption.BuiltInStorage;
            return new CartItemDto
            {
                PhoneOptionId = cartItemModel.PhoneOptionId,
                PhoneName = phone.Name,
                PhoneColor = phoneColor.Name,
                ImageUrl = phoneColor.ImageUrl,
                Price = phoneOption.Price,
                Quantity = cartItemModel.Quantity,
                Stogare = $"{storage.Capacity} {storage.Unit}"
            };
        }

        public static CartItem ToCartItemFromCreateCartItemRequestDto(
            this CreateCartItemRequestDto createCartItemRequestDto)
        {
            return new CartItem
            {
                PhoneOptionId = createCartItemRequestDto.PhoneOptionId,
                CartId = createCartItemRequestDto.CartId,
                Quantity = createCartItemRequestDto.Quantity
            };
        }


    }
}
